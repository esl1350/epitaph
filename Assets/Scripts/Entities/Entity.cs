using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatusEffectManager))]
[RequireComponent(typeof(AugmentManager))]
public class Entity : MonoBehaviour 
{
    [SerializeField] private EntityStats _entityStats;
    public EntityStats EntityStats => _entityStats;
    [SerializeField] protected float intialHealth;
    [SerializeField] protected Animator animator;

    protected Health _health;
    public float HealthVal => _health.health;
    public Health Health => _health;

    private StatusEffectManager _statusEffectManager;
    public StatusEffectManager StatusEffectManager => _statusEffectManager;

    private Rigidbody2D body;

    private bool _isDead;
    private Shader GUIWhite;
    private Shader defaultSpriteShader;

    // need a reference to this to adjust damage dealt and taken
    [SerializeField]
    private AugmentManager _augmentManager;

    private void Awake() {
        _health = new(this, intialHealth);
        _statusEffectManager = gameObject.GetComponent<StatusEffectManager>();
        _statusEffectManager.entity = this;
        _augmentManager = gameObject.GetComponent<AugmentManager>();
        _augmentManager.setCurrent(this);
    }

    public IEnumerator DamageFlash() {
        GetComponent<Renderer>().material.shader = GUIWhite;
        yield return new WaitForSeconds(0.12f);
        GetComponent<Renderer>().material.shader = defaultSpriteShader;
    }

    protected virtual void Start()
    {
		GUIWhite = Shader.Find("GUI/Text Shader");
		defaultSpriteShader = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
        //override in child classes
        body = this.gameObject.GetComponent<Rigidbody2D>();
    }

    public virtual void Die() {
        if(_isDead) return;
        //override in child classes
    }
    // relaying data to augment manager
    public virtual void TakeDamage(float amount)
    {
        if (this.enabled) {
            Health.TakeDamage(amount);
            _augmentManager.updateDamageTaken(amount);
            StartCoroutine(DamageFlash());
        }
    }

    // relaying data to augment manager
    public void DealDamage(Entity target, float dmgAmt)
    {
        DealDamage(target, dmgAmt, new HashSet<AbilityTag>());
    }

    // relaying data to augment manager with tag
    public void DealDamage(Entity target, float dmgAmt, HashSet<AbilityTag> tags)
    {
        target.TakeDamage(dmgAmt);
        _augmentManager.updateDamageDealt(target, dmgAmt, tags);
    }

    // handle augmented damage taken after initial
    public void TakeDamageAugmented(float amount)
    {
        Health.TakeDamage(amount);
    }

    // handle augmented damage dealt after initial
    public void DealDamageAugmented(Entity target, float dmgAmt)
    {
        target.TakeDamage(dmgAmt);
    }

}
    