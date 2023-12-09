using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class OneRoom : MonoBehaviour
{
    [SerializeField] private Transform codeFactoryButton;
    [SerializeField] private Transform exitGameButton;

    [SerializeField] private AudioSource buttonHoverAudioSource;

    private static OneRoom _instance = null;
    public static  OneRoom Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }

    public void MoveCodeFactory()
    {
        SceneManager.LoadScene("CodeFactory");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowCodeFactoryButton()
    {
        DOTweenManager.DoScaleToBig(codeFactoryButton, null);
    }
    
    public void ShowExitGameButton()
    {
        DOTweenManager.DoScaleToBig(exitGameButton, null);
    }
    
    public void HideCodeFactoryButton()
    {
        DOTweenManager.DoScaleToSmall(codeFactoryButton, () => codeFactoryButton.gameObject.SetActive(false), 0.25f);
    }
    
    public void HideExitGameButton()
    {
        DOTweenManager.DoScaleToSmall(exitGameButton, () => exitGameButton.gameObject.SetActive(false), 0.25f);
    }

    public void ButtonHoverSound()
    {
        buttonHoverAudioSource.Play();
    }
}
