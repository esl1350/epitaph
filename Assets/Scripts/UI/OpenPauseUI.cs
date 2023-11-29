using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OpenPauseUI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject pauseMenu; 

    [SerializeField] public GameObject keybindMenu; 

    private MusicArea pause = MusicArea.PAUSE;
    private MusicArea battle = MusicArea.BATTLE;
    private MusicArea shop = MusicArea.SHOP_MUSIC;
    //[SerializeField] private InputAction pausing;
    //[SerializeField] public InputActionAsset playerPausing;
    private GameObject player;
    void Start(){
        //var gameplayActionMap = playerPausing.GetActionMap("Player");
        //pausing = gameplayActionMap.GetAction("Pause");   
        //pausing.per
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("Paused");
            pauseMenu.SetActive(true);
            player.GetComponent<PlayerController>().UpdateSound(true);
            Debug.Log(pause);
            AudioManager.instance.SetMusic(pause, "Pausing");
            Time.timeScale = 0;
        }
    }

    public void Resume(){
        
        if (SceneManager.GetActiveScene().buildIndex < 8)
        {
            AudioManager.instance.SetMusic(battle);
        }
        else
        {
            AudioManager.instance.SetMusic(shop);
        }
        
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit(){
        Application.Quit();
    }

    public void ChangeControls(){
        //opens menu to change control scheme for when playing the game
        keybindMenu.SetActive(true);
    }
}
