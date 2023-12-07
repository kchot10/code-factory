using UnityEngine;
using UnityEngine.EventSystems;

public class UI_OptionUiMouseHoverSoundEvent : MonoBehaviour, IPointerEnterHandler
{
    
    // UI 옵션에 버튼 Hover시 사운드 실행
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Instance.UISoundManager.ControllerOptionUiSound(true);   
    }
}
