using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Combat
{
    protected int damage;

    public Damage(string n, int dmg) : base(n) 
    {
        damage = dmg;
    }
    public int GetDamage() {
        return damage;
    }
}
