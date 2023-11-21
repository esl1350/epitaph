using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ConsumableFormatter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected TextMeshProUGUI tmp;
    private RosaryBeadsConsumable consumable;

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
       tmp = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
       tmp.enabled = false;
    }

    public void UpdateText() {
        string descriptions = System.String.Join(',', consumable.GetConsumableDescriptions());
        tmp.SetText("<b>" + consumable.cName + "</b><br>" + "<b>Cooldown:</b><br>" + consumable.cooldownTime + "<br>" + descriptions);
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
