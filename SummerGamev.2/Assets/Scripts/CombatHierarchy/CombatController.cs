using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject TalentTree;
    public GameObject[] obj, weapons;
    public float LightningDistance, LightningHeight, BeamDistance;
    public float FireballCooldown, WindBladesCooldown, IceSpikesCooldown, LightningCooldown, LightBeamCooldown;
    float FC, WC, IC, LC, LBC;
    bool isEquipped = false;
    bool isMeleeAttack = false;
    int weaponIndex = 1;
    GameObject cam, player, weapon, weaponControls, rightArm;
    Melee weaponLogic;
    Vector3 weaponPlace;
    Quaternion weaponRotation, activeWeaponRotation;
    Combat state;
    Rigidbody rigid;
    void Start()
    {
        cam = GameObject.FindWithTag("ModelRoot");
        rightArm = GameObject.FindWithTag("RightArm");
        player = this.gameObject;
        weaponLogic = GetComponentInChildren<Melee>();
        weaponControls = weaponLogic.gameObject;
        weapon = weaponControls.transform.GetChild(0).gameObject;
        weaponPlace = weapon.transform.localPosition;
        weaponRotation = weapon.transform.localRotation;
        activeWeaponRotation = weaponRotation * Quaternion.Euler(0f, -90f, 0f);
    }
    void Update()
    {
        UpdateCooldown();
        EquipUnequipWeapon();
        WeaponAttackLogic();
        if(Input.GetKeyDown("q") && !isEquipped) {
            weaponIndex++;
            if(weaponIndex >= weapons.Length) {
                weaponIndex = 0;
            }
            Destroy(weapon);
            GameObject temp = Instantiate(weapons[weaponIndex], weaponControls.transform.position, weaponControls.transform.rotation * weaponRotation, weaponControls.transform);
            weapon = null;
            weapon = temp;
            weaponLogic.UpdateChild(weapon);
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
    void UpdateCooldown()
    {
        if (FireballCooldown > 0f)
        {
            FireballCooldown = FireballCooldown - Time.deltaTime;
        }
        else FireballCooldown = 0f;
        if (WindBladesCooldown > 0f)
        {
            WindBladesCooldown = WindBladesCooldown - Time.deltaTime;
        }
        else WindBladesCooldown = 0f;
        if (IceSpikesCooldown > 0f)
        {
            IceSpikesCooldown = IceSpikesCooldown - Time.deltaTime;
        }
        else IceSpikesCooldown = 0f;
        if (LightningCooldown > 0f)
        {
            LightningCooldown = LightningCooldown - Time.deltaTime;
        }
        else LightningCooldown = 0f;
        if (LightBeamCooldown > 0f)
        {
            LightBeamCooldown = LightBeamCooldown - Time.deltaTime;
        }
        else LightBeamCooldown = 0f;
    }
    public void MeleeAttackOff()
    {
        isMeleeAttack = false;
    }
    void EquipUnequipWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !weaponLogic.isSlashing)
        {
            if (!isEquipped)
            {
                if (weapon.tag == "Sword")
                {
                    weapon.transform.localPosition = new Vector3(1.1f, 0.44f, 0.62f);
                    weapon.transform.localRotation = activeWeaponRotation;
                    isEquipped = true;
                }
                else if (weapon.tag == "Spear")
                {
                    weapon.transform.localPosition = new Vector3(1.1f, 0.44f, 0.62f);
                    weapon.transform.localRotation = Quaternion.Euler(0f, -90.7f, -55f);
                    isEquipped = true;
                }
                else if (weapon.tag == "Axe")
                {
                    weapon.transform.localPosition = new Vector3(1.1f, 0.11f, 0.45f);
                    weapon.transform.localRotation = activeWeaponRotation * Quaternion.Euler(30f, 60f, 20f);
                    isEquipped = true;
                }
            }
            else if (isEquipped)
            {
                weapon.transform.localPosition = weaponPlace;
                weapon.transform.localRotation = weaponRotation;
                isEquipped = false;
            }
        }
    }
    void WeaponAttackLogic()
    {
        if (Input.GetMouseButtonDown(0) && isEquipped && !isMeleeAttack)
        {
            if (weapon.tag == "Sword")
            {
                //weaponLogic.StartThreeSixtySlashing();
                weaponLogic.StartNormalSlashing();
            }
            else if (weapon.tag == "Spear")
            {
                weaponLogic.StartSpearThrust();
            }
            if (weapon.tag == "Axe")
            {
                //weaponLogic.StartThreeSixtySlashing();
                weaponLogic.StartAxeSlashing();
            }
            isMeleeAttack = true;
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
            o.AddComponent<WindBlades>();
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
        GameObject o = Instantiate(obj[3], cam.transform.position + cam.transform.forward * LightningDistance + new Vector3(0f, LightningHeight, 0f), this.transform.rotation);
        o.AddComponent<LightningCloud>();
        o.GetComponent<LightningCloud>().LightningPosition = cam.transform.position + cam.transform.forward * LightningDistance + new Vector3(0f, LightningHeight * 0.5f, 0f);
    }
    public void CreateLightning(Vector3 LightningPosition) {
        GameObject o = Instantiate(obj[4], LightningPosition, this.transform.rotation);
        o.transform.localScale = new Vector3(o.transform.localScale.x, LightningHeight, o.transform.localScale.z);
        o.AddComponent<LightningStrike>();
        for(int i = 0; i < 2; i++) {
            GameObject o1 = Instantiate(obj[4], LightningPosition + new Vector3(Random.Range(-4f, 4f), 0f, Random.Range(-4f, 4f)), this.transform.rotation);
            o1.transform.localScale = new Vector3(o.transform.localScale.x, LightningHeight, o.transform.localScale.z);
            o1.AddComponent<LightningStrike>();
        }
    }
    IEnumerator StartBeam()
    {
        Vector3 v = cam.transform.position + cam.transform.forward * BeamDistance;
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
