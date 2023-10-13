using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRInput;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float amount = 45.0f;

    void Update()
    {
        Vector2 thumbstickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        Vector3 moveDirection = new Vector3(-thumbstickInput.x, 0.0f, -thumbstickInput.y);

        if (moveDirection.magnitude > 1.0f)
        {
            moveDirection.Normalize();
        }

        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0.0f;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        float thumbstickX = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;

        if (thumbstickX > 0)
        {
            amount = 45.0f;
        }
        else if (thumbstickX < 0)
        {
            amount = -45.0f;
        }
        else
        {
            amount = 0.0f;
        }

        float horizontalRotation = amount * Time.deltaTime;

        transform.Rotate(Vector3.up, horizontalRotation);
    }
}
