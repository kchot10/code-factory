using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Stage7 : MonoBehaviour
{
    [SerializeField] private Animation Animation;
    [SerializeField] private Animation Animation2;
    [SerializeField] private TextMeshProUGUI Stage7Text;
    private int _step2Count = 0;

    private void Process()
    {
        Animation.Play();
        Animation2.Play();
        StartCoroutine(TextPlay());
    }

    private IEnumerator TextPlay()
    {
        for (int i = 0; i < 6; i++)
        {
            Stage7Text.text += "도색 시작\n";
            yield return new WaitForSeconds(1f);
        }
        Stage7Text.text += "도색 완료\n";
    }

    public void IncreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count + 1, 0, 6);
        if (_step2Count == 6)
        {
            Process();
        }
    }

    public void DecreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count - 1, 0, 6);
    }
}
