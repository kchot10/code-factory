using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ClearListObject : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI clearListMainText;
    [SerializeField] private Text clearListTimeText;

    public void SetClearListData(string clearTitleText, int clearTime)
    {
        clearListMainText.text = clearTitleText;

        string timeText = clearTime + "초";
        clearListTimeText.DOText(timeText, 1.5f, true, ScrambleMode.Numerals);
    }

}
