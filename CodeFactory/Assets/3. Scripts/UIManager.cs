using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public enum StageList
    {
        None = -1,
        Tutorial = 0,
        GrabTutorial = 1,
        Stage1 = 2,
        Stage2 = 3,
        Stage3 = 4,
        Stage4 = 5,
        Stage5 = 6,
        Stage6 = 7,
        Exit = 8,
    }
    
    public enum OptionListUI
    {
        None = -1,
        optionUI = 0,
        toDoList = 1,
        hintUI = 2,
        extiGameUI = 3
    }
    
    private string[] _globalTextStrings = new string[]
    {
        // AI
        "왼쪽 버튼을 아무 손으로 가져다가 대면 상호작용합니다.",  // 0
    };

    private string[] _bossTextStrings = new string[]
    {
        // 밖
        "오! 이야기 들었네 이번에 처음 들어온 사원인가?",    // 0
        "그럼 버튼을 눌러 내부로 들어가보게",
        "자네가 들어올 때 누른 버튼처럼 누를 수 있는 버튼들이 공장 내에 많이 있네. 버튼에 기능이 적혀 있으니 보고 눌러 보게나.", 
        
        // 튜토리얼
        "왼손 컨트롤러의 조이스틱을 이용하여 움직일 수 있다네",  // 4
        "어디로 갈지 모르겠다면, 바닥의 초록샌 선을 따라 가면 된다네",
        "양손 조이스틱의 방아쇠를 눌러 물건을 집을 수 있고, 집은 채로 물건을 옮길 수 있다네.",
        "물건을 놓고 싶다면 방아쇠를 놓으면 된다네",
        "기본적인 사용법을 배웠으니, 자 이제 우리 공장의 내부로 이동하게나",
        
        // 공장 내부
        "일단 공장 발전기들이 전부 고장이 나, 수리해주길 바래",  // 8
        "문제가 있는 설비에 다가가서 해결해봐",
        "보다시피 하나 빼고 전부 고장이 나 곤란한 상태야..",
        "하루 빨리 고쳐줬으면 바라네",
        
        // 엔딩
        "신입 덕분에 공장이 전부 가동되네, 고맙네", // 12
        "오늘 할 일은 다했으니, 이만 퇴근하게",
        "남은 직원들은 아직 할 일이 많으니 마무리 작업 준비하고..."
    };

    private Dictionary<NPC, string[]> _npcTexts;
    private Dictionary<NPC, Sprite> _npcPortraits;
    private Dictionary<NPC, string> _npcNames;

    [SerializeField] private Sprite[] npcPortraitSprites;

    
    [Header("글로벌 UI 텍스트")]
    [SerializeField] private GameObject globalMessageUI;
    [SerializeField] private TextMeshProUGUI globalMessageNpcName;
    [SerializeField] private TextMeshProUGUI globalMessageText;
    [SerializeField] private Button globalMessageButton;
    [SerializeField] private TextMeshProUGUI globalMessageButtonText;
    [SerializeField] private Image globalMessagePortrait;

    [Header("옵션 UI")]
    [SerializeField] private GameObject optionUI;
    [SerializeField] private Button hintUiButton;
    [SerializeField] private Button todoListUiButton;
    [SerializeField] private Button exitGameUiButton;
    [SerializeField] private Button optionUiCloseButton;
    private Transform _lastOptionUi;

    [Header("Todo List UI")]
    [SerializeField] private GameObject toDoListUI;
    [SerializeField] private GameObject todoListPrefab;
    [SerializeField] private Button todoListCloseButton;
    [SerializeField] private Transform todoListContentRoot;
    private Dictionary<StageList, TodoListObject> _todoListObjects = new Dictionary<StageList, TodoListObject>();
    private Dictionary<StageList, string> _todoListTexts;
    private Coroutine _todoListCoroutine;
    private readonly WaitForSeconds _DELAY = new WaitForSeconds(0.15f);

    [Header("Hint UI")] 
    [SerializeField] private GameObject hintUI;
    [SerializeField] private Button hintUiCloseButton;
    [SerializeField] private TextMeshProUGUI hintTitleText;
    [SerializeField] private TextMeshProUGUI hintText;

    private Dictionary<StageList, string> _hintTitleTexts;
    private Dictionary<StageList, string> _hintTexts;

    [Header("Exit Game UI")]
    [SerializeField] private GameObject exitGameUI;
    [SerializeField] private Button exitGameYesButton;
    [SerializeField] private Button exitGameCancelButton;

    [Header("라디오 UI 텍스트")]
    [SerializeField] private GameObject radioMessageUI;
    [SerializeField] private TextMeshProUGUI radioMessageNpcName;
    [SerializeField] private TextMeshProUGUI radioMessageText;
    [SerializeField] private Button radioMessageButton;
    [SerializeField] private TextMeshProUGUI radioMessageButtonText;
    [SerializeField] private Image radioMessagePortrait;

    [Header("라디오 발신자 안내 UI")] 
    [SerializeField] private GameObject callingMessageUI;
    [SerializeField] private TextMeshProUGUI callerNameText;
    [SerializeField] private Image callerPortrait;
    [SerializeField] private Button callerAcceptButton;
  
    public enum NPC
    {
        None = -1,
        Tutorial = 0,
        Boss,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6
    }

    private readonly StringBuilder _stringBuilder = new StringBuilder();
    private readonly WaitForSeconds _waitForSeconds = new WaitForSeconds(0.025f);
    
    private NPC _talkNPC;
    private int _currentTextIndex, _endTextIndex;
    private Button _currentMessageButton;
    private TextMeshProUGUI _currentMessageButtonText;
    private GameObject _currentMessageUI;
    private TextMeshProUGUI _currentMessageTextUi;
    private Image _currentMessagePortrait;
    

    private void Awake()
    {
        _npcTexts = new Dictionary<NPC, string[]>
        {
            [NPC.Tutorial] = _globalTextStrings,
            [NPC.Boss]  = _bossTextStrings,
        };

        _npcNames = new Dictionary<NPC, string>()
        {
            [NPC.Tutorial] = "튜토리얼 관계자",
            [NPC.Boss] = "사장",
        };
        
        _npcPortraits = new Dictionary<NPC, Sprite>()
        {
            [NPC.Tutorial] = npcPortraitSprites[0],
            [NPC.Boss] = npcPortraitSprites[1],
        };

        _todoListTexts = new Dictionary<StageList, string>()
        {
            [StageList.Tutorial] = "문을 열고 튜토리얼을 진행하자",
            [StageList.GrabTutorial] = "3개의 물건들을 잡아서 넣자",
            [StageList.Stage1] = "1단계 튜토리얼을 진행하자",
            [StageList.Stage2] = "2단계 튜토리얼을 진행하자",
            [StageList.Stage3] = "3단계 튜토리얼을 진행하자",
            [StageList.Stage4] = "4단계 튜토리얼을 진행하자",
            [StageList.Stage5] = "5단계 튜토리얼을 진행하자",
            [StageList.Stage6] = "6단계 튜토리얼을 진행하자",
        };

        _hintTitleTexts = new Dictionary<StageList, string>()
        {
            [StageList.Tutorial] = "튜토리얼 # 1",
            [StageList.GrabTutorial] = "튜토리얼 # 2",
            [StageList.Stage1] = "1 스테이지",
            [StageList.Stage2] = "2 스테이지",
            [StageList.Stage3] = "3 스테이지",
            [StageList.Stage4] = "4 스테이지",
            [StageList.Stage5] = "5 스테이지",
            [StageList.Stage6] = "6 스테이지",
            
        };
        
        _hintTexts = new Dictionary<StageList, string>()
        {
            [StageList.Tutorial] = "● 튜토리얼 # 1 힌트 내용 ~~~",
            [StageList.GrabTutorial] = "● 튜토리얼 # 2 힌트 내용 ~~~",
            [StageList.Stage1] = "● 1스테이지 힌트 내용  ~~~",
            [StageList.Stage2] = "● 2스테이지 힌트 내용  ~~~",
            [StageList.Stage3] = "● 3스테이지 힌트 내용  ~~~",
            [StageList.Stage4] = "● 4스테이지 힌트 내용  ~~~",
            [StageList.Stage5] = "● 5스테이지 힌트 내용  ~~~",
            [StageList.Stage6] = "● 6스테이지 힌트 내용  ~~~",
        };
    }

    
    
    /// <summary>
    /// UI 버튼들 이벤트 초기화
    /// </summary>
    public void UiButtonEventInit()
    {
        optionUiCloseButton.onClick.AddListener( () =>
        {
            if(_lastOptionUi != null) CloseMessageUI(_lastOptionUi.gameObject, 0.25f);
            CloseMessageUI(optionUI, 0.25f);
        });
        
        todoListUiButton.onClick.AddListener(() => EnableOptionUI(OptionListUI.toDoList));
        todoListCloseButton.onClick.AddListener( () =>
        {
            CloseMessageUI(toDoListUI, 0.25f);
            HideTodoListObjects();
        });
        
        hintUiButton.onClick.AddListener(() => EnableOptionUI(OptionListUI.hintUI));
        hintUiCloseButton.onClick.AddListener( () => CloseMessageUI(hintUI, 0.25f));
        
        exitGameUiButton.onClick.AddListener( () => EnableOptionUI(OptionListUI.extiGameUI));
        exitGameCancelButton.onClick.AddListener( () => CloseMessageUI(exitGameUI, 0.25f));
        exitGameYesButton.onClick.AddListener(() => GameManager.Instance.ExitGameProgram());
        
        callerAcceptButton.onClick.AddListener( () =>
        {
            // 전화 진동 효과 중단
            GameManager.Instance.StopControllerRepeatHaptic();
            CloseMessageUI(callingMessageUI.gameObject, 0f);
            // Todo : 전화 받기 클릭 효과음
            EnableRadioMessageUI();
        });
    }

    #region 글로벌 및 라디오 UI

    public void SetMsgIndex(int startTextIndex, int endTextIndex, NPC npcType)
    {
        _currentTextIndex = startTextIndex;
        _endTextIndex = endTextIndex;
        _talkNPC = npcType;
    }

    public void EnableGlobalMessageUI()
    {
        _currentMessageButton = globalMessageButton;
        _currentMessageButtonText = globalMessageButtonText;
        _currentMessageTextUi = globalMessageText;
        _currentMessageUI = globalMessageUI;
        _currentMessagePortrait = globalMessagePortrait;
        
        _currentMessageTextUi.text = string.Empty;
        globalMessageNpcName.text = _npcNames[_talkNPC];
        globalMessageButtonText.text = "계속";

        globalMessageButton.gameObject.SetActive(false);  // TextProcess 코루틴에서 활성화 해줌
        ChangeMessageButtonEvent(() => ChangeMessageText(_npcTexts[_talkNPC]));
        ChangeMessagePortrait(_npcPortraits[_talkNPC]);
        
        DOTweenManager.DoScaleToBig(globalMessageUI.transform, () =>
        {
            ChangeMessageText(_npcTexts[_talkNPC]);
        });
    }
    
    public void EnableRadioMessageUI()
    {
        _currentMessageButton = radioMessageButton;
        _currentMessageButtonText = radioMessageButtonText;
        _currentMessageTextUi = radioMessageText;
        _currentMessageUI = radioMessageUI;
        _currentMessagePortrait = radioMessagePortrait;
        
        _currentMessageTextUi.text = string.Empty;
        radioMessageNpcName.text = _npcNames[_talkNPC];
        radioMessageButtonText.text = "계속";

        radioMessageButton.gameObject.SetActive(false); // TextProcess 코루틴에서 활성화 해줌
        ChangeMessageButtonEvent(() => ChangeMessageText(_npcTexts[_talkNPC]));
        ChangeMessagePortrait(_npcPortraits[_talkNPC]);

        DOTweenManager.DoScaleToBig(radioMessageUI.transform, () =>
        {

            ChangeMessageText(_npcTexts[_talkNPC]);
        });
    }

    public void EnableCallingUI()
    {
        if(radioMessageUI.activeSelf) radioMessageUI.SetActive(false);
        if(callingMessageUI.activeSelf) callingMessageUI.SetActive(false);
        
        // 발신자 이름, 초상화 변경
        callerNameText.text = "발신자 : " + _npcNames[_talkNPC];
        callerPortrait.sprite = _npcPortraits[_talkNPC];
        
        DOTweenManager.DoScaleToBig(callingMessageUI.transform, () =>
        {
            // 전화 진동 효과 진행
             GameManager.Instance.ControllerRepeatHaptic(GameManager.ControllerHand.Right, 1f);
        });
        

    }

    private void ChangeMessagePortrait(Sprite portrait)
    {
        _currentMessagePortrait.sprite = portrait;
    }

    private void ChangeMessageText(string[] textArray)
    {
        StartCoroutine(TextEffectProcess(textArray[_currentTextIndex]));

        if (_currentTextIndex < _endTextIndex)
        {
            _currentTextIndex++;
        }
        else
        {
            _currentMessageButtonText.text = "닫기";
            ChangeMessageButtonEvent(() => CloseMessageUI(_currentMessageUI));
        }
    }

    private IEnumerator TextEffectProcess(string msg)
    {
        _currentMessageButton.gameObject.SetActive(false);
        
        _currentMessageTextUi.text = string.Empty;
        _stringBuilder.Clear();

        foreach (var word in msg)
        {
            _stringBuilder.Append(word);
            _currentMessageTextUi.text = _stringBuilder.ToString();
            yield return _waitForSeconds;
        }
        
        _currentMessageButton.gameObject.SetActive(true);
    }

    private void ChangeMessageButtonEvent(UnityAction buttonEvent)
    {
        _currentMessageButton.onClick.RemoveAllListeners();
        _currentMessageButton.onClick.AddListener(buttonEvent);
    }



    #endregion

    #region 옵션 UI
    

    
    /// <summary>
    /// 옵션 UI 컨트롤
    /// </summary>
    /// <param name="optionListUI">활성화 옵션 UI 대상</param>
    public void EnableOptionUI(OptionListUI optionListUI)
    {
        // 전 옵션 UI가 활성화 된 경우 
        if (_lastOptionUi != null)
        {
            _lastOptionUi.gameObject.SetActive(false);
            _lastOptionUi = null;
        }
        
        UnityAction eventArg = null;
        
        // 옵션 UI 객체 등록
        switch (optionListUI)
        {
            case OptionListUI.optionUI : _lastOptionUi = optionUI.transform; break;
            case OptionListUI.toDoList : 
                _lastOptionUi = toDoListUI.transform;
                HideTodoListObjects();
                eventArg = ShowTodoListObjects;
                break;
            case OptionListUI.hintUI : _lastOptionUi = hintUI.transform; break; 
            case OptionListUI.extiGameUI : _lastOptionUi = exitGameUI.transform; break;
            
            default: return;
        }

        // 옵션 관련 UI SetActive
        DOTweenManager.DoScaleToBig(_lastOptionUi.transform, eventArg);
    }
    
    #endregion

    #region Todo List UI

    /// <summary>
    /// To do List 등록
    /// </summary>
    /// <param name="stage">스테이지 종류</param>
    public void CreateNewTodoList(StageList stage)
    {
        if (_todoListObjects.ContainsKey(stage))
        {
            Debug.LogError(stage + "의 TodoList 딕셔너리가 이미 존재합니다.");
            return;
        }

        TodoListObject todoListObject = Instantiate(todoListPrefab, todoListContentRoot).GetComponent<TodoListObject>();
        
        DOTweenManager.DoScaleToBig(todoListObject.transform, () => todoListObject.SetTodoListData(_todoListTexts[stage]) );
        
        _todoListObjects.Add(stage, todoListObject);
    }

    /// <summary>
    /// To do List 클리어
    /// </summary>
    /// <param name="clearStage">클리어 스테이지 종류</param>
    public void ClearTodoList(StageList clearStage)
    {
        if (!_todoListObjects.ContainsKey(clearStage))
        {
            Debug.LogError(clearStage + "의 TodoList 딕셔너리가 존재 하지 않습니다.");
            return;
        }

        TodoListObject todoListObject = _todoListObjects[clearStage];
        
        todoListObject.ClearTodoList();
    }

    /// <summary>
    /// To do List 객체들 모두 활성화
    /// </summary>
    private void ShowTodoListObjects()
    {
        _todoListCoroutine =  StartCoroutine(SpawnTodoListObjectProcess());
    }

    private IEnumerator SpawnTodoListObjectProcess()
    {
        Dictionary<StageList, bool> clearStages = GameManager.Instance.GetClearStages();

        foreach (var todoListObject in _todoListObjects)
        {
            var currentTodoListObject = todoListObject.Value; // 현재 TodoListObject를 복사

            DOTweenManager.DoScaleToBig(currentTodoListObject.transform, () =>
            {
                // 이미 클리어한 To do List인 경우 다시 연출 진행
                if (clearStages[todoListObject.Key]) currentTodoListObject.ClearTodoList();
            });

            yield return _DELAY;
        }
    }

    /// <summary>
    /// To do List 객체들 모두 비활성화
    /// </summary>
    private void HideTodoListObjects()
    {
        if (_todoListCoroutine != null)
        {
            StopCoroutine(_todoListCoroutine);
            _todoListCoroutine = null;
        }
        
        foreach (var todoListObject in _todoListObjects.Values)
        {
            todoListObject.gameObject.SetActive(false);
        }
    }

    #endregion

    #region Hint UI

    public void SetStageHint(StageList stageList)
    {
        this.hintTitleText.text = _hintTitleTexts[stageList];
        this.hintText.text = _hintTexts[stageList];
    }

    #endregion
    

    #region 공통 UI 닫기

    private void CloseMessageUI(GameObject targetMessageUI, float duration = 0.5f) // UI 닫기 시간
    {
        UnityAction closeAction = () => targetMessageUI.SetActive(false);
        DOTweenManager.DoScaleToSmall(targetMessageUI.transform, closeAction, duration);

        if (_lastOptionUi == targetMessageUI.transform) _lastOptionUi = null;
    }


    #endregion
}
