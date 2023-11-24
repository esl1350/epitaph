using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class OpenShop : MonoBehaviour
{
    public float distanceAway = 3f;

    private GameObject target;

    private bool isActionKeyPressed = false;

    public TextMeshProUGUI textComponent;

    public GameObject ShopUI;
    private bool isKeyEnabled = true;

    public bool useHitboxToInteract = true;

    private Rigidbody2D rb;

    public InputActionAsset inputActionAsset;

    private InputActionMap actionMap;

    private InputAction actionKey;
    // Start is called before the first frame update
    void Start()
    {
        actionMap = inputActionAsset.FindActionMap("Player");
        actionKey = actionMap.FindAction("Interact");
        target = GameObject.FindWithTag("Player");
        rb = target.GetComponent<Rigidbody2D>();
        actionKey.started += OnActionKeyStarted;
        actionKey.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (useHitboxToInteract != true){
            if (dist < distanceAway)
            {
                textComponent.text ="Press "+actionKey.GetBindingDisplayString(0)+" to Interact";
                if (isKeyEnabled)
                    {
                        if (isActionKeyPressed)
                        {
                            isActionKeyPressed = false;
                            actionMap.Disable();
                            ShopUI.SetActive(true);
                        }
                    }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (useHitboxToInteract){
            if (other.gameObject.tag == "Player")
            {
                ShopUI.SetActive(true);
                actionMap.Disable();
                //change the list dialog within the DialogLogic Script to match this dialog stated in this script
            }
        }
    }

    private void OnActionKeyStarted(InputAction.CallbackContext context)
    {
        // Set the boolean variable to true when the action key is pressed
        isActionKeyPressed = true;
    }
}
