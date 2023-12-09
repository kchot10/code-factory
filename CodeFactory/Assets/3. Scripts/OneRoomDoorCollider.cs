using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OneRoomDoorCollider : MonoBehaviour
{
    [SerializeField] private UnityEvent doorAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHand") || other.CompareTag("RightHand"))
        {
            doorAction?.Invoke();
        }
    }
}
