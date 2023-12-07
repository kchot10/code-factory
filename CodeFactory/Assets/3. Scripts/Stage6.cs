using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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
        leverCollider.enabled = false;
        Animation.Play();
        Animation2.Play();
        Animation3.Play();
        StartCoroutine(TextPlay());
        GameManager.Instance.ClearTodoListUpdate(UIManager.StageList.Stage6);
        // Todo: ���� ����
        ConveyorbeltSFX.Play();
        WhirlpoolSFX.Play();
        MoterSFX.Play();
        ChainsawLong.Play();
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

    private IEnumerator TextPlay()
    {
        for (int i = 0; i < 9; i++)
        {
            Stage6Text.text += "���� Ŀ��\n";
            yield return new WaitForSeconds(1f);
        }
        Stage6Text.text += "Ŀ�� �Ϸ�\n";
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
}
