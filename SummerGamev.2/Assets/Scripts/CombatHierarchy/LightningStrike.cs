using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : Damage
{
    private int iteration = 0;
    public LightningStrike() : base("Lightning", Type.MAGIC, 10) 
    {
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        obj = this.gameObject;
        StartCoroutine(Grow());
    }
    void Update()
    {
    }
    IEnumerator Grow() {
        while(iteration < 25) {
            this.transform.localScale = this.transform.localScale * 1.1f;
            iteration++;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
