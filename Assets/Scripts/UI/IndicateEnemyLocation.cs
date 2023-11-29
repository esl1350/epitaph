using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicateEnemyLocation : MonoBehaviour
{
    private GameObject player;
    private GameObject cam;
    private bool hidden;
    private bool posYShown;
    private bool negYShown;
    // Start is called before the first frame update
    void Start()
    {
        posYShown = false;
        negYShown = false;
        hidden = false;
        player = GameObject.FindWithTag("Player");
        cam = GameObject.FindWithTag("MainCamera");
    }

    void Update () {
        if (transform.childCount < 6) {
            ShowIndicators();
            hidden = false;
        } else {
            if (!hidden) {
                HideIndicators();
                hidden = true;
            }
        }
    }

    void HideIndicators() {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            GameObject pointer = this.gameObject.transform.GetChild(i).Find("Pointer").gameObject;
            pointer.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    
    void ShowIndicators() {
        for (int i = 0; i < this.gameObject.transform.childCount; i++) {
            posYShown = false;
            negYShown = false;
            GameObject enemy = this.gameObject.transform.GetChild(i).gameObject;
            GameObject pointer = enemy.transform.Find("Pointer").gameObject;
            if (!enemy.GetComponent<SpriteRenderer>().isVisible) {
                Vector3 rotation = (enemy.transform.position - player.transform.position);
                if (rotation.y > 0 && !posYShown) {
                    posYShown = true;
                    Vector3 camRotation = (enemy.transform.position - cam.transform.position);
                    float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                    float camRotZ = Mathf.Atan2(camRotation.y, camRotation.x) * Mathf.Rad2Deg;
                    float denom = (1.0f / 225.0f) * Mathf.Pow(Mathf.Cos(camRotZ * Mathf.Deg2Rad), 2) + (1.0f / 71.19f) * Mathf.Pow(Mathf.Sin(camRotZ * Mathf.Deg2Rad), 2);
                    float l = Mathf.Sqrt(1.0f / denom) - 2.0f;
                    pointer.transform.rotation = Quaternion.Euler(0, 0, rotZ);
                    Vector2 r_norm = (new Vector2(camRotation.x, camRotation.y)).normalized;
                    pointer.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 0) + new Vector3(r_norm.x * l, r_norm.y * l, 0);
                    pointer.GetComponent<SpriteRenderer>().enabled = true;
                }
                else if (rotation.y < 0 && !negYShown) {
                    negYShown = true;
                    Vector3 camRotation = (enemy.transform.position - cam.transform.position);
                    float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                    float camRotZ = Mathf.Atan2(camRotation.y, camRotation.x) * Mathf.Rad2Deg;
                    float denom = (1.0f / 225.0f) * Mathf.Pow(Mathf.Cos(camRotZ * Mathf.Deg2Rad), 2) + (1.0f / 71.19f) * Mathf.Pow(Mathf.Sin(camRotZ * Mathf.Deg2Rad), 2);
                    float l = Mathf.Sqrt(1.0f / denom) - 2.0f;
                    pointer.transform.rotation = Quaternion.Euler(0, 0, rotZ);
                    Vector2 r_norm = (new Vector2(camRotation.x, camRotation.y)).normalized;
                    pointer.transform.position = new Vector3(0, cam.transform.position.y, 0) + new Vector3(r_norm.x * l, r_norm.y * l, 0);
                    pointer.GetComponent<SpriteRenderer>().enabled = true;
                } else {
                    pointer.GetComponent<SpriteRenderer>().enabled = false;
                }

            } else {
                pointer.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
