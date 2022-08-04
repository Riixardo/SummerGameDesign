using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class LightCircleGrower : MonoBehaviour
{
    Transform t;
    bool startRaining, isDone;
    List<GameObject> rain = new List<GameObject>();
    public GameObject lightBlade;
    void Start()
    {
        t = this.transform;
        StartCoroutine(Grow());
    }
    void Update()
    {
        if(startRaining && !isDone) {
            StartCoroutine(Rainfall());
            isDone = true;
        }
    }
    IEnumerator Grow() {
        for(int i = 0; i < 80; i++) {
            t.localScale = new Vector3(t.localScale.x * 1.05f, t.localScale.y, t.localScale.z * 1.05f);
            yield return new WaitForSeconds(0.05f);
        }
        startRaining = true;
    }
    IEnumerator Rainfall() {
        for(int i = 0; i < 500; i++) {
            for(int j = 0; j < 10; j++) {
                var o = Instantiate(lightBlade, this.transform.position + new Vector3(Random.Range(-1 * GetComponent<Collider>().bounds.size.x / 2, GetComponent<Collider>().bounds.size.x / 2), 0f, Random.Range( -1 * GetComponent<Collider>().bounds.size.z / 2, GetComponent<Collider>().bounds.size.z / 2)), new Quaternion());
                rain.Add(o);
            }
            yield return null;
        }
        foreach (GameObject o in rain) {
            o.GetComponent<Rigidbody>().velocity = Vector3.down * 150f;
        }
        yield return new WaitForSeconds(1f);
        BossState.ultimateIsDone = true;
        Destroy(gameObject);
    }
}
