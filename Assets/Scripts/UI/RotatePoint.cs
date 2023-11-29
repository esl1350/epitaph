using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePoint : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = (mousePos - transform.parent.position).normalized;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        float denom = (1.0f / 0.7225f) * Mathf.Pow(Mathf.Cos(rotZ * Mathf.Deg2Rad), 2) + (1.0f / 1.21f) * Mathf.Pow(Mathf.Sin(rotZ * Mathf.Deg2Rad), 2); //hmm
        float l = Mathf.Sqrt(1.0f / denom);
        transform.rotation = Quaternion.Euler(0, 0, rotZ - 90.0f);
        Vector2 r_norm = (new Vector2(rotation.x, rotation.y)).normalized;
        transform.localPosition = new Vector3(r_norm.x * l, r_norm.y * l, 0);
    }
}
