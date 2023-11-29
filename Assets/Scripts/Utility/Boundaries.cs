using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boundaries : MonoBehaviour
{
    
    //basic boundaries for entities
    
    public Vector2 xBounds { get; private set; }
    public Vector2 yBounds { get; private set; }

    //width and height of entity
    public Vector2 widthHeight { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        GameObject walkable = GameObject.FindWithTag("Walkable");

        float xLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z)).x;
        float xRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x;
        xBounds = new Vector2(xLeft, xRight);

        float walkableHeight = walkable.GetComponent<BoxCollider2D>().bounds.size.y;
        Vector3 colliderCenter = walkable.GetComponent<BoxCollider2D>().bounds.center;
        yBounds = new Vector2(colliderCenter.y - walkableHeight/2, colliderCenter.y + walkableHeight/2);

        widthHeight = this.gameObject.GetComponent<CapsuleCollider2D>().size;
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        GameObject walkable = GameObject.FindWithTag("Walkable");

        float xLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z)).x;
        float xRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x;
        xBounds = new Vector2(xLeft, xRight);

        float walkableHeight = walkable.GetComponent<BoxCollider2D>().bounds.size.y;
        Vector3 colliderCenter = walkable.GetComponent<BoxCollider2D>().bounds.center;
        yBounds = new Vector2(colliderCenter.y - walkableHeight/2, colliderCenter.y + walkableHeight/2);

        widthHeight = this.gameObject.GetComponent<CapsuleCollider2D>().size;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     GameObject walkable = GameObject.FindWithTag("Walkable");

    //     float xLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z)).x;
    //     float xRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x;
    //     xBounds = new Vector2(xLeft, xRight);

    //     float walkableHeight = walkable.GetComponent<BoxCollider2D>().bounds.size.y;
    //     Vector3 colliderCenter = walkable.GetComponent<BoxCollider2D>().bounds.center;
    //     yBounds = new Vector2(colliderCenter.y - walkableHeight/2, colliderCenter.y + walkableHeight/2);

    //     widthHeight = this.gameObject.GetComponent<CapsuleCollider2D>().size;
    // }

    void LateUpdate() {
        float x = Mathf.Clamp(this.transform.position.x, xBounds.x + widthHeight.x/2, xBounds.y - widthHeight.x/2);
        float y = Mathf.Clamp(this.transform.position.y, yBounds.x + widthHeight.y/2, yBounds.y - widthHeight.y/2);
        // float x = Mathf.Clamp(this.transform.position.x, xBounds.x - 1f, xBounds.y + 1f);
        // float y = Mathf.Clamp(this.transform.position.y, yBounds.x, yBounds.y);
        this.transform.position = new Vector2(x, y);
    }
}
