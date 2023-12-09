using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSound : MonoBehaviour
{   
    public AudioClip soundClip; // 사운드 클립들을 할당하는 배열
    [Header("텍스트 출력 간 간격 설정, 기본값 0.15")]
    // UIManager와 동일한 출력간격 0.15초
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
            AudioSource.PlayClipAtPoint(soundClip, transform.position);
            yield return new WaitForSeconds(textInterval); // 일정 시간 간격으로 대기
        }
    }
}