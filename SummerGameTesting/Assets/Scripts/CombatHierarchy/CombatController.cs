using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject[] objs;
    Combat state;
    void Start()
    {
        //state = obj.AddComponent<Fireball>();
        if(state.type == Type.DAMAGE) {
            Debug.Log(((Damage)(state)).GetDamage());
        }
        Debug.Log(state);
    }
    void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            Instantiate
        }
    }
}
