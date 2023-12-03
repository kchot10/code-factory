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
    #region Enum

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
        Stage7 = 8,
        Exit = 9,
    }
    
    public enum OptionListUI
    {
        None = -1,
        optionUI = 0,
        toDoList = 1,
        hintUI = 2,
        exitGameUI = 3
    }
    
    public enum NPC
    {
        None = -1,
        Tutorial = 0,
        Boss,
        Megan,
        Garry,
        Steve,
        Joe,
        Jackson,
        Alex,
        Jim,
        Eugene
    }
    
    #endregion

    #region NPC 대사 텍스트

    private string[] _globalTextStrings = new string[]
    {
        // AI
        "왼쪽 버튼을 아무 손으로 가져다가 대면 상호작용합니다.",  // 0
    };

    private string[] _bossTextStrings = new string[]
    {
        // 밖
        "오! 이야기 들었네 이번에 처음 들어온 사원인가?",    // 0
        "오른손 조이스틱을 사용하여 움직일 수 있고, 왼손 조이스틱을 사용하여 시점을 돌릴 수 있다네. 한번 둘러보게나",
        "버튼을 눌러보면 문이 열릴걸세, 눌러보고 들어가보게! 공장 안으로 들어가게", 
        
        // 튜토리얼
        "안에 들어왔는가? 신입이니 아무것도 모를텐데 눈치가 빠르구만",  // 3
        "다들 최신식 기기가 어색해서 헤메더군 허허, 내가 그래서 익숙해지라고 튜토리얼을 만들었지!",
        "밑에 화살표를 따라 진행해보면 된다네, 움직이는 방법은 잘 기억하고 있으리라 믿네",
        "우리 공장에서 상호작용해볼 것들이 꽤 많이 있다네, 익숙해지기 위해 간단한 퍼즐을 준비해 보았다네 한번 풀어보게나",
        "완벽하네! 문이 열려서 들어가 볼 수 있다네, 들어와서 본격적으로 일을 해보자!",
        
        // 공장 내부
        "이곳이 우리 공장의 메인 구역이라네, 많이 없지만 꽤나 잘 돌아가고 있었지..",  // 8
        "하필 신입이 온날에 모든 공장이 고장났네, 오는 날이 장날이라고 하던가 하하..",
        "그래도 보여줄 거라도 있어야 하니 급하게라도 하나는 고쳤다네, 왼쪽에 보면 고쳐진 것이 있으니 가서 작동해보게",

        
        // 스테이지 1
        "'로켓 도색 시작' 블럭을 집어서 기계를 작동시켜 보게나!", // 11
        "신입이 기술이 엄청난데? 다음부터는 고장이 나서 직접 고쳐야 할걸세.. 다음으로 가게나",
        "장난감들은 직접 만져 볼 수 있다네!, 지루하다면 한두번씩 들어보게나, 재미난 일이 일어날 수도 있네",
        
        // 스테이지 2
        "이번에는 기본중 기본인 변수를 알아볼껄세, 한번 숙지하면 평생 써먹는 기본기중 기본기니 확실하게 해보게나", // 14
        "굉장하구만! 배우는 속도가 매우 빠른데?, 다음 기계도 고쳐보게나",
        "오리가 요새 인기가 많더군, 미국에서 강가에 고무오리를 띄우는 행사를 한다더군, 우리 회사 오리가 몇개 있을 껄세",
        
        // 스테이지 3
        "기본기가 탄탄했던 자들은 전부 오래오래 근무했었지, 지난 기계를 이렇게 빨리 고친걸 보니 오랫동안 같이 일할 수 있겠는데? 열심히 이번것도 고쳐보게나!", // 17
        "아주 완벽해, 쉽지 않았을텐데 실력자인걸?",
        "어디 미국의 영웅이 방패만 들고 한 군대와 맞서싸웠다더라고.. 우리도 하나 만들어볼까?",
        
        // 스테이지 4
        "이제부터 우리 공장들의 최고급 장비들이라네, 혹여나 고장낸다면 100년동안 일해야 할껄세, 하하 장난이네 크게 긴장하지 말게나!", // 20
        "믿고 있었다네 자네!",
        "내가 고무를 구할려고 아프리카까지 가서 신선한 고무를 구해왔지! 최상급 공이 만들어질걸세",
        
        // 스테이지 5
        "평범하게 농구공이 들어갔다가 나오니 너무 재미없고 단조롭더라고, 그래서 내가 좀 리모델링을 해보았다네!", // 23
        "신입 전에 다른데서 일하다가 왔었나? 실력이 엄청난데?",
        "농구공을 집어서 던져보게, 트램펄린 위로 올라가지는 말고?",
        
        // 스테이지 6
        "이번에 외지인의 도움을 좀 받아서 순간이동 기술을 좀 배워왔지, 초전도체를 쓴다고 하더라고 우리 공장의 첫 시범으로 써보았다네", // 26
        "잘했네! 앞으로 하나만 더 하면 퇴근할 수 있다네",
        "나무 블럭을 집어보게, 위험할 수 도 있지만 잘만 잡는다면 문제 없을걸세! 아마도..",
        
        // 스테이지 7
        "마지막 설비일세, 지금까지 훌륭하게 해온 자네에는 이번 어려운 문제도 문제 없이 풀 것이라고 믿네", // 29
        "고생했네, 이만 오늘 할 일은 다했으니 퇴근하게",
        
        // Exit
        "출구 앞에서 오늘 일당을 받아가서 퇴근하면 된다네", // 31
        "다른 직원들은 퇴근 안하냐고? 오늘 고장이 나서 늦어진 만큼 일을 더 해야 할걸세 하하하",
        "눈치보지 말고 일급받고 어서 퇴근하게, 내일도 잘 부탁해",
        "자네같은 직원이 많아야 할텐데 다들 만족스럽지 못해서 아쉽구만"
    };

    private string[] _meganTextStrings = new string[]
    {
        "이곳은 내가 고쳐놨으니 바로 작동만 시키면 돼~",
        "레버가 빛나는거 같은데? 가서 한번 눌러봐",
        "로켓이 잘 작동하는지 집어볼수 있어~ 한번 집어봐",
    };
    
    private string[] _garryTextStrings = new string[]
    {
        "신입이 오자마자 고생이 많네.",
        "오늘 일찍 집에 가긴 글렀다야..",
    };
    
    private string[] _steveTextStrings = new string[]
    {
        "이번달 할부가 얼마나 밀렸더라..",
        "방패를 끼는 영웅이라니, 조만간 사이보그도 나오겠네?",
        "신입도 방패끼고 영웅이 되면 되겠다!",
    };
    
    private string[] _joeTextStrings = new string[]
    {
        "고무가 너무 튀어서 매일매일 옷을 빨아야해..",
        "퇴근하고싶다...",
    };

    private string[] _jacksonTextStrings = new string[]
    {
        "지금이 낮인지 밤인지도 모르겠다...",
        "내가 왕년에 농구 선수였지.. 무릎에 농구공을 맞기 전까지",
    };
    
    private string[] _alexTextStrings = new string[]
    {
        "어디 먼 나라에서 외계인을 납치해서 기술 뜯어간다는 소문이 있던데 사실일까?",
        "문득 야근을 빡빡하게 굴리는 사장님이 대단하다고 새삼 느껴지네",
    };
    
    private string[] _jimTextStrings = new string[]
    {
        "이제 이것만 고치면 다 고쳐지네? 빨리빨리 움직여라 신입!",
        "야근하기 전에 빨리 끝내고 퇴근 준비해야지",
    };
    
    private string[] _eugeneTextStrings = new string[]
    {
        "오늘 일급일세. 신입치곤 꽤나 기술자인데",
        "나가는 문은 오른쪽일세, 내일도 부탁하네",
    };

    #endregion

    #region 대사 관련 딕셔너리
    
    private Dictionary<NPC, string[]> _npcTexts;
    private Dictionary<NPC, Sprite> _npcPortraits;
    private Dictionary<NPC, string> _npcNames;
    
    #endregion

    #region 대사 UI

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
    
    private readonly StringBuilder _stringBuilder = new StringBuilder();
    private readonly WaitForSeconds _waitForSeconds = new WaitForSeconds(0.025f);
    
    private NPC _talkNPC;
    private int _currentTextIndex, _endTextIndex;
    private Button _currentMessageButton;
    private TextMeshProUGUI _currentMessageButtonText;
    private GameObject _currentMessageUI;
    private TextMeshProUGUI _currentMessageTextUi;
    private Image _currentMessagePortrait;
    
    #endregion
    
    private void Awake()
    {
        _npcTexts = new Dictionary<NPC, string[]>
        {
            [NPC.Tutorial] = _globalTextStrings,
            [NPC.Boss]  = _bossTextStrings,
            [NPC.Megan] = _meganTextStrings,
            [NPC.Garry] = _garryTextStrings,
            [NPC.Steve] = _steveTextStrings,
            [NPC.Joe] = _joeTextStrings,
            [NPC.Jackson] = _jacksonTextStrings,
            [NPC.Alex] = _alexTextStrings,
            [NPC.Jim] = _jimTextStrings,
            [NPC.Eugene] = _eugeneTextStrings
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
            [StageList.Tutorial] = "플레이어를 움직여서 버튼을 눌러보자!",
            [StageList.GrabTutorial] = "책상의 물품들을 들어 알맞은 위치에 넣어보자!",
            [StageList.Stage1] = "로켓 도색 공정을 가동해보자!",
            [StageList.Stage2] = "고무오리 도색 공정을 가동해보자!",
            [StageList.Stage3] = "방패 도색 공정을 가동해보자!",
            [StageList.Stage4] = "고무공 제작 공정을 가동해보자!",
            [StageList.Stage5] = "농구공 제작 공정을 가동해보자!",
            [StageList.Stage6] = "나무 절단 공정을 가동해보자!",
            [StageList.Stage7] = "문자 프린팅 공정을 가동해보자!",
            [StageList.Exit] = "퇴근이다! 일당을 받고 퇴근하라!"
        };

        _hintTitleTexts = new Dictionary<StageList, string>()
        {
            [StageList.Stage2] = "2 스테이지",
            [StageList.Stage3] = "3 스테이지",
            [StageList.Stage4] = "4 스테이지",
            [StageList.Stage5] = "5 스테이지",
            [StageList.Stage6] = "6 스테이지",
            [StageList.Stage7] = "7 스테이지",
        };
        
        _hintTexts = new Dictionary<StageList, string>()
        {
            [StageList.Stage2] = "정수형 (Integer), int :\n" +
                                 "정수형은 프로그래밍에서 정수 값을 저장하는 변수형입니다. 양수, 음수, 0을 표현할 수 있습니다.\n" +
                                 "ex ) int age = 25; // 나이를 저장하는 정수형 변수에 25라는 값을 저장합니다.\n" +
                                 "ex ) int quantity = 10; // 상품의 수량을 저장하는 정수형 변수에 10이라는 값을 저장합니다.\n\n" +

                                 "실수형 (Floating-point), float :\n" +
                                 "실수형은 소수점을 가지는 실수 값을 저장하는 변수형입니다. 숫자의 소수점 이하 자릿수를 다룰 때 사용됩니다.\n" +
                                 "ex ) double pi = 3.14159; // 파이(원주율) 값을 저장하는 실수형 변수에 3.14159라는 값을 저장합니다.\n" +
                                 "ex ) float temperature = 26.5; // 온도를 저장하는 실수형 변수에 26.5라는 값을 저장합니다.\n\n" +

                                 "문자형 (Character), char :\n" +
                                 "문자형은 단일 문자를 저장하는 변수형입니다. 알파벳, 숫자, 특수문자 등을 저장할 수 있습니다.\n" +
                                 "ex ) char grade = 'A'; // 학점을 저장하는 문자형 변수에 'A'라는 문자를 저장합니다.\n" +
                                 "ex ) char symbol = '@'; // 특수문자 '@'를 저장하는 문자형 변수에 '@'라는 값을 저장합니다.",
            
            [StageList.Stage3] = "대입 연산자 : =\n" +
                                "'='는 변수에 값을 할당하는 대입 연산자입니다. \n" +
                                "ex ) 'a = 10'은 변수 a에 10을 할당하는 것을 의미합니다.\n\n" +

                                "같다 : ==\n" +
                                "두 개의 값이 서로 같은지 비교합니다. \n" +
                                "ex ) 'a == b'는 변수 a와 b의 값이 같은지를 확인합니다.\n\n" +

                                "다르다 : !=\n" +
                                "두 개의 값이 서로 다른지 비교합니다\n" +
                                "ex ) 'a != b'는 변수 a와 b의 값이 다른지를 확인합니다.\n\n" +

                                "크다 : > , 크거나 같다 : >=\n" +
                                "왼쪽 값이 오른쪽 값보다 큰지(>), 또는 크거나 같은지(>=) 비교합니다.\n" +
                                "ex ) 'a > b'는 변수 a가 변수 b보다 큰지를 확인합니다.\n" +
                                "ex ) 'a >= b'는 변수 a가 변수 b보다 크거나 같은지를 확인합니다.\n\n" +

                                "작다 : < , 작거나 같다 : <=\n" +
                                "왼쪽 값이 오른쪽 값보다 작은지(<), 또는 작거나 같은지(<=) 비교합니다.\n" +
                                "ex ) 'a < b'는 변수 a가 변수 b보다 작은지를 확인합니다.\n" +
                                "ex ) 'a <= b'는 변수 a가 변수 b보다 작거나 같은지를 확인합니다.",
            
            [StageList.Stage4] = "if-else 조건문의 구조:\n" +
                                 "if (조건식) \n" +
                                 "{\n" +
                                 "    // 조건식이 참일 때 실행되는 코드 블록\n" +
                                 "}\n" +
                                 "else\n" +
                                 "{\n" +
                                 "   // 조건식이 거짓일 때 실행되는 코드 블록\n" +
                                 "}\n\n" +

                                 "if-else-if 조건문의 구조:\n" +
                                 "if (조건식1) \n" +
                                 "{\n" +
                                 "    // 조건식1이 참일 때 실행되는 코드\n" +
                                 "} \n" +
                                 "else if (조건식2) \n" +
                                 "{\n" +
                                 "    // 조건식2가 참일 때 실행되는 코드\n" +
                                 "} \n" +
                                 "else\n" +
                                 "{\n" +
                                 "    // 모든 조건식이 거짓일 때 실행되는 코드\n" +
                                 "}",
            
            [StageList.Stage5] = "if-else-if 조건문의 구조:\n" +
                                 "if (조건식1) \n" +
                                 "{\n" +
                                 "    // 조건식1이 참일 때 실행되는 코드\n" +
                                 "} \n" +
                                 "else if (조건식2) \n" +
                                 "{\n" +
                                 "    // 조건식2가 참일 때 실행되는 코드\n" +
                                 "} \n" +
                                 "else\n" +
                                 "{\n" +
                                 "    // 모든 조건식이 거짓일 때 실행되는 코드\n" +
                                 "}\n" +
                                 "if-elseif의 작동 방식\n" +
                                 "1. 조건식1이 참일 경우 if(조건식1) 코드 블럭이 실행됩니다.\n" +
                                 "2. 조건식1이 거짓이고, 조건식2가 참일 경우 elseif(조건식2) 코드 블럭이 실행됩니다.\n" +
                                 "3. 모든 조건식이 거짓일 때 실행되는 else 코드 블럭이 실행됩니다.\n" +
                                 "주의사항:\n" +
                                 "※ 조건식의 순서: 조건식의 순서에 주의해야 합니다. 더 구체적인 조건을 먼저 비교해야 원하는 결과를 얻을 수 있습니다.\n" +
                                 "※ 중복된 조건식: 여러 개의 else-if 구문을 사용할 때, 중복된 조건식을 작성하지 않도록 주의해야 합니다.\n" +
                                 "※ else 구문 위치: else 구문은 모든 조건식이 거짓일 때 실행되는 코드 블록을 처리하기 위해 사용합니다.",
            
            [StageList.Stage6] =  "for 반복문의 구조:\n" +
                                  "for (초기식; 조건식; 증감식) \n" +
                                  "{\n" +
                                  "    // 반복 실행될 코드\n" +
                                  "}\n" +
                                  "초기식: 반복문이 시작될 때 한 번 실행되는 초기화 코드입니다. 반복 변수를 초기화하는 역할을 합니다.\n" +
                                  "ex ) int i = 0\n" +
                                  "조건식: 반복문이 실행될 조건을 판단하는 표현식입니다. 조건식이 참인 동안 반복문이 실행됩니다.\n" +
                                  "ex ) i < value\n" +
                                  "증감식: 반복문이 한 번 실행된 후에 반복 변수를 증가 또는 감소시키는 역할을 합니다. 후위증가식인 ++를 사용할 수도 있습니다.\n" +
                                  "※ i++ 은 i = i + 1 과 똑같은 작동을 합니다, i-- 는 i = i - 1 과 똑같은 작동을 합니다.\n" +
                                  "ex ) i++\n" +
                                  "for 반복문의 작동 방식:\n" +
                                  "1. 초기식을 실행한 후에 조건식을 평가합니다. \n" +
                                  "1-1. 조건식이 참일 경우, 코드 블록 내의 문장들이 실행됩니다.\n" +
                                  "2. 코드 블록 내의 문장들이 실행된 후에 증감식을 실행합니다. \n" +
                                  "2-1. 만약 증감식이 후의증가식 ++를 사용하면 반복 변수의 값을 증가시킵니다.\n" +
                                  "3. 그리고 다시 조건식을 평가하여 반복 실행 또는 종료합니다.\n" +
                                  "주의사항:\n" +
                                  "※ 무한 루프: 초기식, 조건식, 증감식을 적절하게 설정하지 않으면 무한 루프에 빠질 수 있습니다. 프로그램이 응답하지 않을 수 있으므로 조심해야 합니다.\n" +
                                  "※ 반복 횟수: 초기식, 조건식, 증감식을 정확하게 설정하여 원하는 반복 횟수를 얻을 수 있도록 해야 합니다.",
            
            [StageList.Stage7] ="while 반복문의 구조:\n" +
                                "while (조건식) \n" +
                                "{\n" +
                                "    // 반복 실행될 코드\n" +
                                "}\n" +
                                "조건식: 반복문이 실행될 조건을 판단하는 표현식입니다. 조건식이 참인 동안\n" +
                                "while 반복문내 코드블럭 이 실행됩니다.\n" +
                                "주의사항:\n" +
                                "※ 무한 루프: 조건식이 항상 참으로 평가되는 경우, 무한 루프에 빠질 수 있습니다. 프로그램이 응답하지 않을 수 있으므로 조심해야 합니다.\n" +
                                "※ 반복 변수의 업데이트: while 반복문에서는 반복 변수의 값을 업데이트해주어야 합니다. 그렇지 않으면 반복문이 끝나지 않을 수 있습니다.\n" +
                                "※ 초기화와 종료 조건: while 반복문은 초기화와 종료 조건을 명시적으로 처리해야 합니다. 초기화를 제대로 하지 않거나 종료 조건을 갱신하지 않으면 원하는 결과를 얻을 수 없습니다.\n" +
                                "※ 루프 제어: 반복문 내에서 적절한 루프 제어를 해야 합니다. 필요한 경우 break 문이나 continue 문을 사용하여 반복문을 제어할 수 있습니다.\n" +
                                "※※※ for 반복문과의 차이점 ※※※:\n" +
                                "1. 구조: for 반복문은 초기식, 조건식, 증감식을 한 줄에 포함하여 구성하고, while 반복문은 조건식만으로 구성합니다.\n" +
                                "2. 사용 목적: for 반복문은 반복 횟수가 명확하게 주어진 경우에 주로 사용되고, while 반복문은 특정 조건이 만족되는 동안 반복 작업을 수행해야 할 때 사용됩니다.\n" +
                                "3. 초기화와 증감: for 반복문은 초기식과 증감식을 명시적으로 포함하여 반복 변수를 초기화하고 업데이트할 수 있습니다. while 반복문은 초기화와 증감을 반복문 이전이나 이후에 따로 처리해주어야 합니다.\n" +
                                "4. 사용 편의성: for 반복문은 반복 횟수를 명시적으로 지정하고 반복 변수를 관리하는데 편리하며, while 반복문은 조건식이 동적으로 변할 수 있어 유연한 제어가 가능합니다."
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
            GameManager.Instance.EnableLeftHandWatchCollider(); // 왼손 시계 콜라이더 활성화
        });
        
        todoListUiButton.onClick.AddListener(() => EnableOptionUI(OptionListUI.toDoList));
        todoListCloseButton.onClick.AddListener( () =>
        {
            CloseMessageUI(toDoListUI, 0.25f);
            HideTodoListObjects();
        });
        
        hintUiButton.onClick.AddListener(() => EnableOptionUI(OptionListUI.hintUI));
        hintUiCloseButton.onClick.AddListener( () => CloseMessageUI(hintUI, 0.25f));
        
        exitGameUiButton.onClick.AddListener( () => EnableOptionUI(OptionListUI.exitGameUI));
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

    #region 스테이지 NPC 대사

    public void ChangeStageNpcText(int textIndex, TextMeshProUGUI npcTextUiObject, NPC npcType)
    {
        // 스테이지 NPC 대사 가져오기
        string msg = _npcTexts[npcType][textIndex];

        StartCoroutine(StageNpcTextEffectProcess(msg, npcTextUiObject));
    }

    private IEnumerator StageNpcTextEffectProcess(string msg, TextMeshProUGUI npcTextUiObject)
    {
        npcTextUiObject.text = string.Empty;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Clear();

        foreach (var word in msg)
        {
            stringBuilder.Append(word);
            npcTextUiObject.text = stringBuilder.ToString();
            yield return _waitForSeconds;
        }
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
            case OptionListUI.optionUI :
                DOTweenManager.DoScaleToBig(optionUI.transform, null);
                return;
            case OptionListUI.toDoList : 
                _lastOptionUi = toDoListUI.transform;
                HideTodoListObjects();
                eventArg = ShowTodoListObjects;
                break;
            case OptionListUI.hintUI : _lastOptionUi = hintUI.transform; break; 
            case OptionListUI.exitGameUI : _lastOptionUi = exitGameUI.transform; break;
            
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
    private void ClearTodoList(StageList clearStage)
    {
        if (!_todoListObjects.ContainsKey(clearStage))
        {
            Debug.LogError(clearStage + "의 TodoList 딕셔너리가 존재 하지 않습니다.");
            return;
        }

        TodoListObject todoListObject = _todoListObjects[clearStage];
        
        todoListObject.ClearTodoList();
        _todoListObjects.Remove(clearStage);
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
                if (clearStages[todoListObject.Key])
                {
                    ClearTodoList(todoListObject.Key);
                }
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
