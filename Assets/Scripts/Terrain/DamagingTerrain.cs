using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Damaging_Terrain : MonoBehaviour
{

    //variable declarations
    [SerializeField] private bool setByPlayer;
    [SerializeField] private float damageAmount;
    [SerializeField] private float timeInterval;
    private bool isPlayer;
    private List<GameObject> entitiesBeingDamaged = new List<GameObject>();


    void OnTriggerEnter2D(Collider2D collision)
    {
        //determine if the Entity is a Player by seeing if it has the Player script
        isPlayer = (collision.gameObject.GetComponent<Player>()!= null);
        
        //if it is the player and terrain is not set by player OR it is not the player and set by the player
        if((isPlayer && !setByPlayer) || (!isPlayer && setByPlayer))
        {
            //add the Entity to the list of entities currently in the pool
            entitiesBeingDamaged.Add(collision.gameObject);
            Debug.Log("Length of list: " + entitiesBeingDamaged.Count);
            //damage the entity while its in the pool
            InvokeRepeating("Damage", 0f, timeInterval);

        }
        //otherwise, do nothing
        else
        {
            Debug.Log("Wrong Entity!");
        }
    }
    
    //called when an entity leaves the pool
    void OnTriggerExit2D(Collider2D collision)
    {
        //remove the entity from the list of entities in the pool
        entitiesBeingDamaged.Remove(collision.gameObject);
        Debug.Log("Length of list: " + entitiesBeingDamaged.Count);
        //if there are no entities
        if(entitiesBeingDamaged.Count == 0) 
        {
            //stop running the foreach loop
            CancelInvoke();
        }
    }

    // UNFINISHED
    //called when at least one entity is in the pool every timeInterval seconds
    private void Damage()
    {
        //iterate through each entity in the pool
        foreach(GameObject entity in entitiesBeingDamaged)
        {
            //call the Health.Heal method on that entity
            entity.gameObject.GetComponent<Entity>().Health.TakeDamage(damageAmount);
            Debug.Log("Damaged");
        }

    }


    //called from an external script when a Player or enemy places down a healing pool
    //takes in boolean parameter that would be true if this method was called by a Player ability and false otherwise
    public void SetSource(bool wasPlayer)
    {
        setByPlayer = wasPlayer;
    }

}
