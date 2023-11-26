using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetButtonTrigger : MonoBehaviour
{
    [Header("버튼이 눌렸을 때")]
    [SerializeField] private UnityEvent onPressed;
    [Header("버튼이 돌아올 때")]
    [SerializeField] private UnityEvent onReleased;
    
    private bool _delayTimeActive = false;
    private readonly WaitForSeconds _delayTime = new WaitForSeconds(1.0f);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ResetButton") && !_delayTimeActive)
        {
            Debug.Log("리셋 버튼 눌림!");
            onPressed?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ResetButton") && !_delayTimeActive)
        {
            Debug.Log("리셋 버튼 원상복구!");
            onReleased?.Invoke();
            StartCoroutine(nameof(WaitResetButtonTrigger));
        }
    }

    private IEnumerator WaitResetButtonTrigger()
    {
        _delayTimeActive = true;
        yield return _delayTime;
        _delayTimeActive = false;
    }
}
