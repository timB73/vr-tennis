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
    [SerializeField] private GameObject ballMachine; // so we can work out the angle the ball is coming from
    [SerializeField] private GameObject gameController;

    public bool isForehandSide = true;

    private float distanceSceneMustMove;

    void Start()
    {
        button.transform.position = xrRig.transform.position + new Vector3(buttonDistance, 0, 0);
    }

    void Update()
    {
        Debug.Log("Target x pos: " + target.transform.position.x);
        Vector3 hitPoint = Helper.GetHitPoint();
        Debug.Log("Hit point: " + hitPoint + " x = " + hitPoint.x);
    }

    /**
        Called when the button is pressed
    **/
    public void MovePlayer()
    {
        float targetX; // new x position of the target

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
        Vector3 hitPoint = Helper.GetHitPoint();
        distanceSceneMustMove = Mathf.Abs(target.transform.position.x - hitPoint.x);

        if (target.transform.position.x != ballMachine.transform.position.x)
        {
            // ball is coming from an angle
            float angle = GetAngle();
            float offset = Helper.TARGET_DISTANCE_FROM_HIT_POINT * Mathf.Tan(angle * Mathf.Deg2Rad);
            Debug.Log("Additional offset: " + offset + ", angle = " + angle);
            distanceSceneMustMove += offset;
        }

        float newSceneXPosition = scene.transform.position.x;
        float newHitPointX = hitPoint.x;
        if (isForehandSide)
        {
            newSceneXPosition -= distanceSceneMustMove;
            newHitPointX -= distanceSceneMustMove;
        }
        else
        {
            newSceneXPosition += distanceSceneMustMove;
            newHitPointX += distanceSceneMustMove;
        }

        // Try moving all at once
        scene.transform.position = new Vector3(newSceneXPosition, scene.transform.position.y, scene.transform.position.z);
        Vector3 newHitPoint = new Vector3(newHitPointX, hitPoint.y, hitPoint.z); // the hitpoint needs to move with the scene
        Helper.SetHitPoint(newHitPoint);

        // isForehandSide = !isForehandSide; // alternate between forehand and backhand
    }

    /**
        Get the distance between the XRRig and the target
    **/
    private float GetDistanceToTarget()
    {
        return target.transform.position.x - xrRig.transform.position.x;
    }

    /**
        Get the distance between the target and the hit point
    **/
    private float GetDistanceToMove()
    {
        float dist = Mathf.Abs(target.transform.position.x - Helper.GetHitPoint().x) + (Mathf.Abs(target.transform.position.x - ballMachine.transform.position.x) / 3);
        Debug.Log("Distance to move: " + dist);
        return dist;
    }

    private float GetAngle()
    {
        float distanceZ = Mathf.Abs(target.transform.position.z - ballMachine.transform.position.z);
        float distanceX = Mathf.Abs(target.transform.position.x - ballMachine.transform.position.x);

        var angle = Mathf.Rad2Deg * Mathf.Atan(distanceX / distanceZ); // TOA - opposite / adjacent then convert to degrees

        return angle;
    }
}
