using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToStart : MonoBehaviour
{
    public GameObject touch;
    void Start()
    {
        EventManager.instance.MouseDownEvent += GameStart; // 이벤트 등록
    }

    void OnDisable()
    {
        // 이벤트 구독 해제 (필수)
        EventManager.instance.MouseDownEvent -= GameStart;
    }

    void GameStart()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Main)
        {
            GameManager.instance.gameState = GameManager.GameState.Ready; // 상태변경
            EventManager.instance.TriggerStateChanged(); // 상태변경 이벤트 트리거
            touch.SetActive(false);
            gameObject.SetActive(false); // 메인화면 ui 제거
        }
    }
}
