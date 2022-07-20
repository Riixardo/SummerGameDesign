using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaTest : MonoBehaviour
{
    public Slider manaSlider;
    public float refillSpeed;
    public bool refilling;
    public KeyCode MapedKey;


    public PlayerStatsManager stm;
    // Start is called before the first frame update
    void Start()
    {
        manaSlider = GetComponent<Slider> ();
        stm = GameObject.Find("StatManager").GetComponent<PlayerStatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        manaSlider.value = (float)stm.mana / stm.maxMana;
    }
}
