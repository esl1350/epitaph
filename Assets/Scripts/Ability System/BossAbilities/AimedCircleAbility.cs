using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedCircleAbility : BossAbility
{
    public GameObject indicator;
    public BossProjectile projectile;

    [SerializeField]
    private float delay;

    [SerializeField]
    private float delayBetweenRounds;

    [SerializeField]
    private float damage = 5;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private float maxRounds;

    public override void AbilityBehavior(GameObject parent)
    {
        StartCoroutine(WaitAndCast(parent));
    }

    IEnumerator WaitAndCast(GameObject parent)
    {
        for (int i = 0; i < maxRounds; i++)
        {
            // Create Indicator for where the projectile will go

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 aimedLocation = player.transform.position;
            Instantiate(indicator, aimedLocation, Quaternion.identity);
            Vector3 direction = (aimedLocation - parent.transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -direction);
            BossProjectile proj = Instantiate(projectile, parent.transform.position + (direction * 2), rotation);

            yield return new WaitForSeconds(delay);
            Rigidbody2D rb = proj.GetRB();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }

            yield return new WaitForSeconds(delayBetweenRounds);
        }

        Destroy(this.gameObject);
    }

}
