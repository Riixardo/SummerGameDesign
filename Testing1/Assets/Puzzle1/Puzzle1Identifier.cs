using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Identifier : MonoBehaviour
{
    public int Identifier;
    public Light Light;
    void Update() {

    }
    public void ChangeLight() {
        Light.GetComponent<Light>().enabled = !Light.GetComponent<Light>().enabled;
    }
}
