using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CustomObjectMovement : MonoBehaviour
{
    /*
        Just attach this script to any game object to allow for custom movement, provided it has a trigger and that
        the player enters and presses a key (key is customizable)

        Script can be used for infinite movement, however, if toggled all children must have it toggled on or else there will be asynchronous motion
    */

    public bool IsControlMaster = true;
    public bool IsMoving = true;
    public TMP_Text floatingText;
    public bool ProximityBased = false;
    public string InputKey;
    public string ObjectName;
    public float MovementIncrement = 0.01f;
    public bool MoveForever = false;
    public float MovementTime = 2.0f;
    public bool RotateBackwards = false;
    public bool ObjectMovementReset = false;
    public float RotateValue, LeftValue, RightValue, UpValue, DownValue;

    bool inRangeToMove = false;
    bool animationPlaying = false;
    bool resetAfter = false;
    bool moveForever = false;
    bool isKeyDown = false;
    CustomObjectMovement[] movObjs;

    private void Start()
    {
        movObjs = GetComponentsInChildren<CustomObjectMovement>();
        if(floatingText != null)
        {
            Debug.Log("LOL");
            floatingText.text = "Press '" + InputKey.ToUpper() + "' to interact with " + ObjectName + " !";
            floatingText.enabled = false;
        }
        if(ObjectMovementReset)
        {
            resetAfter = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRangeToMove = true;
            if (floatingText != null)
            {
                floatingText.enabled = true;
            }
            if(ProximityBased)
            {
                if (IsMoving)
                {
                    if (MoveForever)
                    {
                        moveForever = !moveForever;
                    }
                    StartCoroutine(PlayMoveTranslation(resetAfter));
                    resetAfter = !resetAfter;
                    if (floatingText != null)
                    {
                        floatingText.enabled = false;
                    }
                }
                else
                {
                    for (int i = 0; i < movObjs.Length; i++)
                    {
                        if (movObjs[i].GetMasterAndMove())
                        {
                            if (movObjs[i].GetMoveForever())
                            {
                                movObjs[i].ChangeMoveForever();
                            }
                            else if (!(movObjs[i].GetAnimationPlaying()))
                            {
                                movObjs[i].ThirdPartyAccess();
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRangeToMove = false;
            if (floatingText != null)
            {
                floatingText.enabled = false;
            }
            if(ProximityBased && MoveForever)
            {
                ChangeMoveForever();
            }
        }
    }
    public bool GetMasterAndMove()
    {
        return (!IsControlMaster) && IsMoving && (!animationPlaying || moveForever);
    }
    public bool GetMoveForever()
    {
        return moveForever;
    }
    public bool GetAnimationPlaying()
    {
        return animationPlaying;
    }
    public void ChangeMoveForever()
    {
        moveForever = !moveForever;
        animationPlaying = false;
        if (inRangeToMove)
        {
            if (floatingText != null)
            {
                floatingText.enabled = false;
            }
        }
    }
    void Update()
    {
        if(inRangeToMove)
        {
            if (!InputKey.Equals(null) && animationPlaying)
            {
                if (Input.GetKeyDown(InputKey))
                {
                    isKeyDown = true;
                }
            }
            if ((Input.GetKeyDown(InputKey) && !animationPlaying) && IsControlMaster)
            {
                if(IsMoving)
                {
                    if(MoveForever)
                    {
                        moveForever = !moveForever;
                    }
                    StartCoroutine(PlayMoveTranslation(resetAfter));
                    resetAfter = !resetAfter;
                    if (floatingText != null)
                    {
                        floatingText.enabled = false;
                    }
                }
                else
                {                   
                    for (int i = 0; i < movObjs.Length; i++)
                    {
                        if (movObjs[i].GetMasterAndMove())
                        {
                            if(movObjs[i].GetMoveForever())
                            {
                                movObjs[i].ChangeMoveForever();
                            }
                            else if(!(movObjs[i].GetAnimationPlaying()))
                            {
                                movObjs[i].ThirdPartyAccess();
                            }
                        }
                    }
                }
            }
        }
    }
    public void ThirdPartyAccess()
    {
        if (MoveForever)
        {
            moveForever = !moveForever;
        }
        StartCoroutine(PlayMoveTranslation(resetAfter));
        resetAfter = !resetAfter;
        if (floatingText != null)
        {
            floatingText.enabled = false;
        }
    }
    IEnumerator PlayMoveTranslation(bool resetAfter)
    {
        animationPlaying = true;
        float timer = 0f;
        float timeLimit = MovementTime / MovementIncrement;
        float slidingLeftDelay = LeftValue / timeLimit;
        float slidingRightDelay = RightValue / timeLimit;
        float slidingUpDelay = UpValue / timeLimit;
        float slidingDownDelay = DownValue / timeLimit;
        float rotateDelay = RotateBackwards ? -1f : 1f;
        rotateDelay = rotateDelay * RotateValue / timeLimit;
        float backTrack = 1f;
        if(ObjectMovementReset && !resetAfter)
        {
            backTrack = -1f;
        }
        while (timer < timeLimit || moveForever)
        {
            timer++;
            this.transform.Rotate(new Vector3(0, rotateDelay * backTrack, 0), Space.World);
            this.transform.Translate(new Vector3((-slidingLeftDelay) * backTrack, 0, 0), Space.World);
            this.transform.Translate(new Vector3(slidingRightDelay * backTrack, 0, 0), Space.World);
            this.transform.Translate(new Vector3(0, slidingUpDelay * backTrack, 0), Space.World);
            this.transform.Translate(new Vector3(0, (-slidingDownDelay) * backTrack, 0), Space.World);
            if (timer >= timeLimit && !MoveForever)
            {
                animationPlaying = false;
                if (inRangeToMove)
                {
                    if (floatingText != null)
                    {
                        floatingText.enabled = false;
                    }
                }
            }
            if(timer > 2f && moveForever && IsControlMaster && isKeyDown)
            {
                animationPlaying = false;
                moveForever = !moveForever;
                isKeyDown = false;
                Debug.Log("Start");
            }
            if(!moveForever && MoveForever)
            {
                break;
            }
            yield return new WaitForSeconds(MovementIncrement);
        }
    }
}