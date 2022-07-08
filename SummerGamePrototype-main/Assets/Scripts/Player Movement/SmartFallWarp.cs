using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartFallWarp : MonoBehaviour
{
    GameObject player;
    public int warpHeight;
    Vector3 respawnPoint;
    bool grounded = false;

    private void Start()
    {
        player = GameObject.Find("SummerPlayer3P");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
            Debug.Log("Respawn point set successfully!");
            respawnPoint = gameObject.transform.position + new Vector3(0f, 0f, 0f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    /*public void SmartWarpWaypointSet()
    {
        if(grounded)
        {
            Debug.Log("Respawn point set successfully!");
            respawnPoint = gameObject.transform.position + new Vector3(0f, 123.0f, 0f);
        }  
    }*/

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y <= warpHeight)
        {
            player.transform.position = respawnPoint;
        }
    }
}
