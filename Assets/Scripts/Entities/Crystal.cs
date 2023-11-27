using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Entity
{
    LichController lich;
    bool damaged = false;
    float lastDamaged;
    // Start is called before the first frame update
    protected override void Start()
    {
        lich = this.transform.parent.GetComponentInChildren<LichController>();
        lastDamaged = Time.time;
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.1f;
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.blue, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;

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
        Health.TakeDamage(amount);
        damaged = true;
    }

    public bool WasDamaged()
    {
        return damaged;
    }

    public override void Die()
    {
        lich.RemoveCrystal();
        Destroy(gameObject);
    }
}
