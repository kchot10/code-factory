using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSound : MonoBehaviour
{   
    [Header("사운드 클립 배열 햘당하는 곳")]
    public AudioClip[] soundClips; // 사운드 클립들을 할당하는 배열
    [Header("텍스트 출력 간 간격 설정, 기본값 0.15")]
    public float textInterval = 0.15f; // 텍스트 간격 설정
    [Header("테스트용 문장, 한글만 넣을것")]
    public string TestText = "오! 이야기 들었네 이번에 처음 들어온 사원인가?";
    void Start()
    {
        string text = TestText; // 재생할 텍스트
        StartCoroutine(ShowTextWithSound(text));
    }

    IEnumerator ShowTextWithSound(string text)
    {
        foreach (char c in text)
        {   
            // 옮길 때 들고갈 것 ------
            int unicodeValue = c; // 유니코드 값 가져오기, 가(16진수 AC00, 10진수 44032)-힣(16진수 D7A3, 10진수 55023)
            // 한글만 소리나게 한다. 띄어쓰기, 특수문자, 영어 제외 ( 대사에 영어는 없음 )
            if(unicodeValue >= 44032 && unicodeValue <= 55023){
                int soundIndex = unicodeValue  % soundClips.Length ; // 나머지를 이용하여 사운드 클립 선택
                PlaySound(soundIndex);
                //Debug.Log(" 텍스트 : " + c + ", 유니코드 : "+ unicodeValue +", 소리 인덱스 :" + soundIndex);
            }
            // 여기까지 ------ 
            yield return new WaitForSeconds(textInterval); // 일정 시간 간격으로 대기
        }
    }
    // PlaySound 전체도 꼭 들고가기
    private void PlaySound(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < soundClips.Length)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(soundClips[soundIndex]);
            Destroy(audioSource, soundClips[soundIndex].length);
        }
    }
}