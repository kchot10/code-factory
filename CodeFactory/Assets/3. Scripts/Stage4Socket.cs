using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Stage4Socket : MonoBehaviour
{
    public ObjectTypeEnum TargetObjectType;

    public UnityEvent OnSokect;
    public UnityEvent UnSokect;

    public void OnSelectSocket(SelectEnterEventArgs targetArgs)
    {
        var objectType = targetArgs.interactableObject.transform.GetComponent<ObjectType>();

        if (objectType == null) return;

        if (objectType.Object_Type_Enum == TargetObjectType)
        {
            OnSokect?.Invoke();
        }
    }

    public void OnUnSelectSocket(SelectExitEventArgs targetArgs)
    {
        var objectType = targetArgs.interactableObject.transform.GetComponent<ObjectType>();

        if (objectType == null) return;

        if (objectType.Object_Type_Enum == TargetObjectType)
        {
            UnSokect?.Invoke();
        }
    }
}
