using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class SceneTransitionManager : MonoBehaviour
{
    // Update is called once per frame
    private GameObject enemyManager;
    private Animator transition;
    private bool forceInventoryTransition = false;

    [SerializeField]
    private bool sceneHasCombat;

    [SerializeField]
    private int nextLevel;

    [SerializeField] private GameObject levelGenerator;
    private AbilityHolder abilityHolder;

    private AbilityInventoryManager abilityInventoryManager;

    private PlayerInput playerInput;

    [SerializeField]
    private bool disableRunEnd;

    private GameObject player;

    public void LoadNextLevel() {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().resetPos();
        if (!loadingScene) {
            StartCoroutine(AsyncLoadNextLevel(nextLevel));
            loadingScene = true;
        }
    }

    private bool loadingScene;

    IEnumerator AsyncLoadNextLevel(int sceneToLoad) {
        if (!playerInput) Debug.Log("[SceneTransitionManager] PlayerInput not found. Is the player not loaded?");
        else playerInput.DeactivateInput();
        transition.ResetTrigger("RunEnd");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    void Awake() {
        transition = GameObject.FindWithTag("UI").GetComponent<Animator>();
        transition.SetBool("InterludeRoom", !sceneHasCombat);
    }
    void Start() {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager");
        abilityHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityHolder>();
        if (!abilityHolder) {
           Debug.Log("[SceneTransitionManager] Couldn't fetch ability inventory. Is the player not loaded?");
        }

        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        if (!playerInput) {
            Debug.Log("[SceneTransitionManager] Couldn't fetch player input component. Is the player not loaded?");
        }

        abilityInventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityInventoryManager>();
        if (!abilityInventoryManager) {
            Debug.Log("[SceneTransitionManager] Couldn't fetch player input component. Is the player not loaded?");
        }

        if (sceneHasCombat || forceInventoryTransition) {
            playerInput.DeactivateInput();
            if (abilityHolder.abilityInventoryActive == false)
                abilityHolder.OnAbilityInventory();
        }

        loadingScene = false;
    }

    public void OnRunStart() {
        Debug.Log("Run Start");
        playerInput.ActivateInput();
        transition.SetTrigger("RunStart");
        abilityHolder.OnAbilityInventory();
        abilityInventoryManager.hotbar.Refresh();
        abilityInventoryManager.OnRunStart();
    }

    public void OnRunEnd() {
        if (disableRunEnd) return;
        if (enemyManager.transform.childCount > 0) {
            return;
        }
        playerInput.DeactivateInput();
        transition.ResetTrigger("RunStart");
        transition.SetTrigger("RunEnd");
        abilityInventoryManager.OnRunEnd();
    }
}
