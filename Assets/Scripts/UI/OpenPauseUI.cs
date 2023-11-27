using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenPauseUI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject pauseMenu; 

    [SerializeField] public GameObject keybindMenu; 
    //[SerializeField] private InputAction pausing;
    //[SerializeField] public InputActionAsset playerPausing;
    void Start(){
        //var gameplayActionMap = playerPausing.GetActionMap("Player");
        //pausing = gameplayActionMap.GetAction("Pause");   
        //pausing.per
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Resume(){
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
