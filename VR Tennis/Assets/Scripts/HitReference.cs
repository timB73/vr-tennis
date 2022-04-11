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

    void Start()
    {
        Redirect redirect = GameObject.Find("Teleport").GetComponent<Redirect>();
        buttonDistance = redirect.buttonDistance;
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
        button.transform.position = xrRigPosition + new Vector3(-buttonDistance, 0, 0); // start on forehand side
        target.transform.position = new Vector3(hitReference.x, target.transform.position.y, hitReference.z + 2.5f);

    }
}
