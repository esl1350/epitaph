using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttachCinemachineCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        attach();
    }
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        attach();
    }

    private void attach() {
        CompositeCollider2D collider = GameObject.FindGameObjectWithTag("CinemachineCollider").GetComponent<CompositeCollider2D>();
        //Debug.Log("Finding Collider..." + collider);
        this.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = collider;
    }
}