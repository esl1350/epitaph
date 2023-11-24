using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AugmentState { INACTIVE, ACTIVE, EXPIRED }

public class AugmentInstance {

    private readonly string VFX_PREFAB_PATH = "AugmentVFX";
    public Augment instance;
    private AugmentType Type => instance.AugmentType;
    public AugmentState AugmentState { get; private set; } = AugmentState.INACTIVE;

    private IEnumerator _associatedCoroutine = null;

    private GameObject _vfxObject = null;
    private float timeElapsed = 0f;

    public void Enable(GameObject parent) {
        if (!parent.TryGetComponent<AugmentManager>(out var manager)) {
            Debug.LogError("[AugmentInstance.cs] Tried to enable augment on entity without manager");
            return;
        }
        Enable(parent, manager);
    }
    public void Enable(GameObject parent, AugmentManager augmentManager) {
        if (AugmentState != AugmentState.INACTIVE) return;
        AugmentState = AugmentState.ACTIVE;

        // Instantiate vfx object
        Object pPrefab = Resources.Load(VFX_PREFAB_PATH);
        _vfxObject = (GameObject) Object.Instantiate(pPrefab, parent.transform);

        // Pass vfx object to Scriptable Object and maybe start coroutine
        _associatedCoroutine = instance._Enable(parent, _vfxObject);
        if (_associatedCoroutine != null)
            augmentManager.StartCoroutine(_associatedCoroutine);

        if (instance.statModifiers.Count > 0 && parent.TryGetComponent<StatusEffectManager>(out var sfxManager)) {
            sfxManager.AddModifiers(instance.statModifiers);
        }
    }

    public void Disable(GameObject parent) {
        if (!parent.TryGetComponent<AugmentManager>(out var manager)) {
            Debug.LogError("[AugmentInstance.cs] Tried to enable augment on entity without manager");
            return;
        }

        Disable(parent, manager);
    }

    public void Disable(GameObject parent, AugmentManager augmentManager) {
        if (AugmentState != AugmentState.ACTIVE) return;

        // Stop associated coroutine if there is one
        if (_associatedCoroutine != null)
            augmentManager.StopCoroutine(_associatedCoroutine);

        if (instance.statModifiers.Count > 0 && parent.TryGetComponent<StatusEffectManager>(out var sfxManager)) {
            sfxManager.RemoveModifiers(instance.statModifiers);
        }

        // Pass arguments to disable
        instance._Disable(parent, _vfxObject);

        

        AugmentState = AugmentState.INACTIVE;
    }

    public float ApplyAugmentDamageTaken(float damageTaken, Entity source, Entity target) {   
        if (AugmentState == AugmentState.ACTIVE)              
            return instance._ApplyAugmentDamageTaken(damageTaken, source, target);
        return 0;
    }
    public float ApplyAugmentDamageDealt(float damageDealt, Entity source, Entity target, HashSet<AbilityTag> tags) {
        if (AugmentState == AugmentState.ACTIVE)
            return instance._ApplyAugmentDamageDealt(damageDealt, source, target, tags);
        return 0;
    }

    public void Expire(GameObject parent) {
        if (instance.expires == false) return;
        if (AugmentState == AugmentState.ACTIVE) Disable(parent);
        AugmentState = AugmentState.EXPIRED;
    }

    public void Tick(float deltaTime, GameObject parent) {
        if (AugmentState != AugmentState.ACTIVE) return;

        timeElapsed += deltaTime;
        if (instance.expires && instance.expirationTime < timeElapsed) {
            Expire(parent);
        }
    }

    public bool Equals(Augment obj)
    {
        return instance == obj;
    }

}