using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPoint : MonoBehaviour
{
   // 실행할 이벤트 행동
   [SerializeField] private UnityEvent onTrigger;
   
   
   [Header("대사 정보")]
   [SerializeField] private bool isGlobalText;
   [SerializeField] private int startTextIndex;
   [SerializeField] private int endTextIndex;
   [SerializeField] private UIManager.NPC npcType;
   
   /// <summary>
   /// 플레이어 감지 이벤트
   /// </summary>
   /// <param name="player">감지 대상</param>
   private void OnTriggerEnter(Collider player)
   {
      if (player.CompareTag("Player"))
      {
         // 물체 이벤트 실행
         onTrigger?.Invoke();
         
         // 대사 이벤트 실행
         if (isGlobalText)
         {
            GameManager.Instance.CallGlobalMessage(startTextIndex, endTextIndex, npcType);
         }
         else
         {
            GameManager.Instance.CallRadioMessage(startTextIndex, endTextIndex, npcType);
         } 
         
         Destroy(this.gameObject);
      }
   }
}
