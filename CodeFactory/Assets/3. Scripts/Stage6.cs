using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stage6 : MonoBehaviour
{
    [SerializeField] private Animation Animation;
    [SerializeField] private Animation Animation2;
    [SerializeField] private Animation Animation3;
    [SerializeField] private TextMeshProUGUI Stage6Text;
    [SerializeField] private Material RedMaterial;
    [SerializeField] private MeshRenderer CubeCheckMeshRenderer;
    private int _step2Count = 0;
    private bool isGoal = false;
    private static Material InitialMaterial;
    private AudioSource ConveyorbeltSFX;
    private AudioSource WhirlpoolSFX;
    private AudioSource MoterSFX;
    private AudioSource ChainsawLong;

    [SerializeField] private SoundManager.SoundList ConveyorbeltSound;
    [SerializeField] private SoundManager.SoundList WhirlpoolSound;
    [SerializeField] private SoundManager.SoundList MoterSound;
    [SerializeField] private SoundManager.SoundList ChainsawLongSound;

    [SerializeField] private ParticleSystem[] MachinFX;
    [SerializeField] private Image npcTextFrame;
    [SerializeField] private TextMeshProUGUI npcText;

    private void Start()
    {
        // 리셋 버튼 누르면 돌아갈 위치 저장
        InitialMaterial = CubeCheckMeshRenderer.material;

        // SoundManager를 찾거나 만들어둔다.
        SoundManager soundManager = FindObjectOfType<SoundManager>();
        if (soundManager == null)
        {
            Debug.LogError("SoundManager not found in the scene.");
            return;
        }


        // OnSelectSFX에 onSelectSound 할당
        ConveyorbeltSFX = gameObject.AddComponent<AudioSource>();
        ConveyorbeltSFX.clip = soundManager.GetSoundClip(ConveyorbeltSound);
        ConveyorbeltSFX.spatialBlend = 1;
        ConveyorbeltSFX.loop = true;
        WhirlpoolSFX = gameObject.AddComponent<AudioSource>();
        WhirlpoolSFX.clip = soundManager.GetSoundClip(WhirlpoolSound);
        WhirlpoolSFX.spatialBlend = 1;
        WhirlpoolSFX.loop = true;
        MoterSFX = gameObject.AddComponent<AudioSource>();
        MoterSFX.clip = soundManager.GetSoundClip(MoterSound);
        MoterSFX.spatialBlend = 1;
        ChainsawLong = gameObject.AddComponent<AudioSource>();
        ChainsawLong.clip = soundManager.GetSoundClip(ChainsawLongSound);
        ChainsawLong.volume = 0.5F;
        ChainsawLong.spatialBlend = 1;
        ChainsawLong.loop = true;
    }

    public void Process()
    {
        if (isGoal)
        {
            MachineOperation();
        }
        else
        {
            StartCoroutine(CubeFail());
        }
    }

    private void MachineOperation()
    {
        GameManager.Instance.UIManager.ChangeStageNpcText(1, npcText, UIManager.NPC.Alex);
        Animation.Play();
        Animation2.Play();
        Animation3.Play();
        StartCoroutine(TextPlay());
        GameManager.Instance.ClearTodoListUpdate(UIManager.StageList.Stage6);
        // Todo: 성공 사운드
        ConveyorbeltSFX.Play();
        WhirlpoolSFX.Play();
        MoterSFX.Play();
        ChainsawLong.Play();

        foreach (var particle in MachinFX)
        {
            particle.Play();
        }
    }

    private IEnumerator CubeSuccess()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.3f);
            CubeCheckMeshRenderer.material = null;
            yield return new WaitForSeconds(0.3f);
            CubeCheckMeshRenderer.material = InitialMaterial;
        }
    }

    private IEnumerator CubeFail()
    {
        CubeCheckMeshRenderer.material = RedMaterial;
        yield return new WaitForSeconds(1f);
        CubeCheckMeshRenderer.material = InitialMaterial;
        // Todo: 실패 사운드
    }

    private IEnumerator TextPlay()
    {
        for (int i = 0; i < 9; i++)
        {
            Stage6Text.text += "나무 커팅\n";
            yield return new WaitForSeconds(1f);
        }
        Stage6Text.text += "커팅 완료\n";
    }


    public void IncreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count + 1, 0, 5);
        if (_step2Count == 5)
        {
            isGoal = true;
            StartCoroutine(CubeSuccess());
        }
    }

    public void DecreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count - 1, 0, 5);
    }
    public void TriggerZoneEnter()
    {
        npcTextFrame.enabled = true;
        GameManager.Instance.UIManager.ChangeStageNpcText(0, npcText, UIManager.NPC.Alex);
    }
}
