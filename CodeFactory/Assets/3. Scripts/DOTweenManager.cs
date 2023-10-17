using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class DOTweenManager
{

    public static void DoScaleToBig(Transform target, UnityAction eventArg)
    {
        target.gameObject.SetActive(true);
        target.DOScale(Vector3.one, 1f).From(Vector3.zero).OnComplete(() =>
        {
                eventArg?.Invoke();
        });
    }
    
    public static void DoScaleToSmall(Transform target, UnityAction eventArg)
    {
        target.DOScale(Vector3.zero, 0.5f).From(Vector3.one).OnComplete(() =>
        {
            eventArg?.Invoke();
        });
    }

}
