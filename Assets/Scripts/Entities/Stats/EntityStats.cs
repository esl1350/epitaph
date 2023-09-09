using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//C: Put any stats you want on every entity here. These are the stats that can be modified by status effects
[System.Serializable]
public class EntityStats
{
    [SerializeField] private List<ModifiableStat> _stats = new()
    {
        new(StatEnum.WALKSPEED, 5),
    };

    public ModifiableStat GetStat(StatEnum stat)
    {
        return _stats.Where(s => s.statName == stat).FirstOrDefault();
    }

    public float GetStatValue(StatEnum stat)
    {
        ModifiableStat s = _stats.Where(s => s.statName == stat).First();
        return s.GetStatValue();
    }
}
