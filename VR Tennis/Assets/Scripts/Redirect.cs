using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirect : MonoBehaviour
{
    public float buttonDistance = 1;
    [SerializeField] private GameObject xrRig;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject target;

    private bool isForehandSide = true;

    private int xrRigPositionIndex = 0;

    void Start()
    {
        button.transform.position = xrRig.transform.position + new Vector3(buttonDistance * -1, 0, 0);
    }

    public void MovePlayer()
    {
        Vector3 buttonOffset;
        // xrRig.transform.position = TargetPositions.positionsArray[xrRigPositionIndex];
        // button.transform.position = TargetPositions.positionsArray[xrRigPositionIndex] + buttonOffset;
        // if (xrRigPositionIndex + 1 > TargetPositions.positionsArray.Length - 1)
        //     xrRigPositionIndex = 0;
        // else
        //     xrRigPositionIndex += 1;

        if (isForehandSide)
        {
            buttonOffset = new Vector3(buttonDistance, 0, 0);
        }
        else
        {
            buttonOffset = new Vector3(buttonDistance * -1, 0, 0);
        }

        // target.transform.position = button.transform.position + new Vector3(0, 0, 1); // set target to where button was

        button.transform.position = xrRig.transform.position + buttonOffset; // move button
        isForehandSide = !isForehandSide; // alternate between forehand and backhand
    }
}
