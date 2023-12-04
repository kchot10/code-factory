using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialSocket : MonoBehaviour
{
    public ObjectTypeEnum TargetObjectType;

    public UnityEvent OnSokect;
    public UnityEvent UnSokect;

    [SerializeField] private AudioSource IncreaseSFX;
    [SerializeField] private AudioSource DecreaseSFX;

    public void OnSelectSocket(SelectEnterEventArgs targetArgs)
    {
        IncreaseSFX.Play();

        var objectType = targetArgs.interactableObject.transform.GetComponent<ObjectType>();

        if (objectType == null) return;
        
        if (objectType.Object_Type_Enum == TargetObjectType)
        {
            OnSokect?.Invoke();
        }

    }
    
    public void OnUnSelectSocket(SelectExitEventArgs targetArgs)
    {

        DecreaseSFX.Play();

        var objectType = targetArgs.interactableObject.transform.GetComponent<ObjectType>();

        if (objectType == null) return;
        
        if (objectType.Object_Type_Enum == TargetObjectType)
        {
            UnSokect?.Invoke();
        }
    }
}
