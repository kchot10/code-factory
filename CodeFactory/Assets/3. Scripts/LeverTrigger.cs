using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverTrigger : MonoBehaviour
{
    [Header("레버가 아래로 내려갈 때")]
    [SerializeField] private UnityEvent onPressed;
    [Header("레버가 위로 돌아갈 때")]
    [SerializeField] private UnityEvent onReleased;

    [Header("레버 콜라이더")]
    [SerializeField] private CapsuleCollider resetLeverCollider;
    private bool _delayTimeActive = false;
    private readonly WaitForSeconds _delayTime = new WaitForSeconds(3f);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lever") && !_delayTimeActive)
        {
            Debug.Log("레버 눌림!");
            resetLeverCollider.enabled = false;
            onPressed?.Invoke();
            StartCoroutine(nameof(WaitResetLeverTrigger));
        }
    }

    private IEnumerator WaitResetLeverTrigger()
    {
        _delayTimeActive = true;
        yield return _delayTime;
        _delayTimeActive = false;
        resetLeverCollider.enabled = true;
        onReleased?.Invoke();
    }
}
