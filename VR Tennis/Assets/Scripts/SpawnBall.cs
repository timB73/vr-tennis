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
        Debug.Log("SpawnBall script Awake()");
        for (int i = 0; i < 10; i++)
        {
            // Instantiate(BallPrefab, Spawnpoint, false);

        }
    }

    private void OnDestroy()
    {
        SpawnReference.action.started -= SpawnTennisBall;
    }

    void SpawnTennisBall(InputAction.CallbackContext context)
    {
        // Transform spawnTransform = holdingController.transform;
        Debug.Log("Spawned tennis ball");

        Rigidbody ball = Instantiate(BallPrefab, Spawnpoint.position, Spawnpoint.rotation);
    }
}
