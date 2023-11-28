using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class DOTweenManager
{

    public static void DoScaleToBig(Transform target, UnityAction eventArg)
    {
        target.gameObject.SetActive(true);
        target.DOScale(Vector3.one, 1f).From(Vector3.zero).SetEase(Ease.OutBounce).OnComplete(() =>
        {
                eventArg?.Invoke();
        });
    }
    
    public static void DoScaleToSmall(Transform target, UnityAction eventArg, float duration)
    {
        target.DOScale(Vector3.zero, duration).From(Vector3.one).OnComplete(() =>
        {
            eventArg?.Invoke();
        });
    }

    public static void DoFillAmount(Image target, UnityAction eventArg, float startValue, float endValue, float duration)
    {
        target.DOFillAmount(endValue, duration).From(startValue).OnComplete(() =>
        {
            eventArg?.Invoke();
        });
    }

}
