using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CoroutineHandler : MonoBehaviour
{
    public bool reset;
    PostProcessVolume volume;
    ChromaticAberration ab;
    public Transform playerRoot;
    public GameObject monster;
    void Start() {
        playerRoot = GameObject.FindWithTag("ModelRoot").GetComponent<Transform>();
        Debug.Log(playerRoot);
    }
    IEnumerator Teleport() {
        ab = ScriptableObject.CreateInstance<ChromaticAberration>();
        ab.enabled.Override(true);
        ab.intensity.Override(1f);
        volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, ab);
        yield return new WaitForSeconds(1f);
        monster.transform.position = playerRoot.position + playerRoot.forward * 20f;
        Vector3 relPos = playerRoot.position - monster.transform.position;
        monster.transform.rotation = Quaternion.LookRotation(relPos, Vector3.up);
        RuntimeUtilities.DestroyVolume(volume, true, true);
    }
    public void StartTeleport() {
        StartCoroutine(Teleport());
    }
    public void StartFloatWait() {
       
    }
}
