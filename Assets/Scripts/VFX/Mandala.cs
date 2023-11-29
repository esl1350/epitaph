using UnityEngine;

public class Mandala : MonoBehaviour {
    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }
    
    public void Activate() {
        gameObject.SetActive(true);
        if (animator == null) {
            animator = gameObject.GetComponent<Animator>();
            Debug.Log("Animator Null");
        }
        animator.Play("Start");
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }

    public Vector2 GetPos() {
        return (Vector2)transform.position;
    }
}