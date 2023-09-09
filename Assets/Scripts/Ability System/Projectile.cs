using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    protected Rigidbody2D rb;
    protected Vector3 direction;
    public float force;
    [SerializeField] List<StatusEffect> _statusEffects;
    [SerializeField] private float projectileTimer;
    public GameObject parent;

    // increase sprite size
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        if (projectileTimer > 0)
        {
            projectileTimer -= Time.deltaTime;
            return;
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var statusEffectManager = other.GetComponent<StatusEffectManager>();
            statusEffectManager?.ApplyEffects(_statusEffects);
        
        //Debug.Log($"Hit {other.gameObject.name}");

        Destroy(this.gameObject);
    }
}
