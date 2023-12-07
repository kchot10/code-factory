using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource radioUiAudioSource;
    [SerializeField] private AudioSource optionUiAudioSource;

    private void Awake()
    {
        radioUiAudioSource.loop = true;
        optionUiAudioSource.loop = false;
    }

    public void ControllerRadioUiSound(bool isPlay)
    {
        if (isPlay)
        {
            radioUiAudioSource.Play();
        }
        else
        {
            radioUiAudioSource.Stop();
        }
    }
    
    public void ControllerOptionUiSound(bool isPlay)
    {
        if (isPlay)
        {
            optionUiAudioSource.Play();
        }
        else
        {
            optionUiAudioSource.Stop();
        }
    }

    /*
    public static void PlaySound(AudioSource targetAudioSource, AudioClip audioClip, float volume, bool isLoop)
    {
        targetAudioSource.clip = audioClip;
        targetAudioSource.loop = isLoop;
        targetAudioSource.volume = volume;
        targetAudioSource.Play();
    }

    public static void PlayOnShotSound(AudioSource targetAudioSource, AudioClip audioClip, float volume, bool isLoop = false)
    {
        targetAudioSource.loop = isLoop;
        targetAudioSource.volume = volume;
        targetAudioSource.PlayOneShot(audioClip);
    }

    public static void PlayOnDelaySound(AudioSource targetAudioSource, AudioClip audioClip, float delayTime, float volume, bool isLoop = false)
    {
        // 지연 시간 후에 재생되도록 코루틴 사용
        targetAudioSource.loop = isLoop;
        targetAudioSource.volume = volume;
        targetAudioSource.clip = audioClip;

        // 코루틴 시작
        //_instance.StartCoroutine(_instance.PlayDelayed(targetAudioSource, delayTime));
    }

    private IEnumerator PlayDelayed(AudioSource audioSource, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        audioSource.Play();
    }
    */
}
