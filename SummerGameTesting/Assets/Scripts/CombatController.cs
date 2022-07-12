using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public Rigidbody r;
    public GameObject obj;
    Combat state;
    void Start()
    {
        state = obj.AddComponent<Fireball>();
        //Debug.Log(state.GetDamage());
        Debug.Log(state);
    }
    void Update()
    {
        
    }
}
