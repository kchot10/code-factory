using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance = null;
    public static  GameManager Instance => _instance;
    
    [SerializeField] private UIManager uiManager;
    [SerializeField] private UI_SoundManager uiSoundManager;
    [SerializeField] private StageItemManager stageItemManager;
    [SerializeField] private ExitRoom exitRoom;
    
    [SerializeField] private GameObject exitRoomFrontCheckPoint; // 탈출구 트리거 Zone
    [SerializeField] private GameObject tunnelingVignette; // 멀미 방지 기능
    
    public UIManager UIManager => uiManager;
    public UI_SoundManager UISoundManager => uiSoundManager;
    public StageItemManager StageItemManager => stageItemManager;

    private Dictionary<UIManager.StageList, bool> _playerQuestStages = new Dictionary<UIManager.StageList, bool>();
    private Dictionary<UIManager.StageList, int> _playerStageClearTime = new Dictionary<UIManager.StageList, int>();

    [SerializeField] private XRBaseController xrRightController;
    [SerializeField] private XRBaseController xrLeftController;
    private Coroutine _repeatControllerHaptic;

    [SerializeField] private LeftHandWatch leftHandWatch;

    private int _stageClearTimer = 0;
    private Coroutine _stageClearTimerProcess;
    private readonly WaitForSeconds seconds = new WaitForSeconds(1f);
    
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
        
        _playerStageClearTime.Add(UIManager.StageList.Stage1, 0);
        _playerStageClearTime.Add(UIManager.StageList.Stage2, 0);
        _playerStageClearTime.Add(UIManager.StageList.Stage3, 0);
        _playerStageClearTime.Add(UIManager.StageList.Stage4, 0);
        _playerStageClearTime.Add(UIManager.StageList.Stage5, 0);
        _playerStageClearTime.Add(UIManager.StageList.Stage6, 0);
        _playerStageClearTime.Add(UIManager.StageList.Stage7, 0);
    }

    public void CallGlobalMessage(int startTextIndex, int endTextIndex, UIManager.NPC npcType)
    {
        UIManager.SetMsgIndex(startTextIndex, endTextIndex, npcType);
        UIManager.EnableGlobalMessageUI();
    }
    
    public void CallRadioMessage(int startTextIndex, int endTextIndex, UIManager.NPC npcType)
    {
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

        // 스테이지 퀘스트 시작 시 타이머 작동 시작
        if (_playerStageClearTime.ContainsKey(stageList))
        {
            _stageClearTimer = 0;

            if (_stageClearTimerProcess != null)
            {
                StopCoroutine(_stageClearTimerProcess);
                _stageClearTimerProcess = null;
            }

            _stageClearTimerProcess = StartCoroutine(StageClearTimerProcess());
        }
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

        // 스테이지 퀘스트 클리어 시 타이머 기록
        if (_playerStageClearTime.ContainsKey(stageList))
        {
            StopCoroutine(_stageClearTimerProcess);
            _stageClearTimerProcess = null;

            _playerStageClearTime[stageList] = _stageClearTimer;
        }
        
        // 돈을 받은 후 퀘스트를 모두 완료하면 게임 종료
        if (_playerQuestStages.Count == (int)UIManager.StageList.StageListCount)
        {
            if (CheckQuestClear())
            {
                // 게임 결과 UI 출력
                uiManager.EnableOptionUI(UIManager.OptionListUI.clearGameUI);
            }
            return;
        }
        
        // 돈을 받기 전 퀘스트를 모두 완료하면
        if (_playerQuestStages.Count == (int)UIManager.StageList.StageListCount - 1)
        {
            if (CheckQuestClear())
            {
                // 퇴근 퀘스트 추가
                NewTodoListUpdate(UIManager.StageList.Exit);
                
                // 출구 트리거 활성화
                exitRoomFrontCheckPoint.SetActive(true);
                
                // 탈출구 개방
                exitRoom.ExitRoomDoorOpen();
            }
        }
    }

    [ContextMenu("DebugModeClearAllStage")]
    /// <summary>
    /// 개발자 모드 모든 스테이지 클리어
    /// </summary>
    public void DebugModeClearAllStage()
    {
        UIManager.StageList[] stageList = (UIManager.StageList[])Enum.GetValues(typeof(UIManager.StageList));

        foreach (var stage in stageList)
        {
            if (stage == UIManager.StageList.None || stage == UIManager.StageList.StageListCount ||
                stage == UIManager.StageList.Exit) return;
            
            
            if (_playerQuestStages.ContainsKey(stage))
            {
                if (_playerQuestStages[stage] == false)
                {
                    ClearTodoListUpdate(stage);
                }
            }
            else
            {
                NewTodoListUpdate(stage);
                ClearTodoListUpdate(stage);
            }
        }
    }

    // 스테이지 타이머 코루틴
    private IEnumerator StageClearTimerProcess()
    {
        while (true)
        {
            yield return seconds;
            _stageClearTimer += 1;
        }
    }
    
    /// <summary>
    /// 퀘스트 완료 확인
    /// </summary>
    /// <returns>완료 결과</returns>
    private bool CheckQuestClear()
    {
        foreach (bool clear in _playerQuestStages.Values)
        {
            if (!clear)
            {
                return false;
            }
        }
        return true;
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

    public Dictionary<UIManager.StageList, int> GetClearStagesTimer()
    {
        return _playerStageClearTime;
    }

    public void ClearGame()
    {
        Application.Quit();
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

    #region 멀미 방지 기능

    public void EnableMotionSickness()
    {
        tunnelingVignette.SetActive(true);
    }
    
    public void DisableMotionSickness()
    {
        tunnelingVignette.SetActive(false);
    }

    #endregion
    

    
    public void ExitGameProgram()
    {
        Application.Quit();
    }
}
