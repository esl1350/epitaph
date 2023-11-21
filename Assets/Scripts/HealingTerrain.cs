using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class HealingTerrain : MonoBehaviour
{
    //variable declarations
    [SerializeField] private bool setByPlayer;
    [SerializeField] private float healAmount;
    [SerializeField] private float timeInterval;
    private bool isPlayer;
    private List<GameObject> entitiesBeingHealed = new List<GameObject>();

    //called once an entity enters the healing pool
    void OnTriggerEnter2D(Collider2D collision)
    {
        //determine if the Entity is a Player by seeing if it has the Player script
        isPlayer = (collision.gameObject.GetComponent<Player>()!= null);
        
        //if it is and the source is player OR if it isn't and the source is enemy
        if((isPlayer && setByPlayer) || (!isPlayer && !setByPlayer))
        {
            //add the Entity to the list of entities currently in the pool
            entitiesBeingHealed.Add(collision.gameObject);
            Debug.Log("Length of list: " + entitiesBeingHealed.Count);
            //heal the entity while its in the pool
            InvokeRepeating("Heal", 0f, timeInterval);

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
        entitiesBeingHealed.Remove(collision.gameObject);
        Debug.Log("Length of list: " + entitiesBeingHealed.Count);
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
            //call the Health.Heal method on that entity
            entity.gameObject.GetComponent<Entity>().Health.Heal(healAmount);
            Debug.Log("Healed");
        }

    }

    //called from an external script when a Player or enemy places down a healing pool
    //takes in boolean parameter that would be true if this method was called by a Player ability and false otherwise
    public void SetSource(bool wasPlayer)
    {
        setByPlayer = wasPlayer;
    }
   
}
