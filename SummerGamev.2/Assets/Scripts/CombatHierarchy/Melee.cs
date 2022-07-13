using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float slashAngle = 270f;
    public Collider coll;
    public CombatController c;
    public float slashRate = 4;
    [SerializeField]
    private bool isSlashing = false;
    void Start()
    {

    }
    public void StartThreeSixtySlashing()
    {
        StartCoroutine(ThreeSixtySlashCoroutine());
    }
    public void StartNormalSlashing()
    {
        StartCoroutine(NormalSlash());
    }
    IEnumerator ThreeSixtySlashCoroutine()
    {
        isSlashing = true;
        Quaternion startRotation = transform.localRotation;
        float endZRot = 270f;
        float duration = 1f;
        float t = 0;
        float time;
        while (t < 1f)
        {
            Debug.Log("slashing. " + transform.localRotation.eulerAngles);
            t += Time.deltaTime * slashRate;
            time = Mathf.Min(1f, t + Time.deltaTime / duration);
            Debug.Log(time);
            Vector3 newEulerOffset = new Vector3(0, 1, 0) * (endZRot * t);
            // global z rotation
            // transform.localRotation = Quaternion.Euler(newEulerOffset) * startRotation;
            // local z rotation
            transform.localRotation = startRotation * Quaternion.Euler(newEulerOffset);
            yield return null;
        }
        transform.localRotation = startRotation;
        isSlashing = false;
        c.MeleeAttackOff();
    }
    IEnumerator NormalSlash()
    {
        isSlashing = true;
        Quaternion startRotation = transform.localRotation;
        float endXRot = 90f;
        float duration = 1f;
        float t = 0;
        while (t < 1f)
        {
            Debug.Log("slashing. " + transform.localRotation.eulerAngles);
            t += Time.deltaTime * slashRate;
            Vector3 newEulerOffset = new Vector3(1, 0, 0) * (endXRot * t);
            // global z rotation
            // transform.localRotation = Quaternion.Euler(newEulerOffset) * startRotation;
            // local z rotation
            transform.localRotation = startRotation * Quaternion.Euler(newEulerOffset);
            yield return null;
        }
        transform.localRotation = startRotation;
        isSlashing = false;
        c.MeleeAttackOff();
    }
}