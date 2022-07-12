using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour{

    public string Name;
    protected Rigidbody r;
    protected GameObject o;
    
    public Combat (string n) {
        Name = n;
    }
    public Combat() {

    }
}
