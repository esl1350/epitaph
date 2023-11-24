using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTransitionController : MonoBehaviour
{
    // Start is called before the first frame update
    private Boundaries playerBoundaries;

    private SceneTransitionManager sceneTransitionManager;
    void Start()
    {
        playerBoundaries = this.transform.GetComponent<Boundaries>();
        sceneTransitionManager = GameObject.FindGameObjectWithTag("LevelController").GetComponent<SceneTransitionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneTransitionManager == null) {
            sceneTransitionManager = GameObject.FindGameObjectWithTag("LevelController").GetComponent<SceneTransitionManager>();
        }
        if (this.transform.position.y > playerBoundaries.yBounds.y - playerBoundaries.widthHeight.y/2) {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0) {
                // No enemies left
                sceneTransitionManager.OnRunEnd();
                Debug.Log("Run End");
            }

        }
    }
}
