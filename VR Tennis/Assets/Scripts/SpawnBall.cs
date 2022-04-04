using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnBall : MonoBehaviour
{
    public InputActionReference SpawnReference = null;
    public BallShooter ballMachine;
    [SerializeField] private int ballDelayInSeconds = 5;

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
        ballMachine.ShootWithDelay(ballDelayInSeconds);
    }
}
