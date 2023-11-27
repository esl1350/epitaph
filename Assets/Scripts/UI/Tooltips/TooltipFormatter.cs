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
    protected RectTransform tooltipTransform;
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
       tooltipImg = this.transform.GetChild(1).GetComponent<Image>();
       tooltipTransform = this.transform.GetChild(1).GetComponent<RectTransform>();
       tmp = tooltipImg.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
       tmp.enabled = false;
       tooltipImg.enabled = false;
        StartCoroutine(SetBoxSize());
    }
    public IEnumerator SetBoxSize() {
        yield return new WaitForSeconds(0.01f);
        float boxHeight = tooltipTransform.sizeDelta.y;
        tooltipTransform.anchoredPosition = new Vector3(tooltipTransform.anchoredPosition.x, boxHeight + 10, 0);
    }
    public virtual void UpdateText() {

    }

    public virtual void OnPointerEnter(PointerEventData eventData) {
        UpdateText();
        tmp.enabled = true;
        tooltipImg.enabled = true;
        StartCoroutine(SetBoxSize());

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