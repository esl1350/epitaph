using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

class DeadSparkVFX : MonoBehaviour {
    public GameObject lightning_prefab;
    void Start() {   

    }

    private GameObject InitSpark() {
        Transform parent_transform = this.transform;
        return Instantiate(lightning_prefab, parent_transform);
    }

    IEnumerator SendSparkWithTracking(Entity origin, Entity enemy, int indexer) {
        GameObject spark = InitSpark();

        VisualEffect spark_vfx = spark.GetComponent<VisualEffect>();

        Debug.Log(spark_vfx);
        
        const int UPDATES = 30; // 60 updates per second

        spark_vfx.SetVector3("Target", enemy.transform.position);
        spark_vfx.SetVector3("Origin", origin.transform.position);
        spark_vfx.SetInt("Indexer", indexer);
        spark_vfx.SendEvent("OnPlay");

        for (int i = 0; i < UPDATES; i++) {
            if (enemy) spark_vfx.SetVector3("Target", enemy.transform.position);
            if (origin) spark_vfx.SetVector3("Origin", origin.transform.position);        
            yield return new WaitForEndOfFrame(); 
        }

        spark_vfx.SendEvent("OnStop");
        Destroy(spark);
    }

    IEnumerator SendSparkWithOriginTrackingOnly(Entity origin, Vector3 target, int indexer) {
        GameObject spark = InitSpark();

        VisualEffect spark_vfx = spark.GetComponent<VisualEffect>();
        
        const int UPDATES = 30; // 60 updates per second

        spark_vfx.SetVector3("Target", target);
        spark_vfx.SetVector3("Origin", origin.transform.position);
        spark_vfx.SetInt("Indexer", indexer);
        spark_vfx.SendEvent("OnPlay");

        for (int i = 0; i < UPDATES; i++) {
            if (origin) spark_vfx.SetVector3("Origin", origin.transform.position);        
            yield return new WaitForEndOfFrame(); 
        }

        spark_vfx.SendEvent("OnStop");
        Destroy(spark);
    }
  
    public void SendSpark(Entity origin, Entity enemy, int indexer) {
         StartCoroutine(SendSparkWithTracking(origin, enemy, indexer));
    }

    public void SendSpark(Entity origin, Vector3 target, int indexer) {
        StartCoroutine(SendSparkWithOriginTrackingOnly(origin, target, indexer));
    }


}