using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class BoxSocket : MonoBehaviour
{
    public ObjectTypeEnum TargetObjectType;

    public UnityEvent OnSokect;
    public UnityEvent UnSokect;

    //private void Start()
    //{
    //    Debug.Log("boxsocket 호출됨");

    //    OnSokect?.Invoke();
    //}

    public void OnSelectSocket(SelectEnterEventArgs targetArgs)
    {
        Debug.Log("OnSelectSocket recall");
        var objectType = targetArgs.interactableObject.transform.GetComponent<ObjectType>();

        if (objectType == null) return;

        if (objectType.Object_Type_Enum == TargetObjectType)
        {
            Debug.Log("OnSelectSocket recall in");
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
