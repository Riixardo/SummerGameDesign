using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenBeam : Damage
{
    public int Damage = 50;
    public float scaleMultiplier = 1.2f;
    private GameObject talentUI, player;
    TalentSystem talent;
    public FallenBeam() : base("FallenBeam", Type.MAGIC)
    {
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        talentUI = player.GetComponent<CombatController>().GetTalentSystem();
        Damage += talentUI.GetComponent<TalentSystem>().magicDmgProgressionState;
        Destroy(gameObject, 1.2f);
        StartCoroutine(Grow());
    }
    IEnumerator Grow() {
        while(enabled) {
            this.transform.localScale = this.transform.localScale * scaleMultiplier;
            yield return new WaitForSeconds(0.05f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("LOLOLOL");
        if (other.GetComponent<TakeDamage>())
        {
            other.GetComponent<TakeDamage>().LowerHealth(Damage);
        }
    }
}
