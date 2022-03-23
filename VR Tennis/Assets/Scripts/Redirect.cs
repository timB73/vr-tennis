using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirect : MonoBehaviour
{

    GameObject mainCamera;
    [SerializeField] bool rotate = true;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = this.gameObject;
        // StartCoroutine(RotateCamera());

        var headMounts = new List<UnityEngine.XR.InputDevice>();
        Debug.Log("testing 123");
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeadMounted, headMounts);

        // OculusDebug.Instance.log("Hello, getting devices " + headMounts.Count);
        Debug.Log("Hello, getting devices " + headMounts.Count);
        foreach (var device in headMounts)
        {
            // from logs: Device name 'Oculus Quest' has characteristic 'HeadMounted, TrackedDevice'
            string message = string.Format("Device name '{0}' has characteristic '{1}'", device.name, device.characteristics.ToString());
            Debug.Log(message);
            // OculusDebug.Instance.log(message);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

    }

    private IEnumerator RotateCamera()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (rotate)
        {

            mainCamera.transform.eulerAngles = new Vector3(
            mainCamera.transform.eulerAngles.x,
            mainCamera.transform.eulerAngles.y + 5,
            mainCamera.transform.eulerAngles.z
            );

            yield return wait;
        }
    }
}
