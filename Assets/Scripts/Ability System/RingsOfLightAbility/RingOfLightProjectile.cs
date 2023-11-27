using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

class RingOfLightProjectile : Projectile {

    public Augment augmentToApply;

    public new Vector3 direction;

    private SpriteRenderer ringSprite;

        private bool outwards = true;
    private bool markDestroy = false;

    [SerializeField]
    private float returnForce;
    [SerializeField]
    private float returnForceModifier;

    [SerializeField] 
    private float rotation;

    private new Transform particleSystem;

    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * force;

        ringSprite = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        ringSprite.transform.Rotate(0, 0, rotation);


        Vector2 tow_parent = (parent.transform.position - transform.position).normalized;

        rb.velocity += tow_parent * returnForce;
        
        if (Vector3.Dot(rb.velocity, tow_parent) > 0) {
            rb.velocity = Vector3.RotateTowards(rb.velocity, tow_parent, 0.2f, 0);
        }

        returnForce += returnForceModifier * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // Prevent rings from disappearing immediately after instantiation
        if (other.tag == "Player") {
            if (!outwards) DestroyPreserveParticleSystem();
            else outwards = false;
        }
        if (markDestroy) return;
        if (other.tag == "Enemy") {
            Entity target = other.GetComponent<Entity>();
            parent.GetComponent<Entity>().DealDamage(target, damage);
            AugmentManager targetAugmentManager = other.GetComponent<AugmentManager>();
            if (!targetAugmentManager) return;

            targetAugmentManager.AddAugment(augmentToApply);
        }
    }

    /*
    *   We want to destroy the projectile AFTER it crosses the player in order to keep trails clean, but we don't want to
    *   make it seem like the projectile was destroyed before.
    *
    */
    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            if (!markDestroy) return;
            particleSystem.GetComponent<ParticleSystem>().Stop();
            Destroy(gameObject);
        }
    }

    /*
    * In place to prevent trails from disappearing out of nowhere when the projectile is destroyed
    * Particle system is orphaned and set to be destroyed a couple seconds later just to keep the vfx clean
    */
    private void DestroyPreserveParticleSystem() {
        particleSystem = transform.GetChild(1);
        particleSystem.parent = null;
        Destroy(particleSystem.gameObject, 1f);
        markDestroy = true;
        ringSprite.enabled = false;
    }

}