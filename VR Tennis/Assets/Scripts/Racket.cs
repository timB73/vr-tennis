using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{

    // technique inspired from https://github.com/sinoriani/Unity-Projects/blob/master/Tennis%20Game/Player.cs
    [SerializeField] private Transform aimTarget;
    [SerializeField] private float ballForce;

    private Vector3 currentPosition;
    private Vector3 lastPosition;
    private Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;
        velocity = currentPosition - lastPosition;
        lastPosition = transform.position;
    }

    void OnCollisionEnter(Collision col)
    {
        //This method will run when your game object
        //collides with something
        Debug.Log("Racket collided with " + col.gameObject.name);
        if (col.gameObject.name.Contains("Ball"))
        {
            // Debug.Log("Ball collided!");
            // Rigidbody ball = col.gameObject.GetComponent<Rigidbody>();
            // Vector3 ballPos = ball.position;
            // GameObject racketFace = Helper.GetChildWithName(gameObject, "RacketFace");
            // Vector3 racketPos = racketFace.transform.position;

            // Vector3 dir = aimTarget.position - transform.position; // get the direction to where we want to send the ball
            // ball.velocity = dir.normalized * ballForce + new Vector3(0, 10, 0);

            // ball.AddForce(Vector3.Reflect(ball.velocity, racketPos), ForceMode.Impulse);
        }
    }
}
