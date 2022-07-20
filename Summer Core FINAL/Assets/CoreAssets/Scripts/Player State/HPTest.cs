using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPTest : MonoBehaviour
{
    public Slider healthSlider;
    public float refillSpeed;
    public bool refilling;

    public PlayerStatsManager stm;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GetComponent<Slider> ();
        stm = GameObject.Find("StatManager").GetComponent<PlayerStatsManager>();
    }

    // Update is called once per frame
    void Update()
    {

        healthSlider.value = (float)stm.health / stm.maxHealth;

        
    }
}
