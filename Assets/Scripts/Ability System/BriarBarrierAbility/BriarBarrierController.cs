using System;
using UnityEngine;

class BriarBarrierController : MonoBehaviour {

    private int num_children;

    private float timestep = 0;

    [SerializeField]
    private float warmup = 0.2f;

    [SerializeField]
    private float radius = 1f;

    [SerializeField]
    private float angular_speed = 1f;

    [SerializeField]
    private float duration = 5f;

    private float phase_delta;
    void Start() {
        num_children = this.transform.childCount;
        phase_delta = 2 * (float) Math.PI / num_children;

    }

    void Update() {
        timestep += Time.deltaTime;

        Vector3 center = this.transform.position;

        if (timestep <= warmup) {
            for (int i = 0; i < num_children; i++) {
                Transform child = this.transform.GetChild(i);
                Vector3 target = CalculateTarget(center, i);
                Vector3 midpoint = CalculateMidpoint(center, i);
                child.position = QuadraticBezierCurveInterpolate(center, midpoint, target, timestep / warmup);
            }
            return;
        }

        float adj_timestep = (timestep - warmup) % (float) Math.PI;
        for (int i = 0; i < num_children; i++) {
            Vector3 target = center;
            target.x += radius * (float) Math.Cos(phase_delta * i + angular_speed * adj_timestep);
            target.y += radius * (float) Math.Sin(phase_delta * i + angular_speed * adj_timestep);
            this.transform.GetChild(i).position = target;
        }

        if (timestep > duration) {
            DestroyKeepVFX();
        }

    }
    private void DestroyKeepVFX() {
        const float VFX_EXPIRY_TIMER = 2f;
        for (int i = 0; i < this.transform.childCount; i++) {
            Transform vfx_child = this.transform.GetChild(i).GetChild(0);
            vfx_child.parent = null;
            vfx_child.GetComponent<ParticleSystem>().Stop();
            Destroy(vfx_child.gameObject, VFX_EXPIRY_TIMER);
        }
        Destroy(this.transform.gameObject);
    }
    private Vector3 CalculateTarget(Vector3 origin, int index) {
        Vector3 target = origin;
        target.x += radius * (float) Math.Cos(phase_delta * index);
        target.y += radius * (float) Math.Sin(phase_delta * index);
        return target;
    }

    private Vector3 CalculateMidpoint(Vector3 origin, int index) { 
        // Some random point honestly
        Vector3 midpoint = origin;
        midpoint.x += radius / 2 * (float) Math.Cos((phase_delta * (index - 1)) + (phase_delta / 2));
        midpoint.y += radius / 2 * (float) Math.Sin((phase_delta * (index - 1)) + (phase_delta / 2));
        return midpoint;
    }

    // ¯\_(;-;)_/¯ the oracle told me to do this
    private static Vector3 QuadraticBezierCurveInterpolate(Vector3 a, Vector3 b, Vector3 c, float t) {
        Vector3 ab = Vector3.Lerp(a, b, t);   // point between a and b (at t)
        Vector3 bc = Vector3.Lerp(b, c, t);   // point between b and c (at t)
        return Vector3.Lerp(ab, bc, t);       // point between ab and bc (at t)
    }



}