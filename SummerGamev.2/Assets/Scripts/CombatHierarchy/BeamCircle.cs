using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCircle : MonoBehaviour
{
    GameObject player;
    public int size;
    public float Lifespan;
    public bool CreateBeam = false;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Destroy(gameObject, Lifespan);
        StartCoroutine(Grow());
    }
    void Update()
    {
    }
    IEnumerator Grow()
    {
        int iteration = 0;
        while (iteration < size)
        {
            this.transform.localScale = this.transform.localScale * 1.1f;
            iteration++;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.3f);
        if(CreateBeam)
        {
            player.GetComponent<CombatController>().CreateBeam(this.transform.position);
        }
    }
}
