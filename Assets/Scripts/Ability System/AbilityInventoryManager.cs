using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbilityInventoryManager : MonoBehaviour
{

    [SerializeField] private bool DEBUG = false;

    private AbilityCursor cursor;

    // Serialized Abilities (for use in editor)
    [SerializeField] private AbilityWrapper[] startingAbilities;
    [SerializeField] private AbilityWrapper dashAbility;
    
    // Fancy new classes for holding items
    [SerializeField] private GameObject slotsRootObject; 
    public SlotHolder<AbilityWrapper> slots;

    // Active Slot bar (NOT HOTBAR)
    [SerializeField] private GameObject activesRootObject;
    public SlotHolder<AbilityWrapper> actives;

    // Passive Slot bar
    [SerializeField] private GameObject passivesRootObject;
    public SlotHolder<AbilityWrapper> passives;

    // HotBar proper
    public HotBar hotbar { get; private set; }
    
    // Augment manager stuff
    private AugmentManager augmentManager;

    // Ability Choice UI dragging
    [SerializeField] private AbilitySelection abilitySelection;

    // Properties for moving items around
    private Slot<AbilityWrapper> originalSlot;
    bool isMovingItem;

    
    private bool managerActive;

    // Start is called before the first frame update
    private void Awake() {
        // slots = new SlotHolder<AbilityWrapper>(GameObject.FindWithTag("Slots"));
        slots = new SlotHolder<AbilityWrapper>(slotsRootObject);
        actives = new SlotHolder<AbilityWrapper>(activesRootObject);
        passives = new SlotHolder<AbilityWrapper>(passivesRootObject);


        // TODO: Remove Hotbar Reference here and change how it works
        hotbar = new HotBar(GameObject.FindWithTag("HotBar"), dashAbility);
        cursor = GameObject.FindWithTag("Cursor").GetComponent<AbilityCursor>();
        managerActive = false;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            if (slotsRootObject == null) {
                slotsRootObject = GameObject.Find("Inventory").transform.GetChild(1).gameObject;
                slots = new SlotHolder<AbilityWrapper>(slotsRootObject);
            }

            if (activesRootObject == null) {
                activesRootObject = GameObject.Find("Inventory").transform.GetChild(3).gameObject;
                actives = new SlotHolder<AbilityWrapper>(activesRootObject);
            }

            if (passivesRootObject == null) {
                passivesRootObject = GameObject.Find("Inventory").transform.GetChild(5).gameObject;
                passives = new SlotHolder<AbilityWrapper>(passivesRootObject);
            }

            if (hotbar == null) {
                hotbar = new HotBar(GameObject.FindWithTag("HotBar"), dashAbility);
            }

            if (cursor == null) {
                cursor = GameObject.FindWithTag("Cursor").GetComponent<AbilityCursor>();
            }
        }
    }
    private void Start()
    {
        // Add Starting Abilities
        for (int i = 0; i < startingAbilities.Length; i++) {
            slots.Items[i] = startingAbilities[i];
            UpdateTooltip(slots[i]);
        }

        // Add AugmentManager reference so we can update augments
        augmentManager = GetComponent<AugmentManager>();
        
        hotbar.SetupActiveBarListener(actives);
        hotbar.Refresh();    
        augmentManager.SetupPassiveBarListener(passives);
    }

    public void OnRunStart() {
        augmentManager.OnRunStart();
    }

    public void OnRunEnd() {
        augmentManager.OnRunEnd();
    }

    private void Update() {
        // One less than hotbar slots length bc dash ability takes up a slot
        if (isMovingItem && !managerActive) {
            EndItemMove();
        }
        if (Input.GetMouseButtonDown(0) && managerActive) 
        {
            if (isMovingItem) {
                EndItemMove();
            } else {
                BeginItemMove();
            }
        }
    }

    #region Active / Passive Utils
    // No longer needed!
    #endregion

    #region Inventory Utils

    /**
    *   Add
    *   Adds Ability to first available slot
    *   
    *   return: true if ability was successfully added, false if not 
    **/
    public bool Add(AbilityWrapper ability)
    {
        Slot<AbilityWrapper> slotToFill = null;

        foreach (Slot<AbilityWrapper> slot in slots) {
            if (slot.Item == null) slotToFill = slot;
            if (slot.Item == ability) {
                return false;
            }
        }

        slotToFill.Item = ability;
        return true;
    }

    public void UpdateTooltip(Slot<AbilityWrapper> slot) {
        slot.formatter.Ability = slot.Item;
    }

    #endregion Inventory Utils

    #region Drag And Drop
    private enum AbilitySource {
        inventory,
        actives,
        passives,
        selection
    }
    private Tuple<Slot<AbilityWrapper>, AbilitySource> GetClosestSlot() {
        // We want different behavior depending on where the skill comes from, 
        // so unfortunately we have to do (something like) this :(

        foreach (Slot<AbilityWrapper> slot in slots) {
            if (Vector2.Distance(slot.gameObject.transform.position, Input.mousePosition) <= 32)
                return Tuple.Create(slot, AbilitySource.inventory);
        }

        foreach (Slot<AbilityWrapper> slot in actives) {
            if (Vector2.Distance(slot.gameObject.transform.position, Input.mousePosition) <= 32)
                return Tuple.Create(slot, AbilitySource.actives);
        }

        foreach (Slot<AbilityWrapper> slot in passives) {
            if (Vector2.Distance(slot.gameObject.transform.position, Input.mousePosition) <= 32)
                return Tuple.Create(slot, AbilitySource.passives);
        }

        foreach (Slot<AbilityWrapper> slot in abilitySelection.Slots){
            if (Vector2.Distance(slot.gameObject.transform.position, Input.mousePosition) <= 32)
                return Tuple.Create(slot, AbilitySource.selection);
        }

        return null;
    }

    private void BeginItemMove() {
        Tuple<Slot<AbilityWrapper>, AbilitySource> closestSlot = GetClosestSlot();
        if (closestSlot == null || closestSlot.Item1.Item == null) {
            return;
        } 
        originalSlot = closestSlot.Item1;

        cursor.Ability = originalSlot.Item; 
        originalSlot.Item = null;
        UpdateTooltip(originalSlot);
        isMovingItem = true;

        // We don't have a slot to go back to, so we treat is as a null
        if (closestSlot.Item2 == AbilitySource.selection) {
            originalSlot.formatter.Disable();
            originalSlot = null;
            // TODO: Signal Ability Selection to hide
            abilitySelection.HideAbilityChoice();
        }


        return;
    }

    private void EndItemMove() {
        Tuple<Slot<AbilityWrapper>, AbilitySource> closestSlot = GetClosestSlot();
        if (closestSlot == null) {
            // If ability came from selection, abort (don't snap back)
            if (originalSlot == null) return;

            // Moving failed, revert
            originalSlot.Item = cursor.Ability;
            cursor.Ability = null;
        } else {
            // Found a candidate slot
            Slot<AbilityWrapper> slotToFill = closestSlot.Item1;

            // Prevent slotting items into selection
            if (closestSlot.Item2 == AbilitySource.selection) return;

            if (slotToFill.Item != null) {

                // Slot is filled, do some swapping
                AbilityWrapper temp = slotToFill.Item;
                slotToFill.Item = cursor.Ability;
                cursor.Ability = temp;
                UpdateTooltip(slotToFill);
                // Don't null originalSlot, we might need it later
                return;
            } else {
                // We can safely null originalSlot
                originalSlot = null;

                // Slot is not filled
                slotToFill.Item = cursor.Ability;
                UpdateTooltip(slotToFill);
                cursor.Ability = null;
            }
        }
        isMovingItem = false;
        return;
    }
    #endregion Drag And Drop
    public void SetManagerActive(bool active) {
        managerActive = active;
    }
}
