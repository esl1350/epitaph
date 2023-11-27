using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slow Aura Augment", menuName = "Augments/Slow Aura")]
public class SlowAuraAugment : Augment
{
    [SerializeField]
    private Sprite vfxSprite;

    private GameObject childCollider;

    [SerializeField]
    private GameObject colliderPrefab;
    public override IEnumerator _Enable(GameObject parent, GameObject vfxObject) {
        // Do stuff with vfxObject
        if (vfxObject.TryGetComponent(out SpriteRenderer spriteRenderer)) {
            spriteRenderer.sprite = vfxSprite;
            spriteRenderer.enabled = true;
        }

        // Instantiate collider
        childCollider = Instantiate(colliderPrefab, parent.transform);
        
        return null;
    }

    public override void _Disable(GameObject parent, GameObject vfxObject) {
        // Do stuff with vfx object
        if (vfxObject.TryGetComponent(out SpriteRenderer spriteRenderer)) {
            spriteRenderer.enabled = false;
        }

        Destroy(childCollider);
    }

}
