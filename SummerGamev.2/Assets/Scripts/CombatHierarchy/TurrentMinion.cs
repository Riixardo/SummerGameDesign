using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMinion : MonoBehaviour
{

    public Transform turretModel;
    public float rotateSpeed = 10;
    public float attackInterval = 1;
    public GameObject projectile;
    public float detectionRange = 40;
    public Transform playerTransform;
    private float nextAttackTime;
    public float projectileSpeed = 200;
    void Start()
    {
        nextAttackTime = Time.time;
        playerTransform = GameObject.Find("PlayerCenter").transform;
    }
    /*
    public void Attack()
    {
        GameObject proj = Instantiate(projectile, transform.position, new Quaternion());
        proj.GetComponent<Projectile>().initialVelocity = (playerTransform.position - proj.transform.position).normalized * projectileSpeed;
    }
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, playerTransform.position - transform.position, out hit, detectionRange);
        if (hit.collider)
        {
            if (hit.collider.gameObject.GetComponentInParent<Mover>() || hit.collider.gameObject.GetComponent<Mover>())
                turretModel.rotation = Quaternion.Slerp(turretModel.rotation, Quaternion.LookRotation(playerTransform.position - turretModel.position), Time.deltaTime * rotateSpeed);
        }
        if (Time.time >= nextAttackTime && hit.collider)
        {

            if (hit.collider.gameObject.GetComponentInParent<Mover>() || hit.collider.gameObject.GetComponent<Mover>())
                Attack();
                nextAttackTime = Time.time + attackInterval;
        }
    } */
}