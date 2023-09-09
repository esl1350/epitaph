using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class AbilityHolder : MonoBehaviour
{
    [Header("Ability 1")]
    private Ability ability1;
    private Image ability1Img;
    
    [Header("Ability 2")]
    private Ability ability2;
    private Image ability2Img;

    [Header("Ability 3")]
    private Ability ability3;
    private Image ability3Img;

    [Header("Dash Ability")]
    private Ability dashAbility;
    private Image dashAbilityImage;

    GameObject parent;

    [SerializeField] private GameObject abilitySelection;
    [SerializeField] private AbilityInventoryManager abilityManager;
    private bool abilityInventoryActive;

    // Start is called before the first frame update
    void Start()
    {
        // if (abilityManager.GetHotbarAbilities()[0].GetAbility() != null) {
        //     ability1 = abilityManager.GetHotbarAbilities()[0].GetAbility();
        //     ability1Img = abilityManager.GetHotbarSlots()[0].GetComponent<Image>();
        // }

        // if (abilityManager.GetHotbarAbilities()[1].GetAbility() != null) {
        //     ability2 = abilityManager.GetHotbarAbilities()[1].GetAbility();
        //     ability2Img = abilityManager.GetHotbarSlots()[1].GetComponent<Image>();
        // }

        // if (abilityManager.GetHotbarAbilities()[2].GetAbility() != null) {
        //     ability3 = abilityManager.GetHotbarAbilities()[2].GetAbility();
        //     ability3Img = abilityManager.GetHotbarSlots()[2].GetComponent<Image>();
        // }



        abilityInventoryActive = false;

        // ability1Img.sprite = ability1.aSprite;
        // ability2Img.sprite = ability2.aSprite;
        // ability3Img.sprite = ability3.aSprite;


        // ability1Img.fillAmount = 0;
        // ability2Img.fillAmount = 0;
        // ability3Img.fillAmount = 0;
        
        // ability1.SetState(AbilityState.ready);
        // ability2.SetState(AbilityState.ready);
        // ability3.SetState(AbilityState.ready);
        parent = this.gameObject;

//Gotta handle making sure this happens first time ability is assignec
        // ability1.Init();
        // ability2.Init();
        // ability3.Init();

        abilitySelection.SetActive(abilityInventoryActive);
    }

    // Update is called once per frame
    void Update()
    {  
        if (dashAbility == null) {
            dashAbility = abilityManager.GetHotbarAbilities()[0].GetAbility();
            dashAbilityImage = abilityManager.GetHotbarSlots()[0].GetComponent<Image>();
        }

        if (abilityManager.GetHotbarAbilities()[1].GetAbility() != null) {
            ability1 = abilityManager.GetHotbarAbilities()[1].GetAbility();
            ability1Img = abilityManager.GetHotbarSlots()[1].GetComponent<Image>();

        }

        if (abilityManager.GetHotbarAbilities()[2].GetAbility() != null) {
            ability2 = abilityManager.GetHotbarAbilities()[2].GetAbility();
            ability2Img = abilityManager.GetHotbarSlots()[2].GetComponent<Image>();


        }

        if (abilityManager.GetHotbarAbilities()[3].GetAbility() != null) {
            ability3 = abilityManager.GetHotbarAbilities()[3].GetAbility();
            ability3Img = abilityManager.GetHotbarSlots()[3].GetComponent<Image>();


        }

        if (ability1 != null) {
            ability1.AbilityCooldownHandler(parent);
            ability1.AbilityBehavior(parent);
            ability1Img.fillAmount = ability1.fillAmount;

        }

        if (ability2 != null) {
            ability2.AbilityCooldownHandler(parent);
            ability2.AbilityBehavior(parent);
            ability2Img.fillAmount = ability2.fillAmount;
        }

        if (ability3 != null) {
            ability3.AbilityCooldownHandler(parent);
            ability3.AbilityBehavior(parent);
            ability3Img.fillAmount = ability3.fillAmount;
        }

        if(dashAbility != null) {
            dashAbility.AbilityCooldownHandler(parent);
            dashAbility.AbilityBehavior(parent);
            dashAbilityImage.fillAmount = dashAbility.fillAmount;
        }
    }

    public void OnAbility1(InputAction.CallbackContext context) 
    {
        if (ability1 != null) {
            if (context.started)
            {   
                ability1.SetAbilityPressed(true);
            }
            else if (context.canceled)
            {
                ability1.SetAbilityPressed(false);
            }
        }
    }

    public void OnAbility2(InputAction.CallbackContext context) 
    {
        if (ability2 != null) {
            if (context.started)
            {
                ability2.SetAbilityPressed(true);
            }
            else if (context.canceled)
            {
                ability2.SetAbilityPressed(false);
            }
        }

    }

    public void OnAbility3(InputAction.CallbackContext context) 
    {
        if (ability3 != null) {
            if (context.started)
            {
                ability3.SetAbilityPressed(true);
            }
            else if (context.canceled)
            {
                ability3.SetAbilityPressed(false);
            }
        }

    }

    public void OnDashAbility(InputAction.CallbackContext context) {
        if (dashAbility != null) {
            if (context.started) {
                dashAbility.SetAbilityPressed(true);
            } else if (context.canceled) {
                dashAbility.SetAbilityPressed(false);
            }
        }
    }

    public void OnAbilityInventory(InputAction.CallbackContext context) {
        abilityInventoryActive = !abilityInventoryActive;
        abilitySelection.SetActive(abilityInventoryActive);
    }

}   