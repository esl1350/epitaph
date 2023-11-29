using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationEvents : MonoBehaviour
{
    [SerializeField] private LevelGeneration levelGeneration;
    private GameObject enemyManager;
    public void Update() {
        if (levelGeneration == null) {
            levelGeneration = GameObject.Find("LevelGenerator").GetComponent<LevelGeneration>();
            enemyManager = GameObject.FindWithTag("EnemyManager");

        }
    }
    public void OnUIFadeoutEnd() {
        levelGeneration = GameObject.Find("LevelGenerator").GetComponent<LevelGeneration>();
        levelGeneration.PlaceEnemies();
        for (int i = 0; i<enemyManager.transform.childCount; i++) {
            enemyManager.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

}
