using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecoratingUI : MonoBehaviour
{
    public Button myButton;

    void Start()
    {
        // 버튼 클릭 이벤트에 대한 리스너 추가
        myButton.onClick.AddListener(OnButtonClick);
    }

    // 버튼이 클릭되었을 때 실행될 함수
    void OnButtonClick()
    {
        Debug.Log("Button was clicked!");
        // 원하는 동작을 여기에 추가
    }
}
