using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopUILogic : MonoBehaviour
{

    private float timer = 0f;

    public GameObject shopMenu; 

    public GameObject cannotPurchaseTag;

    public GameObject purchaseSuccessTag;

    public InputActionAsset inputActionAsset;

    private InputActionMap actionMap;

    private InputAction actionKey;

    // Start is called before the first frame update
    void Start()
    {
        actionMap = inputActionAsset.FindActionMap("Player");
        actionKey = actionMap.FindAction("Interact");
        cannotPurchaseTag.SetActive(false);
        purchaseSuccessTag.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (shopMenu.activeSelf){
            Time.timeScale = 0;
        }
        if (purchaseSuccessTag.activeSelf){
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                timer = 0f;
                DeactivatePurchaseSuccessTag();
            }
        }

        if (cannotPurchaseTag.activeSelf){
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                timer = 0f;
                DeactivateCannotPurchaseTag();
            }
        }
    }

    public void Exit(){
        shopMenu.SetActive(false);
        Time.timeScale = 1;
        actionMap.Enable();
    }

    private void DeactivateCannotPurchaseTag()
    {
        cannotPurchaseTag.SetActive(false);
    }

    private void DeactivatePurchaseSuccessTag()
    {
        purchaseSuccessTag.SetActive(false);
    }
}
