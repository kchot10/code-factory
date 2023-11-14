using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4 : MonoBehaviour
{
    [SerializeField] private ParticleSystem RubberyLiquidFX;
    [SerializeField] private Animation Animation;
    private int _step2Count = 0;


    private void Process()
    {
        RubberyLiquidFX.Play();
        Animation.Play();
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
