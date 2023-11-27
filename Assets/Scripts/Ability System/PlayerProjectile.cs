using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    // Start is called before the first frame update
    private Vector3 mousePos;
    private Camera mainCam;
    [SerializeField] protected Animator _animator;
    protected override void Start()
    {
        _animator = GetComponent<Animator>();

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }
    protected override void OnTriggerEnter2D(Collider2D other) {
        _animator.SetTrigger("Collide");  

        if (other.gameObject.tag == "Enemy") {
            var statusEffectManager = other.GetComponent<StatusEffectManager>();
                statusEffectManager?.ApplyEffects(_statusEffects);
            var entity = other.GetComponent<Entity>();
            if (entity != null) {
                parent.GetComponent<Entity>().DealDamage(entity, damage);
            }
            Debug.Log($"Hit Base {other.gameObject.name}");

        }
        StartCoroutine(CheckAnimationAndDestroy());

        //Destroy(this.gameObject);
    }

    protected IEnumerator CheckAnimationAndDestroy()
    {   
        // Assuming the animation is in the first layer, index 0
        yield return null;

        // Wait while the animator has animations to play
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f || _animator.IsInTransition(0))
        {
            yield return null;
        }
        // Destroy the GameObject after the animation is complete
        Destroy(gameObject);
    }

}
