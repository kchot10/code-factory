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

    private Coroutine _clearProcess;
    private readonly WaitForSeconds _DELAY = new WaitForSeconds(1f);

    /// <summary>
    /// To do List 데이터 초기화
    /// </summary>
    /// <param name="todoListText">todo List 내용</param>
    public void SetTodoListData(string todoListText)
    {
        todoListClearLine.gameObject.SetActive(false);
        todoListCheckIcon.SetActive(false);
        
        this.todoListText.text = todoListText;
    }

    /// <summary>
    /// To do List 클리어
    /// </summary>
    public void ClearTodoList()
    {
        todoListCheckIcon.SetActive(true);
        todoListClearLine.gameObject.SetActive(true);
        DOTweenManager.DoFillAmount(todoListClearLine, () =>
        {
            // To do List 오브젝트 삭제 준비
            _clearProcess = StartCoroutine(nameof(DelayProcess));
        }, 0f, 1f, 0.5f);
        // TODO : eventArg에 연필로 선 끗는 사운드 넣기
    }

    private IEnumerator DelayProcess()
    {
        yield return _DELAY;
        DOTweenManager.DoScaleToSmall(this.transform, ()=> Destroy(gameObject), 0.5f);
    }
    
    private void OnDisable()
    {
        // 삭제 연출 중인 경우 코루틴을 종료하고 객체를 삭제
        if (_clearProcess != null)
        {
            StopCoroutine(_clearProcess);
            Destroy(gameObject);
        }
        todoListClearLine.gameObject.SetActive(false);
        todoListCheckIcon.SetActive(false);
    }
}
