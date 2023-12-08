using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExitRoom : MonoBehaviour
{
    [SerializeField] private Image npcTextFrame;
    [SerializeField] private TextMeshProUGUI npcText;

    public void TriggerZoneEnter()
    {
        npcTextFrame.enabled = true;
        GameManager.Instance.UIManager.ChangeStageNpcText(0, npcText, UIManager.NPC.Eugene);
    }

    public void MoneySelectText()
    {
        GameManager.Instance.UIManager.ChangeStageNpcText(1, npcText, UIManager.NPC.Eugene);
    }
}
