using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenKeybindUI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject keybindMenu; 

    void Start(){
        //var gameplayActionMap = playerPausing.GetActionMap("Player");
        //pausing = gameplayActionMap.GetAction("Pause");   
        //pausing.per
    }

    public void ExitKeybind(){
        //opens menu to change control scheme for when playing the game
        keybindMenu.SetActive(false);
    }
}
