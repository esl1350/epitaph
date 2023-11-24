using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopUILogic : MonoBehaviour
{

    public GameObject shopMenu; 

    public InputActionAsset inputActionAsset;

    private InputActionMap actionMap;

    private InputAction actionKey;

    // Start is called before the first frame update
    void Start()
    {
        actionMap = inputActionAsset.FindActionMap("Player");
        actionKey = actionMap.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        if (shopMenu.activeSelf){
            Time.timeScale = 0;
        }
    }

    public void Exit(){
        shopMenu.SetActive(false);
        Time.timeScale = 1;
        actionMap.Enable();
    }
}
