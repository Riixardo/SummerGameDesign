using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchAppearScreen : MonoBehaviour
{
    public float Speed = 1f;
    MeshRenderer mesh;
    bool isPlaying;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Player" && !isPlaying)
        {
            isPlaying = true;
            StartCoroutine(AppearWall());
        }
    }
    IEnumerator AppearWall()
    {
        float alphaValue = 0f;
        mesh.enabled = true;
        while(alphaValue < 0.5f)
        {
            alphaValue += Time.deltaTime * Speed;
            if (alphaValue > 0.5f) alphaValue = 0.5f;
            mesh.material.color = new Color(0, 0, 0, alphaValue);
            yield return new WaitForSeconds(0.01f);
        }
        while(alphaValue > 0f)
        {
            alphaValue -= Time.deltaTime * Speed;
            if (alphaValue < 0f) alphaValue = 0f;
            mesh.material.color = new Color(0, 0, 0, alphaValue);
            yield return new WaitForSeconds(0.01f);
        }
        mesh.enabled = false;
        isPlaying = false;
    }
}
