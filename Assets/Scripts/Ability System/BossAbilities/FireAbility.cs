using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class FireAbility : BossAbility
{
    [SerializeField]
    public float delay;
    public GameObject damageCone;

    public override void AbilityBehavior(GameObject parent)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Vector to player
        Vector2 directionToPlayer = parent.transform.position - player.transform.position;
        directionToPlayer.Normalize();
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, directionToPlayer);

        Vector3 displacement = 1.75f * directionToPlayer;
        displacement.y = displacement.y + 0.75f;

        FireCone fire = Instantiate(damageCone, parent.transform.position - displacement, rotation).GetComponent<FireCone>();
        fire.parent = parent;
        Destroy(this.gameObject);
    }
}
