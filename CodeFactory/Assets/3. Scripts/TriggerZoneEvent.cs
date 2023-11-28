using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZoneEvent : MonoBehaviour
{
    [SerializeField] private bool isGlobalText;
    
    [SerializeField] private int startTextIndex;

    [SerializeField] private int endTextIndex;

    [SerializeField] private UIManager.NPC npcType;
    
    
    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            if (isGlobalText)
            {
                GameManager.Instance.CallGlobalMessage(startTextIndex, endTextIndex, npcType);
            }
            else
            {
                GameManager.Instance.CallRadioMessage(startTextIndex, endTextIndex, npcType);
            }
        }
    }
}
