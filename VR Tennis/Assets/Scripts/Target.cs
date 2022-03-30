using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject scene;
    [SerializeField] private bool isRunning = true;
    private Vector3[] targetPositions;
    private UnityEngine.XR.InputDevice headset;

    private int currentTargetPos;


    // Start is called before the first frame update
    void Start()
    {
        currentTargetPos = 0;
        targetPositions = new[] { TargetPositions.LEFT_SERVICE_BOX, TargetPositions.RIGHT_SERVICE_BOX, TargetPositions.BACK_LEFT, TargetPositions.BACK_RIGHT };
        // this.transform.position = TargetPositions.LEFT_SERVICE_BOX;

        var headMounts = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeadMounted, headMounts);

        headset = headMounts[0];

        // scene.transform.position = this.transform.position;

        StartCoroutine(MoveTarget());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 headsetPosition;
        if (headset.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out headsetPosition))
        {
            Debug.Log("Got headset position: " + headsetPosition);
            if (headsetPosition.y < 1.07f)
            {
                Debug.Log("Headset pos: Looking at the ground!");
                // scene.transform.position = new Vector3(scene.transform.position.x, scene.transform.position.y, scene.transform.position.z + 0.1f);
            }
            else
            {
                Debug.Log("Headset pos: NOT looking at the ground");
            }
        }
    }

    private IEnumerator MoveTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(6);

        while (isRunning)
        {
            if (currentTargetPos > targetPositions.Length - 1)
            {
                currentTargetPos = 0;
            }

            iTween.MoveTo(this.gameObject, targetPositions[currentTargetPos], 2);

            currentTargetPos += 1;

            yield return wait;
        }

    }
}
