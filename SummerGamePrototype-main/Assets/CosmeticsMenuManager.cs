using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class CosmeticsMenuManager : MonoBehaviour
{
    // UI Control
    public GameObject cosmeticsCanvas;

    // Player camera control manager
    public ThirdPersonCameraController playerCamController;
    public Camera playerCam;
    public Camera cosmeticsCam;


    // Start is called before the first frame update
    void Start()
    {
        cosmeticsCanvas.active = false;
        playerCamController = (ThirdPersonCameraController)FindObjectOfType(typeof(ThirdPersonCameraController));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            OpenDressingRoom();
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CloseDressingRoom();
        }
    }

    public void OpenDressingRoom()
    {
        cosmeticsCanvas.active = true;
        playerCam.enabled = false;
        cosmeticsCam.enabled = true;
        playerCamController.enabled = false;
    }

    public void CloseDressingRoom()
    {
        cosmeticsCanvas.active = false;
        playerCam.enabled = true;
        cosmeticsCam.enabled = false;
        playerCamController.enabled = true;
    }


}
