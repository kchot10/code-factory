using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialRoom : MonoBehaviour
{
    [SerializeField] private ParticleSystem tutorialExitGuideLineFX;
    [SerializeField] private ParticleSystem tutorialExitDoorFX;
    [SerializeField] private Animation tutorialExitDoorAnimation;
    [SerializeField] private Animation tutorialExitLightAnimation;
    [SerializeField] private AudioSource tutorialDoorAudioSource;
    [SerializeField] private AudioClip[] tutorialDoorAudioClips;


    [SerializeField] private Animation tutorialMetalDoorAnimation;
    
    private int _step2Count = 0;



    private void TutorialExitDoorProcess()
    {
        tutorialExitGuideLineFX.Play();
        tutorialExitDoorFX.Play();
        tutorialExitDoorAnimation.Play();
        tutorialExitLightAnimation.Play();
        GameManager.Instance.ClearTodoListUpdate(UIManager.StageList.GrabTutorial);
        GameManager.Instance.CallGlobalMessage(7, 7, UIManager.NPC.Boss);
        // TODO : 사운드도 추가
    }

    public void TutorialMetalDoorProcess(string clipName)
    {
        tutorialMetalDoorAnimation.Play(clipName);
        UI_SoundManager.PlayOnShotSound(tutorialDoorAudioSource, tutorialDoorAudioClips[0], 1f, false);
        UI_SoundManager.PlayOnShotSound(tutorialDoorAudioSource, tutorialDoorAudioClips[1], 1f, false);
       // SoundManager.PlayOnDelaySound(tutorialDoorAudioSource, tutorialDoorAudioClips[1], 5f, 1f);
    }
    


    public void IncreaseTutorialStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count + 1, 0, 1); // _step2Count를 1 증가하고 0에서 3 사이로 제한

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
