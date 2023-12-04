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

    [SerializeField] private AudioSource OnSelectSFX;
    [SerializeField] private AudioSource OnUnSelectSFX;

    public void OnSelectSocket(SelectEnterEventArgs targetArgs)
    {
        OnSelectSFX.Play();

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
        OnUnSelectSFX.Play();

        var objectType = targetArgs.interactableObject.transform.GetComponent<ObjectType>();

        if (objectType == null) return;

        if (objectType.Object_Type_Enum == TargetObjectType)
        {
            UnSokect?.Invoke();
        }
    }
}
