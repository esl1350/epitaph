using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    public void Close()
    {
        Time.timeScale = 1;
        this.transform.parent.gameObject.SetActive(false);
    }
}
