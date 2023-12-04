using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        SmallShieldAnim.enabled = true;
        MiddleShieldAnim.enabled = true;
        BigShieldAnim.enabled = true;
        clearAnimation.Play();
        GameManager.Instance.ClearTodoListUpdate(UIManager.StageList.Stage3);
        // Todo: 성공 사운드
        ConveyorbeltSFX.Play();
        MoterSFX.Play();
        SpraySFX.Play();
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
}
