using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HitReference : MonoBehaviour
{
    public InputActionReference HitReferenceAction = null;

    [SerializeField] private GameObject gameController;
    [SerializeField] private GameObject xrRig;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject button;

    private float buttonDistance;
    private bool isForehandSide;

    void Start()
    {
        // Get whether the system is configured for forehand or backhand & the button distance from Redirect component
        Redirect redirect = GameObject.Find("Teleport").GetComponent<Redirect>();
        buttonDistance = redirect.buttonDistance;
        isForehandSide = redirect.isForehandSide;
        Debug.Log("Set button distance: " + buttonDistance);
    }

    private void Awake()
    {
        HitReferenceAction.action.started += SetHitReference;
    }

    private void OnDestroy()
    {
        HitReferenceAction.action.started -= SetHitReference;
    }

    void Update()
    {
        Debug.Log("Controller position: " + gameController.transform.position);
    }

    void SetHitReference(InputAction.CallbackContext context)
    {
        // position of player should be about 3 units away from where the target is
        // hit target is about 1 unit away from default standing position (arm length)
        Vector3 hitReference = gameController.transform.position;
        Debug.Log("HitReference: Setting reference " + hitReference);
        Helper.SetHitPoint(hitReference);
        Vector3 xrRigCurrentPosition = xrRig.transform.position;

        Vector3 xrRigPosition = new Vector3(hitReference.x, xrRigCurrentPosition.y, hitReference.z) + new Vector3(0, 0, -1.727f); // set just in front of the hit target
        xrRig.transform.position = xrRigPosition;

        // if forehand, the button needs to be on the left (negative x), so they move back to hit the target from the left side (assumes right handed player)
        float buttonX = buttonDistance;

        if (isForehandSide)
        {
            buttonX = -buttonDistance;
        }

        float targetZ = hitReference.z + Helper.TARGET_DISTANCE_FROM_HIT_POINT; // should be in front of hit point such that ball bounces and contact point is roughly where the hit reference is

        button.transform.position = xrRigPosition + new Vector3(buttonX, 0, 0); // set button new position
        target.transform.position = new Vector3(hitReference.x, target.transform.position.y, targetZ); // set the new target position to in front of the hit point

    }
}
