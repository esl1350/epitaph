using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButton : MonoBehaviour
{
    private MusicArea area = MusicArea.BATTLE;

    public void callOnRunStart()
    {
        //Debug.Log("go pressed");
        
        GameObject levelController = GameObject.FindWithTag("LevelController");
        levelController.GetComponent<SceneTransitionManager>().OnRunStart();
        GameObject spawnLocation = GameObject.Find("PlayerSpawnLocation").gameObject;
        GameObject.Find("Player").gameObject.transform.position = spawnLocation.transform.position;
    }
}
