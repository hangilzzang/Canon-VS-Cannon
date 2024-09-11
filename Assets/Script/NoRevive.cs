using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class NoRevive : MonoBehaviour
{
    public GameObject reviveUI;
    public AudioSource bgm;
    public Image image;
    float fadeoutTime = 0.5f;

    public AudioSource rockBreak;
    
    // 점수 ui 이동
    public RectTransform scoreUI;
    float moveDistance1 = 300f;
    float duration1 = 0.6f;

    float moveDistance2 = 520f;
    float duration2 = 0.5f;

    public GameObject castle;
    public GameObject spareCastle;
    // 카메라
    public GameOver gameOverScript;
    Vector3 targetPosition =  new Vector3(0f, 0f, -10f);
    float cameraMoveDuration = 1f; // 이동에 걸리는 시간

    // 광고 아이콘
    public Button adButton;
    public ReviveUI reviveScript;

    // 광고
    Coroutine NoReviveCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.StateChangeEvent += NoRevive_;
        EventManager.instance.watchedRewardAdEvent += ReviveReward;
    }

    void OnDisable()
    {
        // 이벤트 해지
        EventManager.instance.StateChangeEvent -= NoRevive_;
        EventManager.instance.watchedRewardAdEvent -= ReviveReward;
    }

    void NoRevive_()
    {
        if (GameManager.instance.gameState == GameManager.GameState.NoRevive)
        {
            NoReviveCoroutine = StartCoroutine(NoRevive__());
        }
    } 

    IEnumerator NoRevive__()
    {
        // 시간 정상화
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.01f;
        
        // reviveUI 끄기
        reviveUI.SetActive(false);


        // 컬러 정상화
        Color imageColor = image.color;
        image.color = new Color(imageColor.r, imageColor.g, imageColor.b, 0f); 
        
        yield return new WaitForSeconds(1f);  // 잠시대기 

        // 최고기록 업데이트
        GameManager.instance.UpdateBestScore();

        // 현재 기록 업데이트 
        GameManager.instance.UpdateCurrentScore();
        
        // 실행시간 0.3초
        scoreUI.DOAnchorPosY(scoreUI.anchoredPosition.y - moveDistance1, duration1)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => 
            {
                scoreUI.DOAnchorPosY(scoreUI.anchoredPosition.y + moveDistance2, duration2)
                    .SetEase(Ease.InQuad);
            });
        
        yield return new WaitForSeconds((duration1+duration2-fadeoutTime));  // 잠시대기 
        
        StartCoroutine(GameManager.instance.FadeTo(image, 1f, fadeoutTime));
        StartCoroutine(GameManager.instance.FadeVolume(bgm, 0f, fadeoutTime));
        StartCoroutine(GameManager.instance.FadeVolume(rockBreak, 0f, fadeoutTime));

        yield return new WaitForSeconds(fadeoutTime);  // 잠시대기 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void AdIconClicked()
    {
        // 버튼 비활성화 & 버튼 커졌다작아졌다 중지
        adButton.interactable = false;
        reviveScript.StopAdIconLoop();

        if (RewardAdRevive.instance == null) // 노광고 버전이면 
        {
            StartCoroutine(AdClickCoroutine()); // 바로 부활
        }
        else // 광고버전이면
        {
            NoReviveCoroutine = StartCoroutine(NoRevive__()); // 끝내는 모션
            RewardAdRevive.instance.ShowAd();
        }
    }

    public void ReviveReward()
    {
        StopCoroutine(NoReviveCoroutine); // 끝내는 동작 멈춤
        StartCoroutine(AdClickCoroutine()); // 부활 보상
    }


    public IEnumerator AdClickCoroutine() // 부활 보상
    {
        // 시간 정상화
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.01f;
        
        // reviveUI 끄기
        reviveUI.SetActive(false);

        // 컬러 정상화
        Color imageColor = image.color;
        image.color = new Color(imageColor.r, imageColor.g, imageColor.b, 0f); 
        EventManager.instance.TriggerDestoryAllCannonBallEvent(); // 대포알들 다 제거
        
        castle.SetActive(false); // 잔해 치우기
        spareCastle.SetActive(true); // 성 리필

        yield return new WaitForSeconds(1f);  // 잠시대기 

        // 카메라 이동
        StartCoroutine(gameOverScript.MoveCameraToPosition(targetPosition, cameraMoveDuration));
        yield return new WaitForSeconds(1f);  // 잠시대기 
        
        
        GameManager.instance.gameState = GameManager.GameState.Game; // 게임상태 바꾸기 대포 쏘기 가능
        EventManager.instance.TriggerStateChanged(); // 적대포 사격 시작
    }
}
