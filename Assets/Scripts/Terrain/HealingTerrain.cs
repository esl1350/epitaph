using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class HealingTerrain : MonoBehaviour
{
    //variable declarations
    [SerializeField] private float healAmount;
    [SerializeField] private float timeInterval;
    [SerializeField] private float lifespan;
    private float timer;
    private List<GameObject> entitiesBeingHealed = new List<GameObject>();



    void Start() {
        timer = 0f;
    }

    void Update() {
        timer += Time.deltaTime;

        if (timer > lifespan) {
            Destroy(this.gameObject);
        }
    }
    //called once an entity enters the healing pool
    void OnTriggerEnter2D(Collider2D collision)
    {
        //determine if the Entity is a Player by seeing if it has the Player script
        bool isPlayer = (collision.gameObject.GetComponent<Player>()!= null);
        bool isEntity = (collision.gameObject.GetComponent<Entity>() != null);
        //if it is and the source is player OR if it isn't and the source is enemy
        if (!isPlayer && isEntity)
        {
            //add the Entity to the list of entities currently in the pool
            entitiesBeingHealed.Add(collision.gameObject);
            //heal the entity while its in the pool
            InvokeRepeating("Heal", 0f, timeInterval);
        }
        
    }

    //called when an entity leaves the pool
    void OnTriggerExit2D(Collider2D collision)
    {
        //remove the entity from the list of entities in the pool
        entitiesBeingHealed.Remove(collision.gameObject);
        //if there are no entities
        if(entitiesBeingHealed.Count == 0) 
        {
            //stop running the foreach loop
            CancelInvoke();
        }
    }

    //called when at least one entity is in the pool every timeInterval seconds
    private void Heal()
    {
        //iterate through each entity in the pool
        foreach(GameObject entity in entitiesBeingHealed)
        {
            Debug.Log(entity.gameObject.GetComponent<Entity>().HealthVal);
            //call the Health.Heal method on that entity
            if (entity.gameObject.GetComponent<Entity>().Health.GetStatValue() + healAmount > entity.gameObject.GetComponent<Entity>().Health.intialValue) {
                entity.gameObject.GetComponent<Entity>().Health.Heal(entity.gameObject.GetComponent<Entity>().Health.intialValue - entity.gameObject.GetComponent<Entity>().HealthVal > 0f ? entity.gameObject.GetComponent<Entity>().Health.intialValue - entity.gameObject.GetComponent<Entity>().HealthVal : 0f);
            } else {
                entity.gameObject.GetComponent<Entity>().Health.Heal(healAmount);
            }
        }

    }
   
}
