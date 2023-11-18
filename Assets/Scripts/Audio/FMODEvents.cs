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
    public static FMODEvents instance {get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }

        instance = this;
    }
}