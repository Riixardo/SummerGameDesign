using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject TalentTree;
    public GameObject[] obj, weapons;
    bool isEquipped = false;
    bool isMeleeAttack = false;
    GameObject cam, player, weapon;
    Melee weaponLogic;
    Vector3 weaponPlace;
    Quaternion weaponRotation, activeWeaponRotation;
    Combat state;
    Rigidbody rigid;
    void Start()
    {
        cam = GameObject.FindWithTag("ModelRoot");
        player = this.gameObject;
        weaponLogic = GetComponentInChildren<Melee>();
        weapon = weaponLogic.gameObject.transform.GetChild(0).gameObject;
        weaponPlace = weapon.transform.localPosition;
        weaponRotation = weapon.transform.localRotation;
        activeWeaponRotation = weaponRotation * Quaternion.Euler(0f, 0f, 100f);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(!isEquipped)
            {
                weapon.transform.localPosition = new Vector3(weaponPlace.x, weaponPlace.y, weaponPlace.z * -3f);
                weapon.transform.localRotation = activeWeaponRotation;
                isEquipped = true;
            }
            else if(isEquipped)
            {
                weapon.transform.localPosition = weaponPlace;
                weapon.transform.localRotation = weaponRotation;
                isEquipped = false;
            }
        }
        if(Input.GetMouseButtonDown(0) && isEquipped && !isMeleeAttack)
        {
            //weaponLogic.StartThreeSixtySlashing();
            weaponLogic.StartNormalSlashing();
            isMeleeAttack = true;
        }
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
        if(Input.GetKeyDown("g"))
        {
            StartCoroutine(StartBeam());
        }
    }
    public void MeleeAttackOff()
    {
        isMeleeAttack = false;
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
    IEnumerator StartBeam()
    {
        Vector3 v = cam.transform.position + cam.transform.forward * 15f;
        for (int i = 1; i < 8; i++)
        {
            GameObject o = Instantiate(obj[5], v + new Vector3(0f, 8f * i, 0f), this.transform.rotation);
            o.AddComponent<BeamCircle>();
            BeamCircle c = o.GetComponent<BeamCircle>();
            switch (i)
            {
                case 1: c.size = 15;
                    c.Lifespan = 5f;
                    break;
                case 2: c.size = 20;
                    c.Lifespan = 4.7f;
                    break;
                case 3: c.size = 23;
                    c.Lifespan = 4.4f;
                    break;
                case 4: c.size = 25;
                    c.Lifespan = 4.1f;
                    break;
                case 5: c.size = 23;
                    c.Lifespan = 3.8f;
                    break;
                case 6: c.size = 20;
                    c.Lifespan = 3.5f;
                    break;
                case 7: c.size = 15;
                    c.Lifespan = 3.2f;
                    c.CreateBeam = true;
                    break;
                default: break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void CreateBeam(Vector3 position)
    {
        GameObject o = Instantiate(obj[6], position + new Vector3(0f, -28f, 0f), this.transform.rotation);
        o.AddComponent<FallenBeam>();
    }
}
