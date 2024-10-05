using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToStart : MonoBehaviour
{
    bool started = false;
    public GameObject CollectionUITrigger;
    void Start()
    {
        EventManager.instance.MouseDownEvent += GameStart; // 이벤트 등록
        started = true;

        // 전면광고 보여주기
        IntersitialAd.instance?.ShowAd();
        // 광고 로드 시작
        RewardAdRevive.instance?.LoadAd();
        IntersitialAd.instance?.LoadAd();

    }

    void OnEnable()
    {
        // 게임 오브젝트가 활성화될 때 코루틴을 시작
        if (started == true)
        {
            EventManager.instance.MouseDownEvent += GameStart;
        }
    }

    void OnDisable()
    {
        // 이벤트 구독 해제 (필수)
        EventManager.instance.MouseDownEvent -= GameStart;
    }

    void GameStart(Vector2 mousePosition)
    {
        if (GameManager.instance.gameState == GameManager.GameState.Main)
        {
            GameManager.instance.gameState = GameManager.GameState.Ready; // 상태변경
            EventManager.instance.TriggerStateChanged(); // 상태변경 이벤트 트리거
            gameObject.SetActive(false); // 메인화면 ui 제거
            CollectionUITrigger.SetActive(false);
        }
    }
}
