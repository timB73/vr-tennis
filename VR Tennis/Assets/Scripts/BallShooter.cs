using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{

    public bool runBallMachine = true;

    [SerializeField] private Rigidbody ball;
    [SerializeField] private Transform aimTarget;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootBalls());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator ShootBalls()
    {
        WaitForSeconds wait = new WaitForSeconds(5);

        while (runBallMachine)
        {
            yield return wait;

            GameObject[] balls = GameObject.FindGameObjectsWithTag("Shoot Ball");


            foreach (GameObject b in balls)
            {
                // GameObject.Destroy(b);
            }

            Shoot();

        }
    }


    public void ShootWithDelay(int seconds)
    {
        StartCoroutine(ShootWithDelayCoroutine(seconds));
    }

    private IEnumerator ShootWithDelayCoroutine(int seconds)
    {
        WaitForSeconds wait = new WaitForSeconds(seconds);

        yield return wait;

        Shoot();
    }

    /**
        Use projectile motion physics to calculate the starting velocity vector needed to hit target
        https://www.youtube.com/watch?v=03GHtGyEHas
    **/
    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        // define distance x and y first
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        // create a float that represents our distance
        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        // Projectile equations to calculate initial velocity
        float Vxz = Sxz / time; // Vx = x / t
        float Vy = Sy / time - 0.5f * Physics.gravity.y * time; // Vy0 = y/t - 1/2 * a * t

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    /**
        Shoots a single ball to the target
    **/
    public void Shoot()
    {
        Vector3 velocity = CalculateVelocity(aimTarget.position, transform.position, 1.5f);

        Rigidbody spawnBall = Instantiate(ball, transform.position, transform.rotation);
        spawnBall.tag = "Shoot Ball";
        spawnBall.velocity = velocity;
    }
}
