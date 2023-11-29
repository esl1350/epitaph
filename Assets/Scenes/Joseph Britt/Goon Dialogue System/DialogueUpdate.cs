using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Shows dialogue approximately once every [averageDelayBetweenDialogue] seconds for [displayDuration] seconds.
/// </summary>
public class DialogueUpdate : MonoBehaviour
{
    [SerializeField] private float displayDuration = 3f;
    [SerializeField] private float minimumWait = 30f;
    [SerializeField] private float averageWait = 60f;
    [SerializeField] private List<string> dialogueList;
    [SerializeField] private TextMeshProUGUI tmp;
    private float timeDisplayed;
    private float timeBetweenDialogue;
    private int lastIndex;

    // Start is called before the first frame update
    void Start()
    {
        tmp = this.GetComponent<TextMeshProUGUI>();
        tmp.SetText("");
        timeDisplayed = 0f;
        timeBetweenDialogue = 0f;
        lastIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {
        // guard for empty list
        if (dialogueList.Count == 0) {
            return;
        }
        if (tmp == null) {
            tmp = GameObject.Find("Dialogue").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        }

        if (tmp.text.Equals("")) {
            timeBetweenDialogue += Time.deltaTime;
            if (Random.Range(0f, averageWait) < Time.deltaTime && timeBetweenDialogue > minimumWait) {
                // pick different dialogue than last time
                while (true) {
                    int newIndex = Random.Range(0, dialogueList.Count);
                    if (newIndex != lastIndex || dialogueList.Count <= 1) {
                        lastIndex = newIndex;
                        break;
                    }
                }
                tmp.SetText(dialogueList[lastIndex]);
                timeBetweenDialogue = 0f;
            }
        } else {
            timeDisplayed += Time.deltaTime;
            if (timeDisplayed > displayDuration) {
                tmp.SetText("");
                timeDisplayed = 0f;
            }
        }
    }
}
