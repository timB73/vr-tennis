using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{

    public static Vector3 hitPoint;

    public static void SetHitPoint(Vector3 _hitPoint)
    {
        hitPoint = _hitPoint;
    }
    public static GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

    public static void MovePlayer(GameObject xrRig, GameObject target, Vector3 newPosition)
    {
        // target.transform.position = newPosition;
        xrRig.transform.position = newPosition;
    }

    public static UnityEngine.XR.InputDevice GetHeadset()
    {
        var headMounts = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeadMounted, headMounts);

        UnityEngine.XR.InputDevice headset = headMounts[0];

        return headset;
    }
}
