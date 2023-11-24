using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TalismanSigil : MonoBehaviour
{
    [SerializeField] protected List<StatusEffect> _statusEffects;
    [SerializeField] protected List<StatusEffect> _removeEffects;

    [SerializeField] protected float damage;
    [SerializeField] private float expiryTime;
    [SerializeField] private GameObject sigil;
    private float timer;
    private bool bothSigilsSet;
    protected Player parent;
    private TalismanLine line;

    public bool BothSigilsSet {
        get {return bothSigilsSet;}
    }
    public Player Parent {
        get {return parent;}
        set {value = parent;}
    }

    // Start is called before the first frame update
    void Start()
    {
        bothSigilsSet = false;
        timer = 0f;
        line = this.transform.GetChild(0).gameObject.GetComponent<TalismanLine>();
        line.Damage = damage;
        line.StatusEffects = _statusEffects;
        line.RemoveEffects = _removeEffects;

    }

    // Update is called once per frame
    void Update()
    {
        if (bothSigilsSet) {
            timer += Time.deltaTime;
        }
        if (timer > expiryTime) {
            line.TriggerDestruction();
        }
    }



    public void AddNewSigil(Vector3 pos) {
        GameObject newSigil = Instantiate(sigil, pos, Quaternion.identity, this.transform);
        bothSigilsSet = true;
        line.AlignSprite(this.transform.GetChild(1).position, this.transform.GetChild(2).position);
    }

}
