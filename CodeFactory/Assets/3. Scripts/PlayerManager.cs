using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRInput;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 이동 속도

    void Update()
    {
        Vector2 thumbstickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);

        Vector3 moveDirection = new Vector3(-thumbstickInput.x, 0.0f, -thumbstickInput.y);
        
        // 이동 방향 벡터의 길이를 제한하여 대각선 이동이 일정한 속도로 처리되도록 합니다.
        if (moveDirection.magnitude > 1.0f)
        {
            moveDirection.Normalize();
        }

        // 이동 방향에 이동 속도를 곱하여 이동합니다.
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}

