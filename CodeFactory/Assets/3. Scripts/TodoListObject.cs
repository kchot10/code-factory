using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TodoListObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI todoListText;
    [SerializeField] private Image todoListClearLine;
    [SerializeField] private GameObject todoListCheckIcon;

    /// <summary>
    /// Todo List 데이터 초기화
    /// </summary>
    /// <param name="todoListText">todo List 내용</param>
    public void SetTodoListData(string todoListText)
    {
        todoListClearLine.gameObject.SetActive(false);
        todoListCheckIcon.SetActive(false);
        
        this.todoListText.text = todoListText;
    }

    /// <summary>
    /// Todo List 클리어
    /// </summary>
    public void ClearTodoList()
    {
        todoListCheckIcon.SetActive(true);
        todoListClearLine.gameObject.SetActive(true);
        DOTweenManager.DoFillAmount(todoListClearLine, null, 0f, 1f, 0.5f);
        // TODO : eventArg에 연필로 선 끗는 사운드 넣기
    }

    private void OnDisable()
    {
        todoListClearLine.gameObject.SetActive(false);
        todoListCheckIcon.SetActive(false);
    }
}
