using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

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
        Animation.Play();
        Animation2.Play();
        Animation3.Play();
        GameManager.Instance.ClearTodoListUpdate(UIManager.StageList.Stage5);
        // Todo: 성공 사운드
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
        // Todo: 실패 사운드
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
