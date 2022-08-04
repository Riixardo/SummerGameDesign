using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfBossDead : MonoBehaviour
{
    public GameObject Boss;
    public GameObject Crown;
    public GameObject Placeholder;
    bool isDone;
    void Update()
    {
        if(Boss == null && !isDone) {
            Instantiate(Crown, Placeholder.transform.position, new Quaternion());
            isDone = true;
        }
    }
}
