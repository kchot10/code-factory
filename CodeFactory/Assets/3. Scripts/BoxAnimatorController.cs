using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAnimatorController : MonoBehaviour
{
    public GameObject box1;
    public GameObject box2;
    Animator animator1;
    Animator animator2;
    private bool isMove;

    private void Start()
    {
        animator1 = box1.GetComponent<Animator>();
        animator2 = box2.GetComponent<Animator>();
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
        animator1.SetBool("isMove", true);
        animator2.SetBool("isMove", true);
    }

    public void StopAnimation()
    {
        animator1.SetBool("isMove", false);
        animator2.SetBool("isMove", false);
    }
}
