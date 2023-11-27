using UnityEngine;

public class SlowAuraCollider : MonoBehaviour {

    [SerializeField]
    private StatusEffect slowEffect;

    [SerializeField]
    private StatusEffect removeSlowEffect;

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.TryGetComponent(out StatusEffectManager sem)) {
            sem.ApplyEffect(removeSlowEffect);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Applying slow");
        if (other.gameObject.TryGetComponent(out StatusEffectManager sem)) {
            sem.ApplyEffect(slowEffect);
        }
    }

}