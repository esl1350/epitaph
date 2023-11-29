using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Random Tele Augment", menuName = "Augments/Random Tele")]
public class RandomTeleAugment : Augment
{
    [SerializeField]
    private float interval;
    public override IEnumerator _Enable(GameObject parent, GameObject vfxObject) {
        
        // Return Coroutine
        if (parent.TryGetComponent(out Boundaries boundaries) && parent.TryGetComponent(out Entity player)) {
            return PassiveDamageCoroutine(boundaries, player);
        }
        return null;
    }

    private IEnumerator PassiveDamageCoroutine(Boundaries boundaries, Entity player)
    {
        while (true)
        {
            Vector3 playerTransform = player.transform.position;

            Vector3 randomFactor = new Vector3(UnityEngine.Random.Range(-1f, 1), UnityEngine.Random.Range(-1f, 1)).normalized * 4f;

            playerTransform.x = Math.Clamp(playerTransform.x + randomFactor.x, boundaries.xBounds.x, boundaries.xBounds.y);
            playerTransform.y = Math.Clamp(playerTransform.y + randomFactor.y, boundaries.yBounds.x, boundaries.yBounds.y);

            player.gameObject.transform.position = playerTransform;

            yield return new WaitForSeconds(interval);
        }
        
    }
}
