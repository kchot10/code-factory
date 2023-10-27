using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    [SerializeField] private Animation Animation;
    private int _step2Count = 0;

    //private void Start()
    //{
    //    Debug.Log(Animation.name);
    //    Process();
    //}

    private void Process()
    {
        Animation.Play();
        // TODO : ???????? ????
    }

    public void IncreaseStep2Score()
    {
        Debug.Log("stage1 ???");

        _step2Count = Mathf.Clamp(_step2Count + 1, 0, 1); // _step2Count?? 1 ???????? 0???? 3 ?????? ????

        if (_step2Count == 1)
        {
            Process();
        }
    }

    public void DecreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count - 1, 0, 3); // _step2Count?? 1 ???????? 0???? 3 ?????? ????
    }
}
