using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject TalentTree;
    public GameObject[] obj;
    GameObject cam, player;
    Combat state;
    Rigidbody rigid;
    void Start()
    {
        cam = GameObject.FindWithTag("ModelRoot");
        player = this.gameObject;
    }
    void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            StartFireball();
        }
        if(Input.GetKeyDown("r"))
        {
            StartCoroutine(StartWindBlades());
        }
        if(Input.GetKeyDown("t")) {
            StartIceSpikes();
        }
        if(Input.GetKeyDown("e")) {
            StartLightning();
        }
    }
    void StartFireball()
    {
        GameObject o = Instantiate(obj[0], cam.transform.position + cam.transform.forward * 3f + new Vector3(0f, 3f, 0f), this.transform.rotation);
        o.AddComponent<Fireball>();
        o.GetComponent<Rigidbody>().useGravity = false;
    }
    IEnumerator StartWindBlades()
    {
        for(int i = 0; i < 3; i++)
        {
            Quaternion rotate = Quaternion.Euler(Random.Range(-45f, 45f), this.transform.rotation.y + 90f, Random.Range(-45f, 45f));
            GameObject o = Instantiate(obj[1], cam.transform.position + cam.transform.forward * 3f + new Vector3(0f, 3f, 0f), rotate);
            o.AddComponent<Fireball>();
            o.GetComponent<Rigidbody>().useGravity = false;
            yield return new WaitForSeconds(0.2f);
        }
    }
    void StartIceSpikes() {
        for(int i = -2; i < 3; i++) {
            GameObject o = Instantiate(obj[2], cam.transform.position + cam.transform.forward * 3f + new Vector3(0f, 3f, 0f) + cam.transform.right * i * 2, this.transform.rotation);
            o.AddComponent<IceSpikes>();
            o.GetComponent<Rigidbody>().useGravity = false;
        }
    }
    void StartLightning() {
        GameObject o = Instantiate(obj[3], cam.transform.position + cam.transform.forward * 15f + new Vector3(0f, 15f, 0f), this.transform.rotation);
        o.AddComponent<LightningCloud>();
        o.GetComponent<LightningCloud>().LightningPosition = cam.transform.position + cam.transform.forward * 15f + new Vector3(0f, 7.5f, 0f);
    }
    public void CreateLightning(Vector3 LightningPosition) {
        GameObject o = Instantiate(obj[4], LightningPosition, this.transform.rotation);
        o.AddComponent<LightningStrike>();
        for(int i = 0; i < 2; i++) {
            GameObject o1 = Instantiate(obj[4], LightningPosition + new Vector3(Random.Range(-4f, 4f), 0f, Random.Range(-4f, 4f)), this.transform.rotation);
            o1.AddComponent<LightningStrike>();
        }
    }
}
