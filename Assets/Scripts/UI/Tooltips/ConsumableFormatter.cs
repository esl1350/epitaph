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
       tmp = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
       tmp.enabled = false;
       tooltipImg = this.transform.GetChild(0).GetComponent<Image>();
       tooltipImg.enabled = false;
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