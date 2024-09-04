using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    Vector3 targetPosition =  new Vector3(-9f, 0f, -10f);
    public GameObject enemyCannon;
    float moveDuration = 0.2f; // 이동에 걸리는 시간
    
    void Start()   // 이벤트 등록
    {   
        EventManager.instance.GameOverEvent += GameOver_;
    }
    void OnDisable() // 이벤트 해제
    {
        EventManager.instance.GameOverEvent -= GameOver_;
    }

    void GameOver_()
    {
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = 0.01f * Time.timeScale;
        StartCoroutine(MoveCameraToPosition());
    }
    IEnumerator MoveCameraToPosition()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f; // 경과시간
        while (elapsedTime < moveDuration)
        {
            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;
            
            // 시간에 따른 위치 보간 (Lerp)
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            
            // 다음 프레임까지 대기
            yield return null;
        }

        transform.position = targetPosition;
    }

}
