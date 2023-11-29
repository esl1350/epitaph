using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButton : MonoBehaviour
{

    public void callOnRunStart()
    {
        GameObject levelController = GameObject.FindWithTag("LevelController");
        levelController.GetComponent<SceneTransitionManager>().OnRunStart();
        GameObject spawnLocation = GameObject.Find("PlayerSpawnLocation").gameObject;
        GameObject.Find("Player").gameObject.transform.position = spawnLocation.transform.position;
    }
}
