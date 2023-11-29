using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Player Footsteps")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("Player Dash")]
    [field: SerializeField] public EventReference playerDash {get; private set; }

    [field: Header("Mini Knife Swing")]
    [field: SerializeField] public EventReference miniKnifeSwing { get; private set; }

    [field: Header("Knife Swing")]
    [field: SerializeField] public EventReference knifeSwing { get; private set; }

    [field: Header("Knife Flurry")]
    [field: SerializeField] public EventReference knifeFlurry { get; private set; }

    [field: Header("Quake First")]
    [field: SerializeField] public EventReference quakeFirst { get; private set; }

    [field: Header("Quake Finish")]
    [field: SerializeField] public EventReference quakeFinish { get; private set; }

    [field: Header("Laser")]
    [field: SerializeField] public EventReference laser { get; private set; }

    [field: Header("Yoyo")]
    [field: SerializeField] public EventReference yoyo { get; private set; }

    [field: Header("Light Grenade")]
    [field: SerializeField] public EventReference lightGrenade { get; private set; }

    [field: Header("Barrier")]
    [field: SerializeField] public EventReference barrier { get; private set; }

    [field: Header("Dead Spark")]
    [field: SerializeField] public EventReference deadSpark { get; private set; }

    [field: Header("Fire Sword")]
    [field: SerializeField] public EventReference fireSword { get; private set; }

    [field: Header("Samsara")]
    [field: SerializeField] public EventReference samsara { get; private set; }

    [field: Header("Teleport")]
    [field: SerializeField] public EventReference teleport { get; private set; }

    [field: Header("Boss Circles")]
    [field: SerializeField] public EventReference bossCircles { get; private set; }

    [field: Header("Boss Aim")]
    [field: SerializeField] public EventReference bossAim { get; private set; }

    [field: Header("Fire Cone")]
    [field: SerializeField] public EventReference fireCone { get; private set; }

    public static FMODEvents instance {get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        else 
        {
            instance = this;
        }
    }
}