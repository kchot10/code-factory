using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance = null;

    public static  GameManager Instance => _instance;
    
    [SerializeField] private UIManager uiManager;

    public UIManager UIManager => uiManager;

    private void Awake()
    {
        _instance = this;
    }


    // 입력 테스트
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIManager.EnableGlobalMessageUI();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            UIManager.EnableRadioMessageUI();
        }
    }

    public void CallGlobalMessage(int startTextIndex, int endTextIndex, UIManager.NPC npcType)
    {
        UIManager.SetMsgIndex(startTextIndex, endTextIndex, npcType);
        UIManager.EnableGlobalMessageUI();
    }
    
    public void CallRadioMessage(int startTextIndex, int endTextIndex, UIManager.NPC npcType)
    {
        UIManager.SetMsgIndex(startTextIndex, endTextIndex, npcType);
        UIManager.EnableRadioMessageUI();
    }
}
