using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage6 : MonoBehaviour
{
    [SerializeField] private Animation Animation;
    [SerializeField] private Animation Animation2;
    [SerializeField] private Animation Animation3;
    [SerializeField] private TextMeshProUGUI Stage6Text;
    private int _step2Count = 0;

    private void Process()
    {
        Animation.Play();
        Animation2.Play();
        Animation3.Play();
        StartCoroutine(TextPlay());
    }

    private IEnumerator TextPlay()
    {
        for (int i = 0; i < 9; i++)
        {
            Stage6Text.text += "나무 커팅\n";
            yield return new WaitForSeconds(1f);
        }
        Stage6Text.text += "커팅 완료\n";
    }


    public void IncreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count + 1, 0, 5);
        if (_step2Count == 5)
        {
            Process();
        }
    }

    public void DecreaseStep2Score()
    {
        _step2Count = Mathf.Clamp(_step2Count - 1, 0, 5);
    }
}
