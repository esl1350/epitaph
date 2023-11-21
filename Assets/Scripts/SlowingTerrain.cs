using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SlowingTerrain : MonoBehaviour
{
    //variable declarations

    private float timer;
    private float timerAmt = 1.0f;
    [SerializeField] protected List<StatusEffect> _statusEffects;
    [SerializeField] protected List<StatusEffect> _removeStatusEffects;
    private List<GameObject> entitiesBeingSlowed = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("New Entity being slowed");
        entitiesBeingSlowed.Add(collision.gameObject);
        var statusEffectManager = collision.GetComponent<StatusEffectManager>();
            statusEffectManager?.ApplyEffects(_statusEffects);
        
    }

    //called when an entity leaves the pool
    void OnTriggerExit2D(Collider2D collision)
    {
        entitiesBeingSlowed.Remove(collision.gameObject);
        var statusEffectManager = collision.GetComponent<StatusEffectManager>();
            statusEffectManager?.ApplyEffects(_removeStatusEffects);
        Debug.Log("Entity no longer being slowed");
    }

/*     void Update()
    {
        timer += Time.deltaTime;
        if (timer > timerAmt)
        {
            //iterate through each entity in the pool
            foreach(GameObject entity in entitiesBeingSlowed)
            {
                var statusEffectManager = entity.GetComponent<StatusEffectManager>();
                    statusEffectManager?.ApplyEffects(_statusEffects);
                timer = 0;
            }
        }
    } */

    //called from an external script when a Player or enemy places down a slowing pool
    //takes in boolean parameter that would be true if this method was called by a Player ability and false otherwise
}