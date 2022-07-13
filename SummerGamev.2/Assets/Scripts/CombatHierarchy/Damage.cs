using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Combat
{
    protected int damage;

    public Damage(string n, Type type, int dmg) : base(n, type) 
    {
        damage = dmg;
    }
    public int GetDamage() {
        return damage;
    }
}
