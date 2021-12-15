using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{

    public GameObject lastHit;
    public Vector3 collision = Vector3.zero;

    public Vector3 lastPosition;

    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        if (start == end) return;

        LineRenderer lr;

        lr = GetComponent<LineRenderer>();
        if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
        }
        lr.startWidth = 0.01f;
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

    void FixedUpdate()
    {
        var currentPosition = transform.position;
        if (currentPosition != lastPosition)
        {
            // ray = new Ray(lastPosition, currentPosition);
            Debug.Log("Position changed! New pos: " + currentPosition.ToString("F8") + " last pos: " + lastPosition.ToString("F8"));
            // moved
            RaycastHit[] hits = Physics.RaycastAll(new Ray(lastPosition, (transform.position - lastPosition).normalized), (transform.position - lastPosition).magnitude);

            Debug.Log("Hits: " + hits.Length);
            for (int i = 0; i < hits.Length; i++)
            {
                Debug.Log("Hit object: " + hits[i].collider.gameObject.name);
            }

            // Debug.DrawLine(transform.position, lastPosition);
            DrawLine(transform.position, lastPosition);
        }
        lastPosition = currentPosition;


        // }
        // Vector3 direction = transform.position - lastPosition;
        // Ray ray = new Ray(lastPosition, direction);
        // RaycastHit hit;
        // int maxDistance = 10;
        // if (Physics.Raycast(ray, out hit, direction.magnitude))
        // {
        //     Debug.DrawRay(this.transform.position, this.transform.forward * hit.distance, Color.yellow);
        //     lastHit = hit.transform.gameObject;
        //     Debug.Log(lastHit);
        //     collision = hit.point;
        // }
        // Physics.Raycast(ray);
    }
}
