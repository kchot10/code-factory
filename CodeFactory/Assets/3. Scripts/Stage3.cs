using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stage3 : MonoBehaviour
{
    [SerializeField] private Animator SmallShieldAnim;
    [SerializeField] private Animator MiddleShieldAnim;
    [SerializeField] private Animator BigShieldAnim;
    [SerializeField] private Animation clearAnimation;
    [SerializeField] private Material RedMaterial;
    [SerializeField] private MeshRenderer CubeCheckMeshRenderer;
    private int _step2Count = 0;
    private bool isGoal = false;
    private static Material InitialMaterial;
    private AudioSource ConveyorbeltSFX;
    private AudioSource MoterSFX;
    private AudioSource SpraySFX;

    [SerializeField] private SoundManager.SoundList ConveyorbeltSound;
    [SerializeField] private SoundManager.SoundList MoterSound;
    [SerializeField] private SoundManager.SoundList SpraySound;

    [SerializeField] private ParticleSystem[] MachinFX;
    
    [SerializeField] private Collider leverCollider;

    [SerializeField] private Image npcTextFrame;
    [SerializeField] private TextMeshProUGUI npcText;

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
        ConveyorbeltSFX = gameObject.AddComponent<AudioSource>();
        ConveyorbeltSFX.clip = soundManager.GetSoundClip(ConveyorbeltSound);
        ConveyorbeltSFX.spatialBlend = 1;
        ConveyorbeltSFX.loop = true;
        MoterSFX = gameObject.AddComponent<AudioSource>();
        MoterSFX.clip = soundManager.GetSoundClip(MoterSound);
        MoterSFX.spatialBlend = 1;
        SpraySFX = gameObject.AddComponent<AudioSource>();
        SpraySFX.clip = soundManager.GetSoundClip(SpraySound);
        SpraySFX.spatialBlend = 1;
        SpraySFX.loop = true;
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
        GameManager.Instance.UIManager.ChangeStageNpcText(2, npcText, UIManager.NPC.Steve);
        SmallShieldAnim.enabled = true;
        MiddleShieldAnim.enabled = true;
        BigShieldAnim.enabled = true;
        clearAnimation.Play();
        GameManager.Instance.ClearTodoListUpdate(UIManager.StageList.Stage3);
        GameManager.Instance.CallRadioMessage(18, 19, UIManager.NPC.Boss);
        // Todo: ���� ����
        ConveyorbeltSFX.Play();
        MoterSFX.Play();
        SpraySFX.Play();

        foreach (var particle in MachinFX)
        {
            particle.Play();
        }
    }

    private IEnumerator CubeSuccess()
    {
        GameManager.Instance.UIManager.ChangeStageNpcText(1, npcText, UIManager.NPC.Steve);
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
        _step2Count = Mathf.Clamp(_step2Count + 1, 0, 3);
        if (_step2Count == 3)
        {
            isGoal = true;
            StartCoroutine(CubeSuccess());
        }
    }

    public void DecreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count - 1, 0, 3);
    }
    public void TriggerZoneEnter()
    {
        npcTextFrame.enabled = true;
        GameManager.Instance.UIManager.ChangeStageNpcText(0, npcText, UIManager.NPC.Steve);
    }
}
