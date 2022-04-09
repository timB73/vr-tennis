using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirect : MonoBehaviour
{
    public float buttonDistance = 1;
    [SerializeField] private GameObject xrRig;
    [SerializeField] private GameObject scene;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject courtCenter;

    [SerializeField] private float distanceThreshold = 0.33f; // the x distance between the target and the player which to no longer move the scene
    [SerializeField] private float sceneMoveBy = 0.5f; // distance to move the scene by every second

    private bool isForehandSide = false;

    void Start()
    {
        // Helper.MovePlayer(xrRig, target, TargetPositions.positionsArray[positionIndexes[currentIndex]]);
        button.transform.position = xrRig.transform.position + new Vector3(buttonDistance * -1, 0, 0);
    }

    void Update()
    {
        float distance = GetDistanceToTarget();
        // Debug.Log("Distance to target: " + distance);
        Debug.Log("Target x pos: " + target.transform.position.x);
        Vector3 hitPoint = Helper.GetHitPoint();
        Debug.Log("Hit point: " + hitPoint + " x = " + hitPoint.x);
    }

    /**
        Called when the button is pressed
    **/
    public void MovePlayer()
    {
        float targetX;

        if (isForehandSide)
        {
            targetX = courtCenter.transform.position.x + 3;
        }
        else
        {
            targetX = courtCenter.transform.position.x - 3;
        }

        // NEW ALGORITHM
        target.transform.position = new Vector3(targetX, target.transform.position.y, target.transform.position.z); // move target
        StartCoroutine(MoveScene());

        isForehandSide = !isForehandSide; // alternate between forehand and backhand
    }

    private IEnumerator MoveScene()
    {
        if (isForehandSide)
            sceneMoveBy *= -1;

        float currentDistance = GetDistanceToTarget();

        WaitForSeconds wait = new WaitForSeconds(1);

        Vector3 hitPoint = Helper.GetHitPoint();

        // OR while target.transform.x !== hitPoint.x
        while (Mathf.Abs(target.transform.position.x - hitPoint.x) > sceneMoveBy)
        {
            yield return wait;
            scene.transform.position = new Vector3(scene.transform.position.x + sceneMoveBy, scene.transform.position.y, scene.transform.position.z);
            Vector3 newHitPoint = new Vector3(hitPoint.x + sceneMoveBy, hitPoint.y, hitPoint.z); // the hitpoint needs to move with the scene
            Helper.SetHitPoint(newHitPoint);
            currentDistance = GetDistanceToTarget();
        }
    }

    /**
        Get the distance between the XRRig and the target
    **/
    private float GetDistanceToTarget()
    {
        return target.transform.position.x - xrRig.transform.position.x;
    }
}
