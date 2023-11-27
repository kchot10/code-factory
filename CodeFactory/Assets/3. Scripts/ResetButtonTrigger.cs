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

    [Header("리셋 버튼 콜라이더")]
    [SerializeField] private BoxCollider restButtonCollider;
    private bool _delayTimeActive = false;
    private readonly WaitForSeconds _delayTime = new WaitForSeconds(2f);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ResetButton") && !_delayTimeActive)
        {
            Debug.Log("리셋 버튼 눌림!");
            restButtonCollider.enabled = false;
            onPressed?.Invoke();
            StartCoroutine(nameof(WaitResetButtonTrigger));
        }
    }

    private IEnumerator WaitResetButtonTrigger()
    {
        _delayTimeActive = true;
        yield return _delayTime;
        _delayTimeActive = false;
        restButtonCollider.enabled = true;
        onReleased?.Invoke();
    }
}
