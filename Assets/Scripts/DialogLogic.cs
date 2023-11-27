using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class DialogLogic : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameComponent;

    public GameObject arrow;

    private GameObject target;

    public Image characterComponenet;
    public string[] lines;
    public string[] namesPerLine;
    public List<Sprite> Sprites;
    public float textSpeed;
    private int index;

    UIAnimator UIAnimatorScript;

    StartConvo StartConvoScript;

    public InputActionAsset inputActionAsset;

    private InputActionMap actionMap;
    
    // Start is called before the first frame update
    void Start()
    {
        actionMap = inputActionAsset.FindActionMap("Player");
        arrow.SetActive(false);
        UIAnimatorScript = arrow.GetComponent<UIAnimator>();
        gameObject.SetActive(false);
        target = GameObject.FindWithTag("Player");
        textComponent.text = string.Empty;
        nameComponent.text = string.Empty;
        StartConvoScript = GameObject.FindGameObjectWithTag("NPC").GetComponent<StartConvo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            Rigidbody2D playerRigid = target.GetComponent<Rigidbody2D>();
            playerRigid.velocity = Vector2.zero;
            PlayerController playerController = target.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.enabled = false;
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            arrow.SetActive(true);
            if (textComponent.text == lines[index]){
                nextLine();
            }
            else{
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialog(){
        index=0;
        gameObject.SetActive(true);
        StartCoroutine(TypeLine());
        nameComponent.text = namesPerLine[index];
        characterComponenet.sprite = Sprites[index];
    }

    IEnumerator TypeLine(){
        // type one by one
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text +=c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void nextLine(){
        UIAnimatorScript.Func_PlayUIAnim();
        arrow.SetActive(false);
        if (index < lines.Length-1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
            nameComponent.text = namesPerLine[index];
            characterComponenet.sprite = Sprites[index];
        }
        else
        {
            gameObject.SetActive(false);
            textComponent.text = string.Empty;
            nameComponent.text = string.Empty;
            StartConvoScript.EnableKey();
            actionMap.Enable();
            target.GetComponent<PlayerController>().enabled = true;
            UIAnimatorScript.Func_StopUIAnim();
        }
    }
}
