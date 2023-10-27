using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4 : MonoBehaviour
{
    [SerializeField] private ParticleSystem RubberyLiquidFX;
    private int _step2Count = 0;

    private void Process()
    {
        RubberyLiquidFX.Play();
    }

    public void IncreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count + 1, 0, 4);
        if (_step2Count == 4)
        {
            Process();
        }
    }

    public void DecreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count - 1, 0, 4);
    }
}
