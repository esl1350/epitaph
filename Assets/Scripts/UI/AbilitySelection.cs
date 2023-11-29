using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AbilitySelection : MonoBehaviour
{

    [SerializeField] private GameObject root;
    public static AbilitySelection Instance { get; private set; }

    private SlotHolder<AbilityWrapper> choiceSlots;
    public SlotHolder<AbilityWrapper> Slots { get { return choiceSlots; } } 

    [SerializeField] private List<AbilityWrapper> startingAbilityChoices;
    private List<AbilityWrapper> abilityChoices;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            if (root == null) {
                root = GameObject.Find("NewAbilityUI").transform.GetChild(1).gameObject;
                choiceSlots = new SlotHolder<AbilityWrapper>(root);
                abilityChoices = new List<AbilityWrapper>(startingAbilityChoices);
            }

            // if (this == null) {
            //     this = GameObject.Find("AbilitySelection").transform.gameObject.GetComponent<AbilitySelection>();
            // }
        }

        if (abilityChoices == null)
            // Copying because unity does NOT like serialized fields changing as much as this one does
            abilityChoices = new List<AbilityWrapper>(startingAbilityChoices);


        for (int i = 0; i < 3; i++) {
            if (abilityChoices.Count > 0) {
                int randomIndex = Random.Range(0, abilityChoices.Count);
                choiceSlots[i].Item = abilityChoices[randomIndex];
                choiceSlots[i].formatter.Ability = abilityChoices[randomIndex];
                abilityChoices.RemoveAt(randomIndex);
            }
        }

        ShowAbilityChoice();

    }
    
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        choiceSlots = new SlotHolder<AbilityWrapper>(root);
    }

    public void HideAbilityChoice() {
        // Send abilities that were not chosen back into pool
        ReplaceUnchosenAbilities();

        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ReplaceUnchosenAbilities() {
        foreach (Slot<AbilityWrapper> slot in choiceSlots) {
            if (slot.Item != null) {
                abilityChoices.Add(slot.Item);
                slot.Item = null;
                slot.formatter.Ability = null;
            }
        }
    }

    public void ShowAbilityChoice() {
        Instance.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void setAbilityChoices(List<AbilityWrapper> abilityChoices) {
        this.abilityChoices = abilityChoices;
    }

}
