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


    [SerializeField] private Animation tutorialMetalDoorAnimation;
    
    private int _step2Count = 0;



    private void TutorialExitDoorProcess()
    {
        tutorialExitGuideLineFX.Play();
        tutorialExitDoorFX.Play();
        tutorialExitDoorAnimation.Play();
        tutorialExitLightAnimation.Play();
        // TODO : 사운드도 추가
    }

    public void TutorialMetalDoorProcess(string clipName)
    {
        tutorialMetalDoorAnimation.Play(clipName);
    }
    


    public void IncreaseTutorialStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count + 1, 0, 1); // _step2Count를 1 증가하고 0에서 3 사이로 제한

        if (_step2Count == 1)
        {
            TutorialExitDoorProcess();
        }
    }

    public void DecreaseTutorialStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count - 1, 0, 3); // _step2Count를 1 증가하고 0에서 3 사이로 제한
    }

}
