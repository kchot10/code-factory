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


    private AudioSource OnSelectSFX;
    private AudioSource OnUnSelectSFX;

    [SerializeField] private SoundManager.SoundList onSelectSound;
    [SerializeField] private SoundManager.SoundList onUnSelectSound;

    private void Start()
    {
        // SoundManager를 찾거나 만들어둔다.
        SoundManager soundManager = FindObjectOfType<SoundManager>();
        if (soundManager == null)
        {
            Debug.LogError("SoundManager not found in the scene.");
            return;
        }

        // OnSelectSFX에 onSelectSound 할당
        OnSelectSFX = gameObject.AddComponent<AudioSource>();
        OnSelectSFX.clip = soundManager.GetSoundClip(onSelectSound);
        OnSelectSFX.spatialBlend = 1;

        // OnUnSelectSFX에 onUnSelectSound 할당
        OnUnSelectSFX = gameObject.AddComponent<AudioSource>();
        OnUnSelectSFX.clip = soundManager.GetSoundClip(onUnSelectSound);
        OnSelectSFX.spatialBlend = 1;
    }

    public void OnSelectSocket(SelectEnterEventArgs targetArgs)
    {
        
        OnSelectSFX.Play();

        var objectType = targetArgs.interactableObject.transform.GetComponent<ObjectType>();

        if (objectType == null) return;

        if (objectType.Object_Type_Enum == TargetObjectType)
        {
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
