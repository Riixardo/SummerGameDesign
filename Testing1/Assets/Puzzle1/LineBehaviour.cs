using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBehaviour : MonoBehaviour
{
    public LineRenderer Render;
    public Vector3 InitialVelocity;
    void Start()
    {
        Render = GetComponent<LineRenderer>();
        Render.enabled = true;
        float t;
        t = (-1f * Mathf.Abs(InitialVelocity.y)) / Physics.gravity.y;
        t = 2f * t;
        Render.positionCount = 100;
        for(int i = 0; i < Render.positionCount; i++) {
            float time = t * i / (float)(Render.positionCount);
            Vector3 trajectory = this.transform.position + InitialVelocity * time + 0.5f * Physics.gravity * time * time;
            Debug.Log(InitialVelocity * time + 0.5f * Physics.gravity * time * time);
            Render.SetPosition(i, trajectory);
        }
    }
    void FixedUpdate()
    {
    }
}
