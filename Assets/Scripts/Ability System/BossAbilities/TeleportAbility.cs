using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : BossAbility
{
    public GameObject teleportationPoints;
    List<Vector2> tppoints = new List<Vector2>();

    int currentPoint = 0;

    private void Start()
    {
        tppoints.Add(this.transform.position);
        foreach (Transform child in teleportationPoints.transform)
        {
            tppoints.Add(child.position);
        }
    }

    public override void AbilityBehavior(GameObject caster)
    {
        print("teleporting boss");
        currentPoint = (currentPoint + 1) % tppoints.Count;
        caster.transform.position = tppoints[currentPoint];
        AudioManager.instance.PlayOneShot(FMODEvents.instance.teleport, caster.transform.position);
    }
}
