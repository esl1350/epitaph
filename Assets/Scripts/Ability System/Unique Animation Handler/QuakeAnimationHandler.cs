using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuakeAnimationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyParent() {
        Destroy(transform.parent.gameObject);
    }

    public void ActivateHitbox() {
        transform.parent.gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
}
