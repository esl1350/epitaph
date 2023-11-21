using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class AbilityFormatter : TooltipFormatter
{
    public override void UpdateText() {
        tmp.SetText("<b>" + Ability.ActiveAbility.aName + "</b><br>" + "<b>Cooldown:</b>" + Ability.ActiveAbility.cooldownTime + "<br>" + Ability.GetWrappedDescription());
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        if (Ability != null) {
            UpdateText();
            tmp.enabled = true;
        }
    }
}
