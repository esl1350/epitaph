using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

// Updates number of charges on the slot
// Updates image on the slot
public class ConsumableHolder : MonoBehaviour
{
    [SerializeField]
    private RosaryBeadsConsumable rosaryBeadsConsumable;
    // private Image beadsConsumableImg;
    [SerializeField]
    private GameObject consumableSlot;

    // add text for number of charges
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.gameObject;
        rosaryBeadsConsumable.Init();
        consumableSlot.GetComponent<ConsumableFormatter>().Consumable = rosaryBeadsConsumable;
    }

    // Update is called once per frame
    void Update()
    {
        rosaryBeadsConsumable.ConsumableCooldownHandler(parent);
        rosaryBeadsConsumable.ConsumableBehavior(parent.GetComponent<Player>());
        // beadsConsumableImg.fillAmount = beadsConsumable.fillAmount;
        // update counter of charges on hotbar
    }

    public void OnConsumable(InputAction.CallbackContext context) 
    {
        if (context.started) {   
            rosaryBeadsConsumable.SetConsumablePressed(true);
            if (!rosaryBeadsConsumable.hasCharges()) {
                Debug.Log("Out of consumable charges");
            }
        }
        else if (context.canceled) {
            rosaryBeadsConsumable.SetConsumablePressed(false);
        }
    }

}   