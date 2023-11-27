using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandWatch : MonoBehaviour
{
    /* <왼손 시계 콜아이더 충돌 처리>
     *  1. 콜라이더 이벤트를 이용해 옵션 UI를 활성화
     *  2. 충돌 대상은 'Hand' Tag를 가진 오브젝트
     */

    private BoxCollider _watchCollider;

    private void Awake()
    {
        _watchCollider = GetComponent<BoxCollider>();
        _watchCollider.enabled = true;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (_watchCollider.enabled && other.CompareTag("RightHand"))
        {
            GameManager.Instance.CallOptionUI(UIManager.OptionListUI.optionUI);
            _watchCollider.enabled = false;
        }
    }
    
    

    /// <summary>
    /// 왼손 손목시계 콜라이더 컨트롤
    /// </summary>
    public void EnableWatchCollider()
    {
        _watchCollider.enabled = true;
    }
}
