using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stage5 : MonoBehaviour
{
    [SerializeField] private Animation Animation;
    [SerializeField] private Animation Animation2;
    [SerializeField] private Animation Animation3;
    [SerializeField] private Material RedMaterial;
    [SerializeField] private MeshRenderer CubeCheckMeshRenderer;
    private int _step2Count = 0;
    private bool isGoal = false;
    private static Material InitialMaterial;
    private AudioSource CartoonBoingSFX;
    private AudioSource MoterSFX;
    private AudioSource SpraySFX;
    private AudioSource SwooshSFX;

    [SerializeField] private SoundManager.SoundList CartoonBoingSound;
    [SerializeField] private SoundManager.SoundList MoterSound;
    [SerializeField] private SoundManager.SoundList SpraySound;
    [SerializeField] private SoundManager.SoundList SwooshSound;
    [SerializeField] private Image npcTextFrame;
    [SerializeField] private TextMeshProUGUI npcText;

    [SerializeField] private Collider leverCollider;        
    
    private void Start()
    {
        // ���� ��ư ������ ���ư� ��ġ ����
        InitialMaterial = CubeCheckMeshRenderer.material;

        // SoundManager�� ã�ų� �����д�.
        SoundManager soundManager = FindObjectOfType<SoundManager>();
        if (soundManager == null)
        {
            Debug.LogError("SoundManager not found in the scene.");
            return;
        }


        // OnSelectSFX�� onSelectSound �Ҵ�
        CartoonBoingSFX = gameObject.AddComponent<AudioSource>();
        CartoonBoingSFX.clip = soundManager.GetSoundClip(CartoonBoingSound);
        CartoonBoingSFX.spatialBlend = 1;
        CartoonBoingSFX.loop = true;
        MoterSFX = gameObject.AddComponent<AudioSource>();
        MoterSFX.clip = soundManager.GetSoundClip(MoterSound);
        MoterSFX.spatialBlend = 1;
        SpraySFX = gameObject.AddComponent<AudioSource>();
        SpraySFX.clip = soundManager.GetSoundClip(SpraySound);
        SpraySFX.spatialBlend = 1;
        SpraySFX.loop = true;
        SwooshSFX = gameObject.AddComponent<AudioSource>();
        SwooshSFX.clip = soundManager.GetSoundClip(SwooshSound);
        SwooshSFX.spatialBlend = 1;
        SwooshSFX.loop = true;
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
        leverCollider.enabled = false;
        GameManager.Instance.UIManager.ChangeStageNpcText(1, npcText, UIManager.NPC.Jackson);
        Animation.Play();
        Animation2.Play();
        Animation3.Play();
        GameManager.Instance.ClearTodoListUpdate(UIManager.StageList.Stage5);
        GameManager.Instance.CallRadioMessage(24, 25, UIManager.NPC.Boss);
        // Todo: ���� ����
        CartoonBoingSFX.Play();
        MoterSFX.Play();
        SpraySFX.Play();
        SwooshSFX.Play();
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
        // Todo: ���� ����
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
        GameManager.Instance.UIManager.ChangeStageNpcText(0, npcText, UIManager.NPC.Jackson);
    }
}
