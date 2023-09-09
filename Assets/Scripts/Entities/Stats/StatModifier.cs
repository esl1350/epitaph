using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum ApplicationType
{
    NONE,
    SET, //Will be applied last, so will override other multipliers
    ADD,
    ADD_PERCENT,
    MULTIPLY,
    SET_AFFECTABLE, //Will be applied first, so can be affected by other multipliers
};

[System.Serializable]
public class StatModifier: IComparable
{
    public  ApplicationType applicationType;
    public  StatEnum stat;
    public  float amount;
    public  bool beneficial; 

    //If true, the stat will change the base value directly instead of being stored as a temp modifier.
    //ie damage and permanant buffs should change base value, while temporary buffs should not
    public bool changeBaseValue; 
    
    public int CompareTo(object obj)
    {
        if(obj is not StatModifier)
        {
            Debug.LogError("Cannot compare stat modfier to another object type");
            return 0;
        }
        return applicationType.CompareTo(((StatModifier) obj).applicationType);
    }
}

[System.Serializable]
public class Damage: StatModifier
{
    public Damage(float amount)
    {
        applicationType = ApplicationType.ADD;
        stat = StatEnum.HEALTH;
        this.amount = amount * -1;
        beneficial = false;
        changeBaseValue = true;
    }
}

[System.Serializable]
public class Heal: StatModifier
{
    public Heal(float amount)
    {
        applicationType = ApplicationType.ADD;
        stat = StatEnum.HEALTH;
        this.amount = amount;
        beneficial = true;
        changeBaseValue = true;
    }
}
