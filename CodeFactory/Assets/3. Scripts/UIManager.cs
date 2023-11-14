using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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
    
    [Header("옵션 UI 텍스트")]
    [SerializeField] private GameObject global2MessageUI;
    [SerializeField] private TextMeshProUGUI global2MessageNpcName;
    [SerializeField] private TextMeshProUGUI global2MessageText;
    [SerializeField] private Button global2MessageButton;
    [SerializeField] private TextMeshProUGUI global2MessageButtonText;
    [SerializeField] private Image global2MessagePortrait;

    [Header("라디오 UI 텍스트")]
    [SerializeField] private GameObject radioMessageUI;
    [SerializeField] private TextMeshProUGUI radioMessageNpcName;
    [SerializeField] private TextMeshProUGUI radioMessageText;
    [SerializeField] private Button radioMessageButton;
    [SerializeField] private TextMeshProUGUI radioMessageButtonText;
    [SerializeField] private Image radioMessagePortrait;

  
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
    private readonly WaitForSeconds _waitForSeconds = new WaitForSeconds(0.05f);
    
    private  NPC _talkNPC;
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
    
    }

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
        ChangeMessageButtonEvent(() => ChangeMessageText(_npcTexts[_talkNPC]));
        ChangeMessagePortrait(_npcPortraits[_talkNPC]);
        DOTweenManager.DoScaleToBig(globalMessageUI.transform, () => ChangeMessageText(_npcTexts[_talkNPC]));
    }
    
    public void EnableRadioMessageUI()
    {
        _currentMessageButton = radioMessageButton;
        _currentMessageButtonText = radioMessageButtonText;
        _currentMessageTextUi = radioMessageText;
        _currentMessageUI = radioMessageUI;
        _currentMessagePortrait = radioMessagePortrait;
        
        radioMessageNpcName.text = _npcNames[_talkNPC];
        radioMessageButtonText.text = "닫기";
        ChangeMessageButtonEvent(() => ChangeMessageText(_npcTexts[_talkNPC]));
        ChangeMessagePortrait(_npcPortraits[_talkNPC]);
        DOTweenManager.DoScaleToBig(radioMessageUI.transform, () => ChangeMessageText(_npcTexts[_talkNPC]));
    }

    public void ChangeMessagePortrait(Sprite portrait)
    {
        _currentMessagePortrait.sprite = portrait;
    }

    public void ChangeMessageText(string[] textArray)
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

    private void CloseMessageUI(GameObject targetMessageUI)
    {
        DOTweenManager.DoScaleToSmall(targetMessageUI.transform, () => targetMessageUI.SetActive(false));
    }
}
