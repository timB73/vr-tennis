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
        Vector3 hitReference = gameController.transform.position;
        Debug.Log("HitReference: Setting reference " + hitReference);
        Helper.SetHitPoint(hitReference);
        Vector3 xrRigCurrentPosition = xrRig.transform.position;
        Vector3 xrRigPosition = new Vector3(hitReference.x, xrRigCurrentPosition.y, hitReference.z) + new Vector3(0, 0, -1.727f);
        xrRig.transform.position = xrRigPosition;
        button.transform.position = xrRigPosition + new Vector3(-1, 0, 0);
        target.transform.position = new Vector3(xrRigPosition.x, target.transform.position.y, hitReference.z);
    }
}
