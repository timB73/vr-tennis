using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{

    public GameObject lastHit;
    public Vector3 collision = Vector3.zero;

    public Vector3 lastPosition;

    private DrawHelper drawHelper;


    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        drawHelper = new DrawHelper(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // var currentPosition = transform.position;
        // if (currentPosition != lastPosition)
        // {
        //     // ray = new Ray(lastPosition, currentPosition);
        //     Debug.Log("Position changed! New pos: " + currentPosition.ToString("F8") + " last pos: " + lastPosition.ToString("F8"));
        //     // moved
        //     RaycastHit[] hits = Physics.RaycastAll(new Ray(lastPosition, (transform.position - lastPosition).normalized), (transform.position - lastPosition).magnitude);

        //     Debug.Log("Hits: " + hits.Length);
        //     for (int i = 0; i < hits.Length; i++)
        //     {
        //         Debug.Log("Hit object: " + hits[i].collider.gameObject.name);
        //     }

        //     drawHelper.DrawLine(transform.position, lastPosition);
        //     // Debug.DrawLine(transform.position, lastPosition);
        //     // DrawLine(transform.position, lastPosition);
        // }
        // lastPosition = currentPosition;


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
