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
    public GameObject shield;

    public List<GameObject> crystals;
    public int activeCrystals;
    bool hasShield;

    public GameObject teleportationPoints;
    List<Vector2> tppoints = new List<Vector2>();

    int currentPoint = 0;

    bool first = false;
    public bool start = false;
    float lastTP = 0;

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
        if (start)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector2 vectordist = player.transform.position - this.transform.position;
            //find angle and choose sprite
            float angle = Mathf.Atan2(vectordist.y, vectordist.x) * Mathf.Rad2Deg;
            if (angle < 0)
            {
                angle = 360 + angle;
            }
            int math = (int)((angle + 45) / 90) - 1;
            int spriteNo = ((math % 4) + 4) % 4;
            //SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
            //renderer.sprite = sprites[spriteNo];
            animator.SetFloat("angle", angle);

            if (!first)
            {
                BossAbility choice = Instantiate(abilities[2]);
                choice.AbilityBehavior(this.gameObject);
                first = true;
            }
            else if (Time.time - lastCastTime >= castDelay)
            {
                float distance = vectordist.magnitude;

                if (distance < 5 && !hasShield)
                {
                    if (Time.time - lastTP > 12)
                    {
                        StartCoroutine(TeleportFromPlayer());
                        lastTP = Time.time;
                    }
                    else
                    {
                        StartCoroutine(CastingAnimation(false));
                    }
                }
                else
                {
                    StartCoroutine(CastingAnimation(true));
                }
                lastCastTime = Time.time;
            }
        }
    }

    IEnumerator CastingAnimation(bool atk)
    {
        animator.SetBool("casting", true);
        yield return new WaitForSeconds(0.5f);
        
        if(atk)
        {
            ChooseAttack();
        }
        else
        {
            ChooseDefensive();
        }
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("casting", false);
    }

    public void ActivateCrystals()
    {
        foreach (GameObject crystal in crystals)
        {
            crystal.SetActive(true);
        }
        shield.SetActive(true);
        hasShield = true;
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
        castDelay = castDelay / 1.25f;
        Destroy(shield);
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
            if (i <= 10)
            {
                i = 2;
            }
            else if (i <= 65)
            {
                i = 0;
            }
            else
            {
                i = 1;
            }
            BossAbility choice = Instantiate(abilities[i]);
            choice.AbilityBehavior(this.gameObject);
        }
        else
        {
            // Phase 2 stuff
            int i = Random.Range(0, 100);
            if (i <= 60)
            {
                i = 0;
            }
            else
            {
                i = 1;
            }
            BossAbility choice = Instantiate(abilities[i]);
            choice.AbilityBehavior(this.gameObject);
        }
    }

    public void ChooseDefensive()
    {
        int i = Random.Range(0, 100);
        if (i <= 35)
        {
                i = 0;
        }
        else
        {
            i = 1;
        }

        BossAbility choice = Instantiate(defensiveAbilities[i]);
        choice.AbilityBehavior(this.gameObject);
    }

    //honestly can be changed into an ability that tps to a random point later when theres time
    private IEnumerator TeleportFromPlayer()
    {
        animator.SetBool("tp", true);
        yield return new WaitForSeconds(0.9f);
        currentPoint = (currentPoint + 1) % tppoints.Count;
        this.transform.position = tppoints[currentPoint];
        animator.SetBool("tp", false);
    }
}
