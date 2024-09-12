using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    Vector3 targetPosition =  new Vector3(-9f, 0f, -10f);
    float moveDuration = 1f; // 이동에 걸리는 시간

    public GameObject reviveUI;
    public static bool canRevive = true;
    public AudioSource rockBreak;
    
    void Start()   // 이벤트 등록
    {   
        EventManager.instance.GameOverEvent += GameOver_;
        canRevive = true;
    }
    void OnDisable() // 이벤트 해제
    {
        EventManager.instance.GameOverEvent -= GameOver_;
    }
    void GameOver_()
    {
        GameManager.instance.gameState = GameManager.GameState.NoRevive; // 상태변경
        rockBreak.Play();

        // Handheld.Vibrate();
        
        // 카메라 이동
        StartCoroutine(MoveCameraToPosition(targetPosition, moveDuration));
        
        if (canRevive) // 부활 ui팝업
        {       
            if (RewardAdRevive.instance == null) // 노광고 버전이다
            {
                reviveUI.SetActive(true);
                canRevive = false;
            }
            else if (RewardAdRevive.instance != null && RewardAdRevive.instance._rewardedAd != null) // 광고버전이며 광고가 로드되었다
            {
                reviveUI.SetActive(true);
                canRevive = false;
            }
            else // 광고 버전이지만 광고가 로드되지않았다
            {
                EventManager.instance.TriggerStateChanged();
            }
        }
        else // 두번째로 죽은경우
        {
            EventManager.instance.TriggerStateChanged();
        }
    }
    public IEnumerator MoveCameraToPosition(Vector3 targetPosition, float moveDuration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f; // 경과시간
        while (elapsedTime < moveDuration)
        {
            // 경과 시간 업데이트
            elapsedTime += Time.unscaledDeltaTime;
            
            // 시간에 따른 위치 보간 (Lerp)
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            
            // 다음 프레임까지 대기
            yield return null;
        }

        transform.position = targetPosition;

    }

}
