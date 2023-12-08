using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialRoom : MonoBehaviour
{
    // 튜토리얼 테이블의 모든 큐브를 맞췄을 때 시작되는 FX
    [SerializeField] private ParticleSystem tutorialExitGuideLineFX;
    [SerializeField] private ParticleSystem tutorialExitDoorFX;
    [SerializeField] private Animation tutorialExitDoorAnimation;
    [SerializeField] private Animation tutorialExitLightAnimation;
    [SerializeField] private AudioSource tutorialExitDoorSmokeSFX;
    [SerializeField] private AudioSource tutorialExitDoorWarningAlarmSFX;

    // StartButton이 눌렸을 때 시작되는 애니메이션 FX
    [SerializeField] private Animation tutorialMetalDoorAnimation;
    [SerializeField] private AudioSource buttonPressSFX;
    [SerializeField] private AudioSource tutorialMetalDoorOpenSFX;
    [SerializeField] private ParticleSystem tutorialMetalDoorLeftFX;
    [SerializeField] private ParticleSystem tutorialMetalDoorRightFX;

    private int _step2Count = 0;

    private void TutorialExitDoorProcess()
    {
        tutorialExitGuideLineFX.Play();
        tutorialExitDoorFX.Play();
        tutorialExitDoorAnimation.Play();
        tutorialExitLightAnimation.Play();
        tutorialExitDoorSmokeSFX.Play();
        tutorialExitDoorWarningAlarmSFX.Play();
        GameManager.Instance.ClearTodoListUpdate(UIManager.StageList.GrabTutorial);
        GameManager.Instance.CallGlobalMessage(7, 7, UIManager.NPC.Boss);
    }

    public void TutorialMetalDoorProcess(string clipName)
    {
        tutorialMetalDoorAnimation.Play(clipName);
        buttonPressSFX.Play();
        tutorialMetalDoorOpenSFX.Play();
        tutorialMetalDoorLeftFX.Play();
        tutorialMetalDoorRightFX.Play();
    }
    


    public void IncreaseTutorialStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count + 1, 0, 3); // _step2Count를 1 증가하고 0에서 3 사이로 제한

        if (_step2Count == 3)
        {
            TutorialExitDoorProcess();
        }

    }

    public void DecreaseTutorialStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count - 1, 0, 3); // _step2Count를 1 증가하고 0에서 3 사이로 제한
    }

}
