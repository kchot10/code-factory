using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6 : MonoBehaviour
{
    [SerializeField] private Animation Animation;
    [SerializeField] private Animation Animation2;
    private int _step2Count = 0;

    private void Process()
    {
        Animation.Play();
        Animation2.Play();
    }

    public void IncreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count + 1, 0, 1);
        if (_step2Count == 1)
        {
            Process();
        }
    }

    public void DecreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count - 1, 0, 1);
    }
}
