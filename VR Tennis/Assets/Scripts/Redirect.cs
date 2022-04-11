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

    [SerializeField] private float distanceThreshold = 0.33f; // the x distance between the target and the player which to no longer move the scene
    // [SerializeField] private float sceneMoveBy = 0.5f; // distance to move the scene by every second

    private UnityEngine.XR.InputDevice headset;

    private bool isForehandSide = true;
    private float distanceToMove = 0;

    private float distanceSceneMustMove;

    void Start()
    {
        // Helper.MovePlayer(xrRig, target, TargetPositions.positionsArray[positionIndexes[currentIndex]]);
        button.transform.position = xrRig.transform.position + new Vector3(buttonDistance * -1, 0, 0);
        headset = Helper.GetHeadset();
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
        distanceToMove = GetDistanceToMove();
        Vector3 hitPoint = Helper.GetHitPoint();
        distanceSceneMustMove = Mathf.Abs(target.transform.position.x - hitPoint.x);
        // StartCoroutine(MoveScene());

        if (target.transform.position.x != ballMachine.transform.position.x)
        {
            // ball is coming from an angle
            float angle = GetAngle();
            float offset = 2.5f * Mathf.Tan(angle * Mathf.Deg2Rad); // 2 = the distance hit target is from the ball machine target (see HitReference.cs)
            Debug.Log("Additional offset: " + offset + ", angle = " + angle);
            distanceSceneMustMove += offset;
        }

        // Try moving all at once
        scene.transform.position = new Vector3(scene.transform.position.x - distanceSceneMustMove, scene.transform.position.y, scene.transform.position.z);
        Vector3 newHitPoint = new Vector3(hitPoint.x - distanceSceneMustMove, hitPoint.y, hitPoint.z); // the hitpoint needs to move with the scene
        Helper.SetHitPoint(newHitPoint);

        // isForehandSide = !isForehandSide; // alternate between forehand and backhand
    }

    private IEnumerator MoveScene()
    {
        float distancePlayerMustMove = Mathf.Abs(gameController.transform.position.x - Helper.GetHitPoint().x);

        Debug.Log("Distance player must move: " + distancePlayerMustMove);
        Debug.Log("Distance scene must move: " + distanceSceneMustMove);

        float sceneMoveBy = 0.1f;

        if (isForehandSide)
            sceneMoveBy *= -1;

        float currentDistance = GetDistanceToTarget();

        WaitForSeconds wait = new WaitForSeconds(1);

        Vector3 hitPoint = Helper.GetHitPoint();

        float distanceMoved = 0;
        // the scene needs to move by the amount such that the target x position is EXACTLY the same as the hitPoint x position

        // OR while target.transform.x !== hitPoint.x
        while (Mathf.Abs(distanceMoved) < distanceSceneMustMove)
        {
            yield return wait;
            scene.transform.position = new Vector3(scene.transform.position.x + sceneMoveBy, scene.transform.position.y, scene.transform.position.z);
            Vector3 newHitPoint = new Vector3(hitPoint.x + sceneMoveBy, hitPoint.y, hitPoint.z); // the hitpoint needs to move with the scene
            Helper.SetHitPoint(newHitPoint);
            distanceMoved += sceneMoveBy;
            Debug.Log("Distance moved: " + distanceMoved);
        }
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
