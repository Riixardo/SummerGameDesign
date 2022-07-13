using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public Rigidbody rigid;
    public GameObject[] obj;
    GameObject cam, player;
    Combat state;
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
        GameObject o1 = Instantiate(obj[2], cam.transform.position + cam.transform.forward * 3f + new Vector3(0f, 3f, 0f) + cam.transform.right * -4f, this.transform.rotation);
        GameObject o2 = Instantiate(obj[2], cam.transform.position + cam.transform.forward * 3f + new Vector3(0f, 3f, 0f) + cam.transform.right * -2f, this.transform.rotation);
        GameObject o3 = Instantiate(obj[2], cam.transform.position + cam.transform.forward * 3f + new Vector3(0f, 3f, 0f), this.transform.rotation);
        GameObject o4 = Instantiate(obj[2], cam.transform.position + cam.transform.forward * 3f + new Vector3(0f, 3f, 0f) + cam.transform.right * 2f, this.transform.rotation);
        GameObject o5 = Instantiate(obj[2], cam.transform.position + cam.transform.forward * 3f + new Vector3(0f, 3f, 0f) + cam.transform.right * 4f, this.transform.rotation);
        o1.AddComponent<IceSpikes>();
        o1.GetComponent<Rigidbody>().useGravity = false;
        o2.AddComponent<IceSpikes>();
        o2.GetComponent<Rigidbody>().useGravity = false;
        o3.AddComponent<IceSpikes>();
        o3.GetComponent<Rigidbody>().useGravity = false;
        o4.AddComponent<IceSpikes>();
        o4.GetComponent<Rigidbody>().useGravity = false;
        o5.AddComponent<IceSpikes>();
        o5.GetComponent<Rigidbody>().useGravity = false;
    }
    void StartLightning() {
        GameObject o = Instantiate(obj[3], cam.transform.position + cam.transform.forward * 10f + new Vector3(0f, 10f, 0f), this.transform.rotation);
        o.AddComponent<LightningStrike>();
    }
}
