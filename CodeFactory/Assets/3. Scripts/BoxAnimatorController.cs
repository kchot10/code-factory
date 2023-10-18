using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxAnimatorController : MonoBehaviour
{
    public GameObject[] boxs;
    Animator[] animator;

    Color32 color;
    public GameObject[] clearCylinder;
    public GameObject[] failCylinder;
    Material[] clearMaterial;
    Material[] failMaterial;
    bool isAlpha;
    bool isMove;

    private void Start()
    {
        animator = new Animator[boxs.Length];
        clearMaterial = new Material[clearCylinder.Length];
        failMaterial = new Material[failCylinder.Length];
        for (int i = 0; i < boxs.Length; i++)
        {
            animator[i] = boxs[i].GetComponent<Animator>();
        }
        isMove = true;

        for (int i = 0; i < clearCylinder.Length; i++) {
            clearMaterial[i] = clearCylinder[i].GetComponent<Renderer>().material;
            failMaterial[i] = failCylinder[i].GetComponent<Renderer>().material;
        }
        isAlpha = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isMove)
            {
                PlayAnimation(0);
            }
            else
            {
                StopAnimation(0);
            }
            isMove = !isMove;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isMove)
            {
                PlayAnimation(2);
            }
            else
            {
                StopAnimation(2);
            }
            isMove = !isMove;

            SetAlpha(!isAlpha ? 0 : 0, !isAlpha ? 120 : 255);
            isAlpha = !isAlpha;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isMove)
            {
                PlayAnimation(4);
            }
            else
            {
                StopAnimation(4);
            }
            isMove = !isMove;


            SetAlpha(!isAlpha ? 1 : 1, !isAlpha ? 120 : 255);
            isAlpha = !isAlpha;
        }
    }

    public void SetAlpha(int index, int alpha_value)
    {
        clearMaterial[index].color = new UnityEngine.Color(clearMaterial[index].color.r, clearMaterial[index].color.g, clearMaterial[index].color.b, alpha_value / 255);
        failMaterial[index].color = new UnityEngine.Color(failMaterial[index].color.r, failMaterial[index].color.g, failMaterial[index].color.b, alpha_value / 255);

        print("Alpha:"+ alpha_value + " index:"+index);
    }


    // 애니메이션 전환을 실행하는 함수
    public void PlayAnimation(int boxIndex)
    {
        for (int i = boxIndex; i < boxIndex+2; i++)
        {
            animator[i].SetBool("isMove", true);
        }
        print("PlayAnimation");
    }

    public void StopAnimation(int boxIndex)
    {
        for (int i = boxIndex; i < boxIndex+2; i++)
        {
            animator[i].SetBool("isMove", false);
        }
        print("StopAnimation");
    }
}
