using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatusEffectManager))]
public class Entity : MonoBehaviour 
{
    [SerializeField] private EntityStats _entityStats;
    public EntityStats EntityStats => _entityStats;
    [SerializeField] private float intialHealth;
    private Health _health;
    public float HealthVal => _health.health;
    public Health Health => _health;

    private StatusEffectManager _statusEffectManager;
    public StatusEffectManager StatusEffectManager => _statusEffectManager;

    [SerializeField] private float knockbackDelay;
    [SerializeField] private float knockbackForce;

    private float currentHealth;
    private float movementSpeed;
    private Rigidbody2D body;

    [SerializeField] private float attack;

    private bool _isDead;


    public float Attack {
        get { return attack; }
        set { attack = value; }
    }

    private void Awake() {
        _health = new(this, intialHealth);
        _statusEffectManager = gameObject.GetComponent<StatusEffectManager>();
        _statusEffectManager.entity = this;
    }

    protected virtual void Start()
    {
        //override in child classes
        body = this.gameObject.GetComponent<Rigidbody2D>();
    }

    public virtual void Die() {
        if(_isDead) return;
        //override in child classes
        Debug.Log("dead");
    }

    public virtual void TakeDamage(float amount)
    {
        _health.TakeDamage(amount);
    }
    
    public virtual void DealDamage(Entity target, float dmgAmt) {
        target.TakeDamage(dmgAmt);
    }


    public void Knockback(GameObject applier) {
        Debug.Log("KNOCK");
        StopAllCoroutines();
        Vector2 direction = (transform.position - applier.transform.position).normalized;
        body.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(ResetKnockBack());
    }

    private IEnumerator ResetKnockBack() {
        yield return new WaitForSeconds(knockbackDelay);
        body.velocity = Vector3.zero;
    }

}
