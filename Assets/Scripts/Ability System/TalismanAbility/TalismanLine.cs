using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalismanLine : MonoBehaviour
{
    private List<StatusEffect> _statusEffects;
    private List<StatusEffect> _removeEffects;
    private BoxCollider2D collider;
    private SpriteRenderer renderer;
    private float damage;
    private Player parent;

    public float Damage {
        get {return damage;}
        set {damage = value;}
    }
    public List<StatusEffect> StatusEffects {
        get {return _statusEffects;}
        set {_statusEffects = value;}
    }
    public List<StatusEffect> RemoveEffects {
        get {return _removeEffects;}
        set {_removeEffects = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        collider.enabled = false;
        renderer.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            parent.DealDamage(other.GetComponent<Enemy>(), damage);
            var effectsManager = other.GetComponent<StatusEffectManager>();
                effectsManager?.ApplyEffects(_statusEffects);
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Enemy") {
            var effectsManager = other.GetComponent<StatusEffectManager>();
                effectsManager?.ApplyEffects(_removeEffects);
        }
    }
    public void TriggerDestruction() {
        GetComponent<Animator>().SetBool("destroyed", true);
    }
    public void Destroy() {
        Destroy(this.transform.parent.gameObject);
    }

    public void AlignSprite(Vector2 pos1, Vector2 pos2) {
        parent = GameObject.FindWithTag("Player").GetComponent<Player>();
        float dist = Vector2.Distance(pos1, pos2);
        float angle = Vector2.Angle(new Vector2(1.0f, 0f), pos2.y > pos1.y ? pos2-pos1 : pos1-pos2);
        Vector2 midpoint = (pos1 + pos2) / 2.0f;
        collider.size = new Vector2(dist, renderer.size.y*2f);
        renderer.size = new Vector2(dist, renderer.size.y*2f);
        transform.Rotate(new Vector3(0f, 0f, angle));
        transform.position = new Vector3(midpoint.x, midpoint.y, 0f);
        renderer.enabled = true;
        collider.enabled = true;
    }

}
