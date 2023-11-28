using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AbilityHolder : MonoBehaviour
{

    [Header("Dash Ability")]
    private Slot<AbilityWrapper> dashAbility;

    [Header("Mutable Abilities")]
    private Slot<AbilityWrapper>[]  mutableAbilities = new Slot<AbilityWrapper>[3];

    [SerializeField] private GameObject abilitySelection;
    [SerializeField] private AbilityInventoryManager abilityManager;
    public bool abilityInventoryActive {get; private set;}

    // Start is called before the first frame update
    void Awake() {
        abilityInventoryActive = false;
    }
    void Start()
    {
        if (dashAbility == null) {
            dashAbility = abilityManager.hotbar.GetDashAbilitySlot();
        }
        for (int i = 0; i < mutableAbilities.Length; i++) {
            if (mutableAbilities[i] == null)
                mutableAbilities[i] = abilityManager.hotbar.GetMutableAbilitySlot(i);
        }
    }

    // Update is called once per frame
    void Update()
    {  
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            if (abilitySelection == null) {
                abilitySelection = GameObject.Find("AbilitySelection").transform.gameObject;
            }
        }
        if (dashAbility.IsClear() == false) {
            dashAbility.Item.ActiveAbility.AbilityCooldownHandler(gameObject);
            dashAbility.Item.ActiveAbility.AbilityBehavior(gameObject);
            dashAbility.SetFillAmount(dashAbility.Item.ActiveAbility.fillAmount);
        }

        foreach (Slot<AbilityWrapper> slot in mutableAbilities) {
            if (slot.IsClear() == false) {
                slot.Item.ActiveAbility.AbilityCooldownHandler(gameObject);
                slot.Item.ActiveAbility.AbilityBehavior(gameObject);
                slot.SetFillAmount(slot.Item.ActiveAbility.fillAmount);
            }
        }

    }

    private void OnAbility(Slot<AbilityWrapper> slot, InputAction.CallbackContext context) {

        if (slot.IsClear() == true)
            return;

        Ability ability = slot.Item.ActiveAbility;


        if (context.started)
            ability.SetAbilityPressed(true);
        else if (context.canceled)
            ability.SetAbilityPressed(false);


    }
    public void OnAbility1(InputAction.CallbackContext context) 
    {
        OnAbility(mutableAbilities[0], context);
    }

    public void OnAbility2(InputAction.CallbackContext context) 
    {
        OnAbility(mutableAbilities[1], context);
    }

    public void OnAbility3(InputAction.CallbackContext context) 
    {
        OnAbility(mutableAbilities[2], context);
    }

    public void OnDashAbility(InputAction.CallbackContext context) {
        OnAbility(dashAbility, context);
    }

    public void OnAbilityInventory(InputAction.CallbackContext context) {
        abilityInventoryActive = !abilityInventoryActive;
        EnableInventory(abilityInventoryActive);
       // abilitySelection.SetActive(abilityInventoryActive);
    }

    // Programmaticaly triggered inventory - for scene transitions
    public void OnAbilityInventory() {
        abilityInventoryActive = !abilityInventoryActive;
        EnableInventory(abilityInventoryActive);
    }

    public void EnableInventory(bool active) {
        abilitySelection.SetActive(active);
        abilityManager.SetManagerActive(active);
    }

}