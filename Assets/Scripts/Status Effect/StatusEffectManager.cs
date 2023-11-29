using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StatusEffectManager : MonoBehaviour
{
    private List<StatusEffectInstance> _statusEffects = new List<StatusEffectInstance>();
    private List<StatusEffectCode> _statusEffectTypes = new List<StatusEffectCode>();

    public Entity entity;
    private EntityStats _stats;
    private Health _health;
    private SpriteRenderer _renderer;
    private float _currTime;

    private void Start() {
        if(entity == null) 
        {
            Debug.LogError("Status Effect manager applied to an object with no entity class. This should not happen");
        }
        _health = entity.Health;
        _stats = entity.EntityStats;
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        _currTime = Time.time;
        TickStatusEffects();
        RemoveExpiredEffects();
    }


    private void TickStatusEffects()
    {
        for (int i = 0; i < _statusEffects.Count; i++) {
            StatusEffectInstance se = _statusEffects[i];
            if (se.statusEffect.tickStatusEffect != null) {   
                if(_currTime > se.nextTickTime) {
                    se.nextTickTime = _currTime + se.statusEffect.tickTime;
                    AddModifiers(se.statusEffect.tickStatusEffect);
                }
            }
        }
    }

    
    private void RemoveExpiredEffects()
    {
        List<StatusEffectInstance> itemsToRemove = new();
        foreach(StatusEffectInstance se in _statusEffects)
        {
            if(se.HasExpired()) 
            {
                if (se.statusEffect.effectCode == StatusEffectCode.BURN) {
                    var particles = transform.Find("FireParticleManager").gameObject;
                    if (_renderer != null) {
                        _renderer.color = Color.white;
                    }
                    particles?.SetActive(false);
                }
                AddModifiers(se.statusEffect.exitStatusEffect);
                itemsToRemove.Add(se);
            }
        }
        foreach(StatusEffectInstance se in itemsToRemove)
        {
            RemoveExpiredEffect(se);
        }
    }

    public void ApplyEffect(StatusEffect statusEffect) => ApplyEffects(new List<StatusEffect>() { statusEffect});

    public void ApplyEffects(List<StatusEffect> statusEffects)
    {
        foreach(StatusEffect se in statusEffects)
        {
            StatusEffectInstance effect = new StatusEffectInstance
            {
                statusEffect = se
            };
            if (se.effectCode == StatusEffectCode.BURN) {
                var particles = transform.Find("FireParticleManager").gameObject;
                if (_renderer != null) {
                    _renderer.color = new Color(0.72f/255.0f, 0.72f/255.0f, 0.72f/255.0f, 1.0f);
                }
                particles?.SetActive(true);
            }
            AddEffect(effect);
        }
    }

    private void AddEffect(StatusEffectInstance effect)
    {
        //C TODO: how do we want to handle effects that already exist?
        //For now, I am assuming each effect code is only used once
        StatusEffect se = effect.statusEffect;

        if(_statusEffectTypes.Contains(se.effectCode)) {
            StatusEffectInstance currEffect = _statusEffects.Where( s => s.statusEffect.effectCode == se.effectCode).First();
            if(effect.expirationTime > currEffect.expirationTime)
            {
                currEffect.expirationTime = effect.expirationTime;
            }
        } 
        else {
            _statusEffects.Add(effect);
            _statusEffectTypes.Add(se.effectCode);
            effect.expirationTime = _currTime + se.duration;
            effect.nextTickTime = _currTime + se.tickTime;
            AddModifiers(se.entryStatusEffect);
            AddModifiers(se.passiveStatusEffect);
        }
    }

    public void AddModifiers(List<StatModifier> statModifiers)
    {
        foreach(StatModifier sm in statModifiers)
            AddModifier(sm);
    }

    private void AddModifier(StatModifier statModifier)
    {
        StatEnum statEnum = statModifier.stat;
        if(statEnum == StatEnum.HEALTH)
        {
            _health.AddModifier(statModifier);
        }
        else
        {
            ModifiableStat stat = _stats.GetStat(statEnum);
            stat.AddModifier(statModifier);
        }

    }

    private void RemoveExpiredEffect(StatusEffectInstance effect)
    {
        StatusEffect se = effect.statusEffect;
        if(se.removableType == RemovableType.NonRemovable) return;

        RemoveModifiers(se.entryStatusEffect);
        RemoveModifiers(se.passiveStatusEffect);
        RemoveModifiers(se.tickStatusEffect);
        RemoveModifiers(se.exitStatusEffect);

        _statusEffects.Remove(effect);
        _statusEffectTypes.Remove(se.effectCode);
    }

    public void RemoveModifiers(List<StatModifier> statModifiers)
    {
        foreach(StatModifier sm in statModifiers)
            RemoveModifier(sm);
    }

    private void RemoveModifier(StatModifier statModifier)
    {
        StatEnum statEnum = statModifier.stat;
        if(statEnum == StatEnum.HEALTH)
        {
            _health.RemoveModifier(statModifier);
        }
        else
        {
            ModifiableStat stat = _stats.GetStat(statEnum);
            stat.RemoveModifier(statModifier);
        }
    }

}
