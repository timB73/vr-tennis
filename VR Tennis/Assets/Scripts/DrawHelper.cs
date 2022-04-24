using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Helper class for drawing lines - used heavily in debugging racket physics
**/
public class DrawHelper
{
    private GameObject gameObject;

    private LineRenderer lr;

    /** Requires a a game object to attach the line renderer to **/
    public DrawHelper(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    public void DrawLine(Vector3 start, Vector3 end)
    {
        if (start == end) return;

        lr = gameObject.GetComponent<LineRenderer>();
        if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
        }
        lr.startWidth = 0.01f; // straight line by setting start and end widths to the same value
        lr.endWidth = 0.01f;

        // lr.material = new Material(Shader.Find("Sprites/Default"));

        // Set some positions
        Vector3[] positions = new Vector3[2];
        positions[0] = start;
        positions[1] = end;

        Debug.Log("DrawLine position start" + positions[0].ToString("F3"));
        Debug.Log("DrawLine position end" + positions[0].ToString("F3"));

        // positions[2] = new Vector3(2.0f, -2.0f, 0.0f);
        lr.positionCount = positions.Length;
        lr.SetPositions(positions);
    }

    public void ClearLine()
    {
        lr.positionCount = 0;
    }
}
