using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackArtifact : MonoBehaviour
{
    public Transform artifact;
    public Transform player;
    Transform modelRoot;
    bool isDone;
    static int complete = 0;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        modelRoot = GameObject.Find("ModelRoot").transform;
        if(!isDone)
        {
            checkPlayerPickUp();
            checkArtifactPutDown();
            if(complete == 4)
            {
                GameObject door = GameObject.Find("BossDoor");
                if(door != null)
                {
                    door.SetActive(false);
                }
            }
        }
    }
    void checkPlayerPickUp()
    {
        Vector3 dist = player.position - artifact.position;
        if(dist.magnitude < 3f)
        {
            artifact.parent = modelRoot;
        }
    }
    void checkArtifactPutDown()
    {
        Vector3 dist = this.transform.position - artifact.position;
        if (dist.magnitude < 5f)
        {
            isDone = true;
            complete++;
            artifact.parent = null;
            artifact.position = this.transform.position;
        }
    }
}
