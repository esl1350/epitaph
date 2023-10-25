using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartConvo : MonoBehaviour
{
    public float distanceAway = 3f;

    private GameObject target;

    public TextMeshProUGUI textComponent;

    private GameObject textbox;

    public string[] convoLines;
    public string[] convoLinesNames;

    public List<Sprite> allSprites;

    DialogLogic DialogLogicScript;
    private bool isKeyEnabled = true;

    private GameObject DialogBox;

    private Rigidbody2D rb;

    private string key = "e";

    // Start is called before the first frame update
    void Start()
    {
        textbox = GameObject.FindGameObjectWithTag("DialogBox");
        //textComponent = this.GetComponent<TextMeshProUGUI>();
        target = GameObject.FindWithTag("Player");
        rb = target.GetComponent<Rigidbody2D>();
        textComponent.text = string.Empty;
        DialogBox = GameObject.FindGameObjectWithTag("DialogBox");
        DialogLogicScript = GameObject.FindGameObjectWithTag("DialogBox").GetComponent<DialogLogic>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     float dist = Vector3.Distance(transform.position, target.transform.position);
    //     if (dist < distanceAway)
    //     {
    //         textComponent.text ="Press E to Interact";
    //         if (isKeyEnabled)
    //             {
    //                 if (Input.GetKeyDown("e"))
    //                 {
    //                     DisableKey();
    //                     Debug.Log("started convo");
    //                     textbox.SetActive(true);

    //                     //change the list dialog within the DialogLogic Script to match this dialog stated in this script
    //                     DialogLogicScript.lines = convoLines;
    //                     DialogLogicScript.namesPerLine = convoLinesNames;
    //                     DialogLogicScript.Sprites = allSprites;
    //                     DialogLogicScript.StartDialog();
    //                 }
    //             }
    //     }
    //     else{
    //         textComponent.text = string.Empty;
    //     }
    // }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("triggered NPC");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("started convo");
            textbox.SetActive(true);

            //change the list dialog within the DialogLogic Script to match this dialog stated in this script
            DialogLogicScript.lines = convoLines;
            DialogLogicScript.namesPerLine = convoLinesNames;
            DialogLogicScript.Sprites = allSprites;
            DialogLogicScript.StartDialog();
        }
    }

    public void DisableKey()
    {
        isKeyEnabled = false;
    }

    // Call this method to enable the key
    public void EnableKey()
    {
        isKeyEnabled = true;
    }
    
}