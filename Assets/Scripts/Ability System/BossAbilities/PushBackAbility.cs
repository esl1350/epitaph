using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PushBackAbiliity : BossAbility
{
    public float delay;
    public GameObject indicator;
    public GameObject pushBack;

    public override void AbilityBehavior(GameObject parent)
    {
        StartCoroutine(WaitAndCast(parent));
    }

     IEnumerator WaitAndCast(GameObject parent)
    {
         Vector3 displacement = new Vector3 (parent.transform.position.x, parent.transform.position.y - 0.5f, parent.transform.position.z);
        Indicator ind = Instantiate(indicator, displacement, Quaternion.identity).GetComponent<Indicator>();
        ind.timeToDisappear = delay;
        ind.transform.localScale = pushBack.transform.localScale;

        yield return new WaitForSeconds(delay);

        AudioManager.instance.PlayOneShot(FMODEvents.instance.quakeFinish, parent.transform.position);

        PushBackIndicator pb = Instantiate(pushBack, displacement, Quaternion.identity).GetComponent<PushBackIndicator>();
        pb.parent = parent;
        Destroy(this.gameObject);
    }
}
