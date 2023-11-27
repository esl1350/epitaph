using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AbilitySelection : MonoBehaviour
{

    [SerializeField] private GameObject root;

    private SlotHolder<AbilityWrapper> choiceSlots;
    public SlotHolder<AbilityWrapper> Slots { get { return choiceSlots; } } 

    [SerializeField] private List<AbilityWrapper> startingAbilityChoices;
    private List<AbilityWrapper> abilityChoices = null;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (abilityChoices == null)
            // Copying because unity does NOT like serialized fields changing as much as this one does
            abilityChoices = new List<AbilityWrapper>(startingAbilityChoices);

        ShowAbilityChoice();

        for (int i = 0; i < 3; i++) {
            if (abilityChoices.Count > 0) {
                int randomIndex = Random.Range(0, abilityChoices.Count);
                choiceSlots[i].Item = abilityChoices[randomIndex];
                choiceSlots[i].formatter.Ability = abilityChoices[randomIndex];
                abilityChoices.RemoveAt(randomIndex);
            }
        }

        
    }
    void Awake()
    {
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
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void setAbilityChoices(List<AbilityWrapper> abilityChoices) {
        this.abilityChoices = abilityChoices;
    }

}
