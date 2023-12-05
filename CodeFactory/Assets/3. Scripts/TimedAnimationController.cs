using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedAnimationController : MonoBehaviour
{
    // 애니메이션 컨트롤러를 저장할 public 변수
    public Animator animatorController;

    // 애니메이션 클립 배열
    public AnimationClip[] animationClips;

    // 전환 간격
    public float switchInterval = 10f;

    // 타이머
    private float timer = 0f;

    void Start()
    {
        // 애니메이션 컨트롤러가 설정되어 있으면
        if (animatorController != null)
        {
            // Animator Controller 가져오기
            RuntimeAnimatorController controller = animatorController.runtimeAnimatorController;

            // Animator Controller가 존재하면
            if (controller != null)
            {
                // 모든 애니메이션 클립 추출
                animationClips = controller.animationClips;

                // 초기 애니메이션 재생
                PlayRandomAnimation();
            }
        }
    }

    void Update()
    {
        // 타이머 업데이트
        timer += Time.deltaTime;

        // 전환 간격마다 다음 애니메이션으로 전환
        if (timer >= switchInterval)
        {
            PlayRandomAnimation();

            // 타이머 리셋
            timer = 0f;
        }
    }

    // 랜덤한 애니메이션 재생 함수
    void PlayRandomAnimation()
    {
        // 랜덤한 애니메이션 클립 선택
        int randomIndex = Random.Range(0, animationClips.Length);

        // 현재 애니메이션 이름
        string currentAnimation = animationClips[randomIndex].name;

        // 현재 재생 중인 애니메이션 이름
        string playingAnimation = GetCurrentAnimationName();

        // 현재 애니메이션과 다음 애니메이션이 다를 경우에만 전환
        if (currentAnimation != playingAnimation)
        {
            // Animator Controller의 트랜지션 설정
            animatorController.CrossFade(currentAnimation, 0.3f);
        }
    }

    // 현재 재생 중인 애니메이션 이름 가져오기
    string GetCurrentAnimationName()
    {
        AnimatorStateInfo stateInfo = animatorController.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("Base Layer.Idle") ? "Idle" : stateInfo.fullPathHash.ToString();
    }
}

