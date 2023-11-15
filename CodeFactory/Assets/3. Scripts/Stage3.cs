using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3 : MonoBehaviour
{
    [SerializeField] private Animation Animation;
    [SerializeField] private Animation Animation2;
    [SerializeField] private Animation Animation3;
    [SerializeField] private Animation clearAnimation;
    private int _step2Count = 0;

    private void Process()
    {
        Animation.Play();
        Animation2.Play();
        Animation3.Play();
        clearAnimation.Play();
    }

    public void IncreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count + 1, 0, 3);
        if (_step2Count == 3)
        {
            Process();
        }
    }

    public void DecreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count - 1, 0, 3);
    }
}
