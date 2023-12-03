using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using XRController = UnityEngine.InputSystem.XR.XRController;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance = null;
    public static  GameManager Instance => _instance;
    
    [SerializeField] private UIManager uiManager;
    public UIManager UIManager => uiManager;

    private Dictionary<UIManager.StageList, bool> _playerQuestStages = new Dictionary<UIManager.StageList, bool>();

    [SerializeField] private XRBaseController xrRightController;
    [SerializeField] private XRBaseController xrLeftController;
    private Coroutine _repeatControllerHaptic;

    [SerializeField] private LeftHandWatch leftHandWatch;
    
    public enum ControllerHand
    {
        None = -1,
        Left = 0,
        Right = 1,
        All = 2,
    }
    
    
    private void Awake()
    {
        _instance = this;
        
        uiManager.UiButtonEventInit();
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
        // Todo : 전화 수신음 
        UIManager.SetMsgIndex(startTextIndex, endTextIndex, npcType);
        UIManager.EnableCallingUI();
    }

    public void CallOptionUI(UIManager.OptionListUI optionListUI)
    {
        UIManager.EnableOptionUI(optionListUI);
    }

    /// <summary>
    /// to do List 추가
    /// </summary>
    /// <param name="stageList">추가한 스테이지</param>
    public void NewTodoListUpdate(UIManager.StageList stageList)
    {
        uiManager.CreateNewTodoList(stageList);
        QuestAdd(stageList);
    }

    /// <summary>
    /// to do List 클리어
    /// </summary>
    /// <param name="stageList">클리어한 스테이지</param>
    public void ClearTodoListUpdate(UIManager.StageList stageList)
    {
        QuestClear(stageList);
    }

    /// <summary>
    /// to do List 퀘스트 추가
    /// </summary>
    /// <param name="stageList">추가한 스테이지</param>
    private void QuestAdd(UIManager.StageList stageList)
    {
        if (_playerQuestStages.ContainsKey(stageList))
        {
            Debug.LogError(stageList + "는 이미 Todo List에 할당되어 있습니다.");
            return;
        }
        
        _playerQuestStages.Add(stageList, false);
    }
    
    /// <summary>
    /// to do List 퀘스트 클리어
    /// </summary>
    /// <param name="stageList">클리어한 스테이지</param>
    private void QuestClear(UIManager.StageList stageList)
    {
        if (!_playerQuestStages.ContainsKey(stageList))
        {
            Debug.LogError(stageList + "는 Todo List에 할당되어 있지 않습니다.");
            return;
        }
        
        _playerQuestStages[stageList] = true;
    }


    public void UpdateHintUI(UIManager.StageList stageList)
    {
        uiManager.SetStageHint(stageList);
    }

    /// <summary>
    /// 퀘스트 스테이지 정보
    /// </summary>
    /// <returns></returns>
    public Dictionary<UIManager.StageList, bool> GetClearStages()
    {
        return _playerQuestStages;
    }

    #region VR 컨트롤러 진동

    public void CallLeftControllerHaptic()
    {
        xrLeftController.SendHapticImpulse(0.2f, 0.5f);
    }
    
    public void CallRightControllerHaptic()
    {
        xrRightController.SendHapticImpulse(0.2f, 0.5f);
    }

    /// <summary>
    /// 컨트롤러 반복 진동 효과
    /// </summary>
    /// <param name="controllerHand">컨트롤러 위치</param>
    /// <param name="delay">진동 효과 주기</param>
    public void ControllerRepeatHaptic(ControllerHand controllerHand, float delay)
    {
        StopControllerRepeatHaptic();
        _repeatControllerHaptic = StartCoroutine(ControllerRepeatHapticProcess(controllerHand, delay));
    }

    public void StopControllerRepeatHaptic()
    {
        if (_repeatControllerHaptic == null) return;
        
        StopCoroutine(_repeatControllerHaptic);
        _repeatControllerHaptic = null;
        
        Debug.Log("반복 진동 효과 코루틴 종료");
    }
    

    private IEnumerator ControllerRepeatHapticProcess(ControllerHand controllerHand, float delay)
    {
        while (true)
        {
            switch (controllerHand)
            {
                case ControllerHand.Right:
                    CallRightControllerHaptic();
                    Debug.Log("오른손 진동 효과 ~ ~ ~");
                    break;
                
                case ControllerHand.Left:
                    CallLeftControllerHaptic();
                    break;
                
                case ControllerHand.All:
                    CallLeftControllerHaptic();
                    CallRightControllerHaptic();
                    break;
            }

            yield return new WaitForSeconds(delay);
        }
    }

    #endregion


    public void EnableLeftHandWatchCollider()
    {
        leftHandWatch.EnableWatchCollider();
    }
    
    public void ExitGameProgram()
    {
        Application.Quit();
    }
}
