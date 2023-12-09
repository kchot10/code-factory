using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ExitRoom : MonoBehaviour
{
    [SerializeField] private Image npcTextFrame;
    [SerializeField] private TextMeshProUGUI npcText;

    [SerializeField] private Animation exitDoorAnimation;
    [SerializeField] private Outline exitDoorOutline; 

    public void TriggerZoneEnter()
    {
        npcTextFrame.enabled = true;
        GameManager.Instance.UIManager.ChangeStageNpcText(0, npcText, UIManager.NPC.Eugene);
    }

    public void MoneySelectText()
    {
        GameManager.Instance.UIManager.ChangeStageNpcText(1, npcText, UIManager.NPC.Eugene);
        GameManager.Instance.ClearTodoListUpdate(UIManager.StageList.Exit);
    }

    #region 탈출구 관련 함수

    [ContextMenu("Exit Room Open")]
    public void ExitRoomDoorOpen()
    {
        exitDoorAnimation.Play();
        EnableExitRoomGuideLine();
    }

    private void EnableExitRoomGuideLine()
    {
        exitDoorOutline.enabled = true;
    }


    #endregion
}
