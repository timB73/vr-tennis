using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/**
    Handles the getting the input values from the controller and pass to the Hand class to animate
    https://youtu.be/DxKWq7z4Xao
**/
[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour
{
    ActionBasedController controller;
    public Hand hand;


    void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }


    void Update()
    {
        hand.SetGrip(controller.selectAction.action.ReadValue<float>());
        hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
    }
}
