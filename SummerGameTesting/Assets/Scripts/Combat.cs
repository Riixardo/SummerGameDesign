using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour{

    public Type type;
    public string Name;
    protected Rigidbody r;
    protected GameObject o;
    
    public Combat (string n, Type type) {
        Name = n;
        this.type = type;
    }
    public Combat() {

    }
}
