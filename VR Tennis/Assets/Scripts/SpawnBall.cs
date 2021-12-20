using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnBall : MonoBehaviour
{
    public InputActionReference SpawnReference = null;
    public Rigidbody BallPrefab;

    public GameObject holdingController;
    public Transform Spawnpoint;

    private void Awake()
    {
        SpawnReference.action.started += SpawnTennisBall;
    }

    private void OnDestroy()
    {
        SpawnReference.action.started -= SpawnTennisBall;
    }

    void SpawnTennisBall(InputAction.CallbackContext context)
    {
        // Transform spawnTransform = holdingController.transform;
        Debug.Log("Spawned tennis ball");

        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball Clone");


        foreach (GameObject b in balls)
        {
            GameObject.Destroy(b);
        }

        Rigidbody ball = Instantiate(BallPrefab, Spawnpoint.position, Spawnpoint.rotation);
        ball.tag = "Ball Clone";
    }
}
