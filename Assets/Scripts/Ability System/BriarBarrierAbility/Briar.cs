using UnityEngine;

class Briar : Projectile {

    // Do nothing because parent does the circling math
    [SerializeField]
    private float knockBackDuration; 
    protected override void Update() {}
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy") {
            Entity target = other.GetComponent<Entity>();
            parent.transform.parent.GetComponent<Entity>().DealDamage(target, damage);
            var kb = other.GetComponent<Knockback>();
            kb?.KnockbackCustomForce(transform.parent.gameObject, force, knockBackDuration);
        }
        if (other.gameObject.layer == 9) {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }



}