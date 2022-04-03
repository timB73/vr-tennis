using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPositions : MonoBehaviour
{

    public static Vector3 DEFAULT = new Vector3(-0.0299999993f, 0.117399998f, -8.39000034f);

    public static Vector3 LEFT_SERVICE_BOX = new Vector3(-2.91000009f, 0.117399998f, -4.1500001f);

    public static Vector3 RIGHT_SERVICE_BOX = new Vector3(2.28999996f, 0.117399998f, -4.1500001f);

    public static Vector3 BACK_LEFT = new Vector3(-2.74000001f, 0.117399998f, -10.0600004f);

    public static Vector3 BACK_RIGHT = new Vector3(2.49000001f, 0.117399998f, -10.0600004f);

    public static Vector3[] positionsArray = new[] { TargetPositions.LEFT_SERVICE_BOX, TargetPositions.RIGHT_SERVICE_BOX, TargetPositions.BACK_LEFT, TargetPositions.BACK_RIGHT };

}
