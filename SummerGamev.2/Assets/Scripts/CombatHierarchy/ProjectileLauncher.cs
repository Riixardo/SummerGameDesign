using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CMF
{
    public class ProjectileLauncher : MonoBehaviour
    {
        public Camera view;
        public Transform crosshair;
        public GameObject Projectile;
        public GameObject projectileCasing;
        public Animator anim;
        public AnimationClip shoot;
        public float fireDelay = 0.2f; // delay.
        public float nextFireTime;
        public float bulletSpeed = 4;
        public LayerMask whatIsPlayer;
        public Transform muzzlePosition;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            RaycastHit hit;
            Physics.Raycast(view.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out hit, float.PositiveInfinity, ~whatIsPlayer);
            //Debug.DrawRay(view.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1)), transform.forward);

            //Debug.Log(hit.point);
            //transform.rotation = Quaternion.Lerp( transform.rotation, cam.rotation, Time.deltaTime * 10);

            transform.LookAt(hit.point);

            if (Input.GetMouseButtonDown(0) && Time.time > nextFireTime)
            {
                Debug.Log("fired");

                nextFireTime = Time.time + fireDelay;
                GameObject firedProjectile = Instantiate(Projectile, muzzlePosition.position,muzzlePosition.rotation);
                firedProjectile.GetComponent<Projectile>().InitialVelocity = transform.forward * bulletSpeed;
            }


        
        
        }
    }
}
