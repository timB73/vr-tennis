using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnusEffect : MonoBehaviour
{
    bool inCollision = false;

    void OnCollisionEnter(Collision collision)
    {
        inCollision = true;
    }

    void OnCollisionExit(Collision collision)
    {
        inCollision = false;
    }

    void FixedUpdate()
    {

        Rigidbody rb = GetComponent<Rigidbody>();

        if (!inCollision)
        {

            Vector3 velocity = rb.velocity;

            Vector3 rotation = rb.angularVelocity;

            float magnus_effect_constant = .001f;

            rb.AddForce(magnus_effect_constant * velocity.z * new Vector3(rotation.y, -rotation.x, 0), ForceMode.Force);

            rb.AddForce(magnus_effect_constant * velocity.y * new Vector3(-rotation.z, 0, rotation.x), ForceMode.Force);

            rb.AddForce(magnus_effect_constant * velocity.x * new Vector3(0, rotation.z, -rotation.y), ForceMode.Force);
        }

    }
}
