using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    Vector3 targetPosition =  new Vector3(-9f, 0f, -10f);
    public GameObject enemyCannon;
    float moveDuration = 1f; // 이동에 걸리는 시간
    public AudioSource bgm;
    public Image image;
    public GameObject reviveUI;
    
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
        // 어두운 게임 화면 적용
        Color imageColor = image.color;
        image.color = new Color(imageColor.r, imageColor.g, imageColor.b, 0.83f); 
        // 작아지는 bgm
        bgm.volume = 0.3f;
        // 부활 ui팝업
        reviveUI.SetActive(true);
        // 시간 느리게
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = 0.01f * Time.timeScale;
        // 카메라 이동
        StartCoroutine(MoveCameraToPosition());
    }
    IEnumerator MoveCameraToPosition()
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
