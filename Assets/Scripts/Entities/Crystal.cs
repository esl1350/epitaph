using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Enemy
{
    public List<Sprite> damagedSprites;
    LichController lich;
    bool damaged = false;
    float lastDamaged;
    bool died = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        lich = this.transform.parent.GetComponentInChildren<LichController>();
        lastDamaged = Time.time;
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.1f;
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(new Color(0.161f, 0.8f, 0.663f), 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
        lineRenderer.sortingLayerName = "Entities";
    }

    // Update is called once per frame
    void Update()
    {
        //update the lines in case the lich moves :)
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        Vector3[] positions = { this.transform.position, lich.transform.position };
        lineRenderer.SetPositions(positions);

        if (Time.time - lastDamaged > 2)
        {
            damaged = false;
        }
    }
    public override void TakeDamage(float amount)
    {
        damaged = true;
        base.TakeDamage(amount);

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (HealthVal <= intialHealth * 1 / 3) {
            renderer.sprite = damagedSprites[1];
        } else if (HealthVal <= intialHealth * 2 / 3) {
            renderer.sprite = damagedSprites[0];
        }
    }

    public bool WasDamaged()
    {
        return damaged;
    }

    public override void Die()
    {
        if (!died)
        {
            died = true;
            lich.RemoveCrystal();
            Destroy(gameObject);
        }
    }
}
