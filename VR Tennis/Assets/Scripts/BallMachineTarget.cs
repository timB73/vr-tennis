using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMachineTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Contains("Ball"))
        {
            Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
            rb.drag = 0.6f;
        }
    }
}
