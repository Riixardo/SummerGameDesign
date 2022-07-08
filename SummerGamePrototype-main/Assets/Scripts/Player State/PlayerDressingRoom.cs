using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDressingRoom : MonoBehaviour
{
    public Material[] bodyMaterials;
    public Material[] headMaterials;
    public Material[] armMaterials;

    public int currBodyMatNum;
    public int currHeadMatNum;
    public int currArmMatNum;

    public SkinnedMeshRenderer body;
    public SkinnedMeshRenderer head;
    public MeshRenderer[] arms;

    private void Start()
    {
        currArmMatNum = 0;
        currBodyMatNum = 0;
        currHeadMatNum = 0;
    }

    // Start is called before the first frame update
    public void NextBodyMat()
    {
        currBodyMatNum++;
        if(currBodyMatNum >= bodyMaterials.Length)
        {
            currBodyMatNum = 0;
        }
        UpdateBodyMaterials();
    }

    public void NextHeadMat()
    {
        currHeadMatNum++;
        if(currHeadMatNum >= headMaterials.Length)
        {
            currHeadMatNum = 0;
        }
        UpdateHeadMaterials();
    }

    public void NextArmMat()
    {
        currArmMatNum++;
        if(currArmMatNum >= armMaterials.Length)
        {
            currArmMatNum = 0;
        }
        UpdateArmMaterials();
    }

    void UpdateBodyMaterials()
    {
        Material[] mats = body.materials;
        mats[1] = bodyMaterials[currBodyMatNum];
        body.materials = mats;
    }

    void UpdateHeadMaterials()
    {
        Material[] mats = body.materials;
        mats[0] = headMaterials[currHeadMatNum];
        body.materials = mats;
    }

    void UpdateArmMaterials()
    {
        arms[0].material = armMaterials[currArmMatNum];
        arms[1].material = armMaterials[currArmMatNum];
    }
}
