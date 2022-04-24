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
        Called when the button is pressed - see Push component in Unity editor (uses UnityEvents)
    **/
    public void MoveScene()
    {
        float targetX; // new x position of the target

        // the new x position is set to the far left (backhand) or right (forehand) side of the court
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
        distanceSceneMustMove = Mathf.Abs(target.transform.position.x - hitPoint.x); // get the difference the target has moved to determine the new position of the scene

        if (target.transform.position.x != ballMachine.transform.position.x)
        {
            // ball is coming from an angle, thus an additional offset needs to be added
            float angle = GetAngle();
            float offset = Helper.TARGET_DISTANCE_FROM_HIT_POINT * Mathf.Tan(angle * Mathf.Deg2Rad);
            Debug.Log("Additional offset: " + offset + ", angle = " + angle);
            distanceSceneMustMove += offset;
        }

        // Set the new scene position based on forehand (-ve - left side) or backhand (+ve - right side)
        // note the hit reference needs to be adjusted by the same amount so that it can work again
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

        // Move scene all at once
        scene.transform.position = new Vector3(newSceneXPosition, scene.transform.position.y, scene.transform.position.z);
        Vector3 newHitPoint = new Vector3(newHitPointX, hitPoint.y, hitPoint.z); // the hitpoint needs to move with the scene
        Helper.SetHitPoint(newHitPoint);
    }

    private float GetAngle()
    {
        float distanceZ = Mathf.Abs(target.transform.position.z - ballMachine.transform.position.z);
        float distanceX = Mathf.Abs(target.transform.position.x - ballMachine.transform.position.x);

        var angle = Mathf.Rad2Deg * Mathf.Atan(distanceX / distanceZ); // TOA - opposite / adjacent then convert to degrees

        return angle;
    }
}
