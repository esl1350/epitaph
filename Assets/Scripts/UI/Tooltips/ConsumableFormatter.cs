using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsumableFormatter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected TextMeshProUGUI tmp;
    private RosaryBeadsConsumable consumable;
    private Image tooltipImg;
    private Animator anim;
    private RectTransform tooltipTransform;
    public RosaryBeadsConsumable Consumable {
        get {
            return consumable;
        }
        set {
            consumable = value;
        }
    }
    // Start is called before the first frame update
    void Start() {
       anim = GetComponent<Animator>();
       tooltipImg = this.transform.GetChild(0).GetComponent<Image>();
       tooltipTransform = this.transform.GetChild(0).GetComponent<RectTransform>();
       tmp = tooltipImg.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
       tmp.enabled = false;
       tooltipImg.enabled = false;
       StartCoroutine(SetBoxSize());
    }

    IEnumerator SetBoxSize() {
        yield return new WaitForSeconds(0.01f);
        float boxHeight = tooltipTransform.sizeDelta.y;
        tooltipTransform.anchoredPosition = new Vector3(tooltipTransform.anchoredPosition.x, boxHeight + 10, 0);
    }

    void Update() {
        if (consumable != null) {
            anim.SetFloat("charge percent", ((float)consumable.currentCharges)/((float)consumable.startingCharges));
        }
    }

    public void UpdateText() {
        string descriptions = "";
        if (consumable.GetConsumableDescriptions().Count > 0) {
            descriptions = System.String.Join(',', consumable.GetConsumableDescriptions());
        }
        tmp.SetText("<b>" + consumable.cName + "</b><br>" + "<b>Charges:</b><br>" + consumable.currentCharges + "<br>" + descriptions);
       StartCoroutine(SetBoxSize());
    }

    public virtual void OnPointerEnter(PointerEventData eventData) {
        UpdateText();
        tooltipImg.enabled = true;
        tmp.enabled = true;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        tooltipImg.enabled = false;
        tmp.enabled = false;
    }
}