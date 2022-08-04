using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CMF
{
    public class LightningCloud : MonoBehaviour
    {
        public Vector3 LightningPosition;
        CombatController combatController;
        private int iteration;
        void Start()
        {
            combatController = GameObject.Find("Player").GetComponent<CombatController>();
            StartCoroutine(Grow());
        }
        IEnumerator Grow()
        {
            while (iteration < 25)
            {
                this.transform.localScale = this.transform.localScale * 1.02f;
                iteration++;
                yield return new WaitForSeconds(0.1f);
            }
            
            combatController.CreateLightning(LightningPosition);
            yield return new WaitForSeconds(0.2f);
            combatController.CreateLightning(LightningPosition);
            yield return new WaitForSeconds(0.2f);
            combatController.CreateLightning(LightningPosition);
            yield return new WaitForSeconds(0.2f);
            combatController.CreateLightning(LightningPosition);
            
            Destroy(gameObject, 1);
        }
    }
}