using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class Racket : MonoBehaviour
{

    private GameObject racketFace;

    [SerializeField] private float racketZSpring;
    [SerializeField] private float racketYSpring;
    private Matrix4x4[] transform_history = new Matrix4x4[10];// store last 10 local to world matrices for calculating velocity at a particular hit point

    private float timeSinceTrigger = 0;

    // Start is called before the first frame update
    void Start()
    {
        racketFace = Helper.GetChildWithName(gameObject, "RacketFace");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        // shift along rotation history
        System.Array.Copy(transform_history, 0, transform_history, 1, transform_history.Length - 1);
        // add in new position history
        transform_history[0] = racketFace.transform.localToWorldMatrix;
        timeSinceTrigger += Time.fixedDeltaTime;
    }

    void OnTriggerEnter(Collider col)
    {
        if (timeSinceTrigger < 0.1)
        {
            return;
        }
        timeSinceTrigger = 0;
        // getDevice();
        //This method will run when your game object
        // Debug.Log("Racket collided with " + col.gameObject.name);
        if (col.gameObject.name.Contains("Ball"))
        {
            Rigidbody ball = col.gameObject.GetComponent<Rigidbody>();
            Vector3 ballPos = ball.position;
            GameObject racketFace = Helper.GetChildWithName(gameObject, "RacketFace");
            Vector3 racketPos = racketFace.transform.position;
            // where on the racket it was hit
            Vector3 localHitPos = racketFace.transform.InverseTransformPoint(ballPos);
            // world position of the hit point 10 frames back
            Vector3 previousWorldHitPos = transform_history[transform_history.Length - 1].MultiplyPoint(localHitPos);

            Vector3 velocity = (ballPos - previousWorldHitPos) / (Time.fixedDeltaTime * 10.0f);
            print("Hit velocity:" + velocity + "!!!" + (previousWorldHitPos - ballPos));

            // localOutgoingVelocity = new Vector3(-localIncomingVelocity.x,ballYVelocity,ballZVelocity); // where ballY and Z velocity are transformed into the racket space.

            // do the bounce based on how fast the ball is coming towards the racket
            // it should bounce back at this but flipped in the
            //  racket  x axis
            Vector3 incomingVelocity = ball.velocity - velocity;
            Vector3 localIncomingVelocity = transform.InverseTransformVector(incomingVelocity);
            Vector3 spinVector = new Vector3(0, localIncomingVelocity.z, -localIncomingVelocity.y);
            Vector3 spinVectorWorld = transform.TransformVector(spinVector);

            ball.AddTorque(10.0f * spinVectorWorld, ForceMode.Acceleration);
            ball.maxAngularVelocity = 100000;
            Debug.Log("Spin: " + spinVectorWorld + ":" + spinVector + "!" + ball.angularVelocity);
            Vector3 localOutgoingVelocity = new Vector3(-localIncomingVelocity.x, localIncomingVelocity.y, localIncomingVelocity.z);
            Vector3 outgoingVelocity = transform.TransformVector(localOutgoingVelocity) + velocity;
            // Debug.Log("Ball collided!" + velocity + ":" + incomingVelocity + ":" + localIncomingVelocity + "!" + localOutgoingVelocity + "#" + outgoingVelocity);

            outgoingVelocity.z *= racketZSpring;
            outgoingVelocity.y *= racketYSpring;
            ball.velocity = outgoingVelocity;

            // = dir.normalized * ballForce + new Vector3(0, 10, 0);

            // ball.AddForce(Vector3.Reflect(ball.velocity, racketPos), ForceMode.Impulse);
        }
    }
}
