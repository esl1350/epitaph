using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TooltipFormatter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected TextMeshProUGUI tmp;
    private AbilityWrapper ability;

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
       tmp = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
       tmp.enabled = false;
    }

    public virtual void UpdateText() {
    
    }

    public virtual void OnPointerEnter(PointerEventData eventData) {
        UpdateText();
        tmp.enabled = true;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        tmp.enabled = false;
    }
}
