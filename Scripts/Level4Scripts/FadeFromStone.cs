using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeFromStone : MonoBehaviour
{
    public GameObject Origin;
    public GameObject Real;
    Renderer mesh;
    GameObject player;
    public bool isDone;
    void Start()
    {
        mesh = GetComponent<Renderer>();
        player = GameObject.Find("Player");
    }
    void Update()
    {
        //Debug.Log((Origin.transform.position - player.transform.position).magnitude);
        if ((Origin.transform.position - player.transform.position).magnitude < 90f && !isDone) 
        {
            StartCoroutine(BeginFade());
            isDone = true;
        }
    }
    IEnumerator BeginFade()
    {
        float alpha = 1f;
        while(alpha > 0)
        {
            alpha -= Time.deltaTime;
            if (alpha < 0f) alpha = 0f;
            mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, alpha);
            yield return new WaitForSeconds(0.01f);
        }
        Origin.SetActive(false);
        Real.SetActive(true);
    }
}
