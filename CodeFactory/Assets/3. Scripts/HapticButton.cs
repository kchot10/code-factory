using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HapticButton : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {   
        GameManager.Instance.CallRightControllerHaptic();
        GameManager.Instance.CallLeftControllerHaptic();
    }
}
