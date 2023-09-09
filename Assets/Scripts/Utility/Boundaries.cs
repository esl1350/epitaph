using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    
    //basic boundaries for entities
    
    private Vector2 xBounds;
    private Vector2 yBounds;

    //width and height of entity
    private Vector2 widthHeight;

    // Start is called before the first frame update
    void Start()
    {
        GameObject walkable = GameObject.FindWithTag("Walkable");

        float xLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z)).x;
        float xRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x;
        xBounds = new Vector2(xLeft, xRight);

        float walkableHeight = walkable.GetComponent<Collider2D>().bounds.size.y;
        Vector2 walkablePos = walkable.transform.position;
        yBounds = new Vector2(walkablePos.y - walkableHeight/2, walkablePos.y + walkableHeight/2);

        widthHeight = this.gameObject.GetComponent<BoxCollider2D>().size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate() {
        float x = Mathf.Clamp(this.transform.position.x, xBounds.x + widthHeight.x/2, xBounds.y - widthHeight.x/2);
        float y = Mathf.Clamp(this.transform.position.y, yBounds.x + widthHeight.y/2, yBounds.y - widthHeight.y/2);
        this.transform.position = new Vector2(x, y);
    }
}
