using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class DashFormatter : TooltipFormatter
{
    public override void UpdateText() {
        tmp.SetText("<b>" + Ability.ActiveAbility.aName + "</b><br>" + "<b>Cooldown:</b><br>" + Ability.ActiveAbility.cooldownTime + "<br>" + Ability.Description);
    }
}
