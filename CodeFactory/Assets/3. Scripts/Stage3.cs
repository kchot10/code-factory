using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3 : MonoBehaviour
{
    [SerializeField] private Animator SmallShieldAnim;
    [SerializeField] private Animator MiddleShieldAnim;
    [SerializeField] private Animator BigShieldAnim;
    [SerializeField] private Animation clearAnimation;
    private int _step2Count = 0;

    private void Process()
    {
        SmallShieldAnim.enabled = true;
        MiddleShieldAnim.enabled = true;
        BigShieldAnim.enabled = true;
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
