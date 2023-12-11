using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SoundManager : MonoBehaviour
{

    public enum TTS_AudioType
    {
        RadioMsgAudioSource,
        GlobalMsgAudioSource,
    }
    
    [SerializeField] private AudioSource radioCallingUiAudioSource;
    [SerializeField] private AudioSource radioMessageAudioSource;
    [SerializeField] private AudioSource globalMessageAudioSource;
    [SerializeField] private AudioSource optionUiAudioSource;

    [SerializeField] private AudioClip[] bossTTsClip;
    
    
    private void Awake()
    {
        radioCallingUiAudioSource.loop = true;
        optionUiAudioSource.loop = false;
    }

    public void ControllerRadioUiSound(bool isPlay)
    {
        if (isPlay)
        {
            radioCallingUiAudioSource.Play();
        }
        else
        {
            radioCallingUiAudioSource.Stop();
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

    public void ControllerTTS_AudioSource(TTS_AudioType ttsAudioType, int bossTTS_Index, bool isPlay)
    {
        if (ttsAudioType == TTS_AudioType.GlobalMsgAudioSource)
        {
            ControllerGlobalMessageSound(bossTTS_Index, isPlay);
        }
        else if (ttsAudioType == TTS_AudioType.RadioMsgAudioSource)
        {
            ControllerRadioMessageSound(bossTTS_Index, isPlay);
        }
    }

    private void ControllerRadioMessageSound(int bossTTsIndex, bool isPlay)
    {
        if (isPlay)
        {
            radioMessageAudioSource.clip = bossTTsClip[bossTTsIndex];

            if (radioMessageAudioSource.isPlaying)
            {
                radioMessageAudioSource.Stop();
            }
            
            radioMessageAudioSource.Play();
        }
        else
        {
            radioMessageAudioSource.Stop();
        }
    }
    
    private void ControllerGlobalMessageSound(int bossTTsIndex, bool isPlay)
    {
        if (isPlay)
        {
            globalMessageAudioSource.clip = bossTTsClip[bossTTsIndex];

            if (globalMessageAudioSource.isPlaying)
            {
                globalMessageAudioSource.Stop();
            }
            
            globalMessageAudioSource.Play();
        }
        else
        {
            globalMessageAudioSource.Stop();
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
