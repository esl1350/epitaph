using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TooltipFormatter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected TextMeshProUGUI tmp;
    private AbilityWrapper ability;
    protected Image tooltipImg;
    public AbilityWrapper Ability {
        get {
            return ability;
        }
        set {
            ability = value;
        }
    }
    // Start is called before the first frame update
    void Start() {
       tmp = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
       tmp.enabled = false;
       tooltipImg = this.transform.GetChild(1).GetComponent<Image>();
       tooltipImg.enabled = false;
    }

    public virtual void UpdateText() {

    }

    public virtual void OnPointerEnter(PointerEventData eventData) {
        UpdateText();
        tmp.enabled = true;
        tooltipImg.enabled = true;

    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        tmp.enabled = false;
        tooltipImg.enabled = false;

    }

    /**
    * sometimes we want to disable tooltips manually
    * such as when we click on an ability to choose it from the selection screen
    */
    public void Disable()
    {
        tmp.enabled = false;
    }
}