using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeCircleAbility : BossAbility
{
    [SerializeField]
    public float delay;
    public BossProjectile projectile;
    private List<BossProjectile> createdProjectiles;

    [SerializeField]
    private float damage = 5;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float maxRounds;

    public override void AbilityBehavior(GameObject parent)
    {
        createdProjectiles = new List<BossProjectile>();
        StartCoroutine(WaitAndCast(parent));
    }

    IEnumerator WaitAndCast(GameObject parent)
    {
        Vector2 center = parent.transform.position;
        for (int i = 0; i < maxRounds; i++)
        {
            CreateCircle(center, i);
            yield return new WaitForSeconds(delay);
            ThrowCircle(i);
        }

        Destroy(this.gameObject);
    }

    private void CreateCircle(Vector2 center, int offset)
    {
        for (int i = 0; i < 8; i++)
        {
            float angle = ((45 * i) + (22.5f * offset)) / 180 * Mathf.PI;
            float offsetX = Mathf.Cos(angle) * 2;
            float offsetY = Mathf.Sin(angle) * 2;
            Vector2 directionOut = new Vector2(center.x + offsetX, center.y + offsetY);
            BossProjectile proj = Instantiate(projectile, directionOut, Quaternion.identity);
            createdProjectiles.Add(proj);
        }
    }

    private void ThrowCircle(int offset)
    {
        for (int i = createdProjectiles.Count - 1; i >= 0; i--)
        {
            BossProjectile proj = createdProjectiles[i];
            float angle = ((45 * i) + (22.5f * offset)) / 180 * Mathf.PI;
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);
            Rigidbody2D rb = proj.GetRB();
            if (rb != null)
            {
                Vector2 velocity = new Vector2(x, y).normalized * bulletSpeed;
                rb.velocity = velocity;
                proj.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}
