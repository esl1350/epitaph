using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectInstance
{
    public StatusEffect statusEffect;

    public float expirationTime;
    public float nextTickTime;

    public bool HasExpired() => statusEffect.expirableType == ExpirableType.Expirable && Time.time > expirationTime;
}
