using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YOUWIN : MonoBehaviour
{
    public GameObject youWin;
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            Instantiate(youWin, new Vector3(0, 0, 0), new Quaternion());
        }
    }
}
