using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxAnimatorController : MonoBehaviour
{
    public GameObject[] boxs;
    Animator[] animator;
    private bool isMove;

    private void Start()
    {
        animator = new Animator[boxs.Length];
        for (int i = 0; i < boxs.Length; i++)
        {
            animator[i] = boxs[i].GetComponent<Animator>();
        }
        isMove = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isMove)
            {
                PlayAnimation();
                print("PlayAnimation");
            }
            else
            {
                StopAnimation();
                print("StopAnimation");
            }
            isMove = !isMove;
        }
    }

    // 애니메이션 전환을 실행하는 함수
    public void PlayAnimation()
    {
        for (int i = 0; i < animator.Length; i++)
        {
            animator[i].SetBool("isMove", true);
        }
    }

    public void StopAnimation()
    {
        for (int i = 0; i < animator.Length; i++)
        {
            animator[i].SetBool("isMove", false);
        }
    }
}
