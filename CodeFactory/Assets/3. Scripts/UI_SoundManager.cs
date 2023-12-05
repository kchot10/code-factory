using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SoundManager : MonoBehaviour
{
    private static UI_SoundManager _instance;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
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
        _instance.StartCoroutine(_instance.PlayDelayed(targetAudioSource, delayTime));
    }

    private IEnumerator PlayDelayed(AudioSource audioSource, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        audioSource.Play();
    }
}
