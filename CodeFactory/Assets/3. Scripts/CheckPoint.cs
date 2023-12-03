using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPoint : MonoBehaviour
{

   [Header("접촉 시 실행한 이벤트 정보")]
   [SerializeField] private UnityEvent onTrigger;
   
   [Header("Todo List 등록 이벤트")]
   [SerializeField] private UnityEvent<UIManager.StageList> newTodoList;
   
   [Header("Todo List 클리어 이벤트")]
   [SerializeField] private UnityEvent<UIManager.StageList> clearTodoList;
   
   [Header("스테이지 힌트 등록 이벤트")]
   [SerializeField] private UnityEvent<UIManager.StageList> setStageHint;
   
   [Header("Todo List 등록 스테이지 정보")] 
   [SerializeField] private UIManager.StageList newStageList;
   
   [Header("Todo List 클리어 스테이지 정보")]
   [SerializeField] private UIManager.StageList clearStageList;

   [Header("대사 정보")] 
   [SerializeField] private bool isNeedTalk;
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
         // 접촉 이벤트 실행
         onTrigger?.Invoke();
         
         // 할일 목록 등록 이벤트
         newTodoList?.Invoke(newStageList);
         
         // 할일 목록 클리어 이벤트
         clearTodoList?.Invoke(clearStageList);
         
         // 힌트 이벤트
         setStageHint?.Invoke(newStageList);

         // 대사가 필요하면
         if (isNeedTalk)
         {
            // 대사 이벤트 실행
            if (isGlobalText)
            {
               GameManager.Instance.CallGlobalMessage(startTextIndex, endTextIndex, npcType);
            }
            else
            {
               GameManager.Instance.CallRadioMessage(startTextIndex, endTextIndex, npcType);
            } 
         }

         Destroy(this.gameObject);
      }
   }
}
