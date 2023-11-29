using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        playerBoundaries = this.transform.GetComponent<Boundaries>();
    }
    // Update is called once per frame
    void Update()
    {
        playerBoundaries = this.transform.GetComponent<Boundaries>();

        if(sceneTransitionManager == null) {
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
