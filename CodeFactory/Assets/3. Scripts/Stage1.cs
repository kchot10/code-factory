using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    [SerializeField] private Animation Animation;
    private int _step2Count = 0;



    private void Process()
    {
        Animation.Play();
        // TODO : 사운드도 추가
    }

    public void IncreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count + 1, 0, 1); // _step2Count를 1 증가하고 0에서 3 사이로 제한

        if (_step2Count == 1)
        {
            Process();
        }
    }

    public void DecreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count - 1, 0, 3); // _step2Count를 1 증가하고 0에서 3 사이로 제한
    }
}
