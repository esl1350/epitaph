using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuffHealth : Item
{
    [SerializeField]
    private int healthBuff = 15;

    public Sprite abilitySprite;

    public override void firstActivation(Player player)
    {
        Debug.Log("Increasing health by: " + healthBuff);
        player.Health.maxValue += healthBuff;

        if (healthBuff + player.HealthVal > player.Health.maxValue)
        {
            player.Health.Heal(player.HealthVal - healthBuff);
        } else
        {
            player.Health.Heal(healthBuff);
        }
    }

    public override void disableStatic(Player player)
    {
        player.Health.maxValue -= healthBuff;
    }
}
