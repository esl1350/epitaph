using UnityEngine;
using System.Collections;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
public class SetLastSibling : MonoBehaviour
{

    public RectTransform panelRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        panelRectTransform.SetAsLastSibling();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
