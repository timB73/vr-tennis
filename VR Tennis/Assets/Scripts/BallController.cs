using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{

    [SerializeField] private int maxRotation = 100;
    [SerializeField] private int accelerationFactor = 10;
    [SerializeField] private int decellerationFactor = 2;

    private int topspinRotation = 0;

    private int backspinRotation = 0;

    private Rigidbody ballRigidBody;

    private Vector3 m_EulerAngleVelocity;

    private bool jump;

    void Update()
    {
        if (Keyboard.current.wKey.isPressed && topspinRotation < maxRotation)
        {
            Debug.Log("BallController keyboard w pressed");
            topspinRotation += accelerationFactor;
        }
        else if (!Keyboard.current.wKey.isPressed && topspinRotation > 0)
        {
            topspinRotation -= decellerationFactor;
        }

        if (Keyboard.current.sKey.isPressed && backspinRotation < maxRotation)
        {
            backspinRotation += accelerationFactor;
        }
        else if (!Keyboard.current.sKey.isPressed && backspinRotation > 0)
        {
            backspinRotation -= decellerationFactor;
        }

        if (Keyboard.current.spaceKey.isPressed)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ballRigidBody = this.gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        int xRotation = topspinRotation - backspinRotation;

        Debug.Log("X rotation: topspin (" + topspinRotation + ") - backspin (" + backspinRotation + ") = " + xRotation);

        if (xRotation != 0)
        {
            // add topspin
            m_EulerAngleVelocity = new Vector3(xRotation, 0, 0); // rotate about x axis
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
            // ballRigidBody.MoveRotation(ballRigidBody.rotation * deltaRotation);
            ballRigidBody.AddTorque(m_EulerAngleVelocity * Time.fixedDeltaTime, ForceMode.Acceleration);

        }

        if (jump)
        {
            ballRigidBody.AddForce(Vector3.up, ForceMode.VelocityChange);
            jump = false;
        }
    }
}
