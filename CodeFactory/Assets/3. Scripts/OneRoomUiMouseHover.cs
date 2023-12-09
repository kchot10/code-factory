using UnityEngine;
using UnityEngine.EventSystems;

public class OneRoomUiMouseHover : MonoBehaviour, IPointerEnterHandler
{

    // UI 옵션에 버튼 Hover시 사운드 실행
    public void OnPointerEnter(PointerEventData eventData)
    {
        OneRoom.Instance.ButtonHoverSound();
    }
}
