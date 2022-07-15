using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenBeam : Damage
{
    private int damage = 50;
    public FallenBeam() : base("FallenBeam", Type.MAGIC)
    {
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Destroy(gameObject, 1.2f);
        StartCoroutine(Grow());
    }
    IEnumerator Grow() {
        while(enabled) {
            this.transform.localScale = this.transform.localScale * 1.2f;
            yield return new WaitForSeconds(0.05f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("LOLOLOL");
        if (other.GetComponent<TakeDamage>())
        {
            other.GetComponent<TakeDamage>().LowerHealth(damage);
        }
    }
}
