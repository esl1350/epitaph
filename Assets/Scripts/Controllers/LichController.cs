using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LichController : Controller
{
    [SerializeField]
    private float castDelay;
    private float lastCastTime;

    public List<BossAbility> abilities;
    public List<BossAbility> defensiveAbilities;

    [SerializeField]
    int activeCrystals;
    bool hasShield;

    public GameObject teleportationPoints;
    List<Vector2> tppoints = new List<Vector2>();

    int currentPoint = 0;

    // Delete later
    bool first = false;

    void Start()
    {
        base.Start();
        hasShield = true;
        activeCrystals = 3;

        tppoints.Add(this.transform.position);
        foreach (Transform child in teleportationPoints.transform)
        {
            tppoints.Add(child.position);
        }

        lastCastTime = Time.time;
        first = false;
    }

    private void Update()
    {
        if (!first)
        {
            BossAbility choice = Instantiate(abilities[2]);
            choice.AbilityBehavior(this.gameObject);
            first = true;
        }
        if (Time.time - lastCastTime >= castDelay)
        {
            ChooseAttack();
            lastCastTime= Time.time;
        }
    }

    public void RemoveCrystal()
    {
        activeCrystals--;
        if (activeCrystals == 0)
        {
            this.RemoveShield();
        }
    }

    private void RemoveShield()
    {
        hasShield = false;
        Enemy enemyComp = GetComponent<Enemy>();
        enemyComp.enabled = true;
    }

    public bool HasShield()
    {
        return hasShield;
    }

    public void ChooseAttack()
    {
        if (activeCrystals > 0)
        {
            // Phase 1 stuff
            int i = Random.Range(0, 100);
            if ( i <= 8)
            {
                i = 2;
            } else if (i <= 60)
            {
                i = 0;
            } else
            {
                i = 1;
            }
            BossAbility choice = Instantiate(abilities[i]);
            choice.AbilityBehavior(this.gameObject);
        }
        else
        {
            // Phase 2 stuff
            print("Crystals destroyed!, Phase 2 doesn't exist yet sorry");
        }
    }

    public void ChooseDefensive()
    {
        // BossAbility choice = defensiveAbilities[0];
        // choice.AbilityBehavior(this.gameObject);

        TeleportFromPlayer();
    }

    //honestly can be changed into an ability that tps to a random point later when theres time
    private void TeleportFromPlayer()
    {
        currentPoint = (currentPoint + 1) % tppoints.Count;
        this.transform.position = tppoints[currentPoint];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ChooseDefensive();
        }
    }
}
