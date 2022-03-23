using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Vector3[] targetPositions;

    private int currentTargetPos;

    private bool isRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        currentTargetPos = 0;
        targetPositions = new[] { TargetPositions.LEFT_SERVICE_BOX, TargetPositions.RIGHT_SERVICE_BOX, TargetPositions.BACK_LEFT, TargetPositions.BACK_RIGHT };
        this.transform.position = TargetPositions.LEFT_SERVICE_BOX;
        StartCoroutine(MoveTarget());
    }

    // Update is called once per frame
    void Update()
    {

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
