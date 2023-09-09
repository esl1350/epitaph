using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RemovableType { Removable, NonRemovable };
public enum ExpirableType { Expirable, NonExpireable };

[CreateAssetMenu(menuName = "StatusEffects/StatusEffect")]
public class StatusEffect : ScriptableObject
{
    public StatusEffectCode effectCode;
    public float tickTime;
    public float duration;

    public RemovableType removableType;
    public ExpirableType expirableType;

    //C: Do not put passive status effects (ie +5 attack damage)
    // on entry, tick, or exit events. It will not work.
    public List<StatModifier> entryStatusEffect = new List<StatModifier>(); //Applied once on entry
    public List<StatModifier> passiveStatusEffect = new List<StatModifier>(); //Applied permanently  
    public List<StatModifier> tickStatusEffect = new List<StatModifier>(); //Applied at every tick
    public List<StatModifier> exitStatusEffect = new List<StatModifier>(); //Applied once on exit
}
