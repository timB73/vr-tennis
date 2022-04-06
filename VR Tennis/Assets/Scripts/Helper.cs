using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{
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
        target.transform.position = newPosition;
        xrRig.transform.position = newPosition;
    }
}
