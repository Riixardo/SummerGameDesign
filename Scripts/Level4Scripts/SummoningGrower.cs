using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class SummoningGrower : MonoBehaviour
{
    Transform t;
    bool startSummoning, isDone;
    List<GameObject> mimics = new List<GameObject>();
    public GameObject[] enemies;
    void Start() {
        t = this.transform;
        StartCoroutine(Grow());
    }
    void Update() {
        if(startSummoning && !isDone) {
            mimics.Add(Instantiate(enemies[0], this.transform.position + new Vector3(15f, 1f, 0f), new Quaternion()));
            mimics.Add(Instantiate(enemies[0], this.transform.position + new Vector3(-15f, 1f, 0f), new Quaternion()));
            mimics.Add(Instantiate(enemies[0], this.transform.position + new Vector3(0f, 1f, 15f), new Quaternion()));
            mimics.Add(Instantiate(enemies[0], this.transform.position + new Vector3(0f, 1f, -15f), new Quaternion()));
            isDone = true;
        }
        if(checkIfMimicsDead() && isDone) {
            BossState.mimicsDead = true;
            Destroy(gameObject);
        }
    }
    IEnumerator Grow() {
        for(int i = 0; i < 50; i++) {
            t.localScale = new Vector3(t.localScale.x * 1.03f, t.localScale.y, t.localScale.z * 1.03f);
            t.Rotate(0f, 10f, 0f);
            yield return new WaitForSeconds(0.05f);
        }
        startSummoning = true;
    }
    bool checkIfMimicsDead() {
        foreach (GameObject o in mimics) {
            if(o != null) {
                return false;
            }
        }
        return true;
    }
}
