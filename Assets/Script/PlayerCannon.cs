using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class PlayerCannon : MonoBehaviour
{
    // 게임 준비
    public GameObject IngameScore;
    public AudioSource warHorn;
    public AudioSource bgm;
    public AudioSource cannonFrame;
    public AudioClip warBGM; 
    Vector2 rotationPoint = new Vector2(-0.4994f, 0.0294f); // 로컬좌표
    float targetAngle = 80f; // 목표 회전각도
    float rotationSpeed = 31f; // 회전 속도
    float currentAngle = 0f; // 현재 회전각도
    
    // 대포 발사
    public GameObject cannonballPrefab;
    Vector2 fireAngle;
    Vector2 launchPoint = new Vector2(0.24f, -0.02f); // 로컬 좌표계의 발사 지점
    Vector2 worldLaunchPoint;
    float forceAmount = 50f; // 발사할 힘의 크기
    Rigidbody2D cannonballRigidBody; // 대포알 리지드바디
    // 대포 소리
    public AudioSource cannonBody;
    // 대포 연기
    public GameObject cannonCloud_;
    float cloudDistance = 1.13f;
    public  SpriteRenderer CannonCloud;
    float fadeInTime = 0.1f;
    float fadeOutTime = 0.7f;
    float cloudSclaeDuration = 0.8f;
    Vector2 cloudFinalScale = new Vector2(1.2f, 1.2f);
    Vector2 cloudOriginalPosition; 
    Vector2 cloudTargetPosition = new Vector2(-5.2f, -1.61f);
    
    // 대포 반동
    Vector2 originalPosition;
    Vector2 recoilPosition;
    float recoilDistance = 0.2f; // 반동 거리
    float recoilSpeed1 = 0.2f; 
    float recoilSpeed2 = 0.6f; 

    // 대포 스킨
    int selectedBall;

    public Sprite cannonBall;
    public Sprite bawlingBall;
    public Sprite tennisBall;
    public Sprite basketBall;
    public Sprite baseBall;
    public Sprite waterMelon;    

    List<Sprite> ballSprites;
    Sprite selectedSprite;

    // 튜토리얼
    public Image image;
    public GameObject fingerObj;
    public Transform finger;
    Sequence mySequence;
    public GameObject fingerText;



    void Start()   // 이벤트 등록
    {   
        EventManager.instance.StateChangeEvent += GameReady_;
        EventManager.instance.StateChangeEvent += tutorial_;
        EventManager.instance.MouseDownEvent += FireCannonBall_;
    }
    void OnDisable() // 이벤트 해제
    {
        EventManager.instance.StateChangeEvent -= GameReady_;
        EventManager.instance.StateChangeEvent -= tutorial_;
        EventManager.instance.MouseDownEvent -= FireCannonBall_;
    }

    void tutorial_()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Game && GameManager.instance.bestScoreValue == 0 && GameOver.canRevive)
        {
            StartCoroutine(tutorial());
        }
    }
    
    IEnumerator tutorial()
    {
        // 상태 변경(대포 발사 안되게)
        GameManager.instance.gameState = GameManager.GameState.Ready;
        // n초기다림
        yield return new WaitForSeconds(1.78f);
        // 화면 어두워짐
        // Color startColor = image.color;
        // image.color = new Color(startColor.r, startColor.g, startColor.b, 0.15f);
        // 터치 등장
        fingerObj.SetActive(true);
        // 터치애니메이션 재생
        mySequence = DOTween.Sequence()
            .Append(finger.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0.5f).SetUpdate(true))  // Unscaled time으로 스케일 조정
            .Join(finger.DORotate(new Vector3(0, 0, 30), 0.5f).SetUpdate(true))          // Unscaled time으로 회전
            .Join(finger.DOMove(new Vector2(-1f, 1f), 0.5f).SetRelative().SetUpdate(true)) // Unscaled time으로 이동
            .SetLoops(-1, LoopType.Yoyo).SetUpdate(true); // 무한 루프

        // 터치 텍스트
        fingerText.SetActive(true);
        // 게임상태 변경 이제 발사 가능
        GameManager.instance.gameState = GameManager.GameState.Game;
        // 시간멈춤
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
    }


    void FireCannonBall_(Vector2 mousePosition)
    {
        if (GameManager.instance.gameState == GameManager.GameState.Game)
        {
            StartCoroutine(FireCannonBall());
        }
    }
    
    
    IEnumerator FireCannonBall()
    {
        if (GameManager.instance.bestScoreValue == 0)
        {
            // 핑거 애니메이션 종료
            mySequence.Kill();
            fingerObj.SetActive(false);
            //핑거 텍스트 종료
            fingerText.SetActive(false);
            // 시간이 다시 흐름
            Time.timeScale = 1f;
            Time.fixedDeltaTime = GameManager.instance.fixedDeltaTimeValue;   
            // 화면 어두워진거 해제
            // Color startColor = image.color;
            // image.color = new Color(startColor.r, startColor.g, startColor.b, 0f);


        }


        GameManager.instance.gameState = GameManager.GameState.Reloading; // 상태변경
        
        
        GameObject cannonball = Instantiate(cannonballPrefab, worldLaunchPoint, Quaternion.identity); // 프리팹 생성
        
        SpriteRenderer spriteRenderer = cannonball.GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 불러오기
        spriteRenderer.sprite = selectedSprite; // 스프라이트 교체
        
        cannonballRigidBody = cannonball.GetComponent<Rigidbody2D>();

        cannonBody.Play();
        cannonballRigidBody.AddForce(fireAngle * forceAmount, ForceMode2D.Impulse);
        
        StartCoroutine(FireSmoke());
        StartCoroutine(Recoil());
        cannonCloud_.transform.DOScale(cloudFinalScale, cloudSclaeDuration).SetEase(Ease.OutExpo);
        cannonCloud_.transform.DOMove(cloudTargetPosition, cloudSclaeDuration).SetEase(Ease.OutExpo);
        
        yield return new WaitForSeconds(cloudSclaeDuration); 
        
        cannonCloud_.transform.position = cloudOriginalPosition;
        cannonCloud_.transform.localScale = new Vector2(0.5f, 0.5f);

        if (GameManager.instance.gameState == GameManager.GameState.Reloading)
        { 
            GameManager.instance.gameState = GameManager.GameState.Game; // 상태변경
        }
    }

    IEnumerator Recoil()
    {
        yield return StartCoroutine(GameManager.instance.Move(transform, recoilPosition, recoilSpeed1));
        yield return StartCoroutine(GameManager.instance.Move(transform, originalPosition, recoilSpeed2));
    }



    



    IEnumerator FireSmoke()
    {
        yield return StartCoroutine(FadeTo(1f, fadeInTime));
        yield return StartCoroutine(FadeTo(0f, fadeOutTime));
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        Color startColor = CannonCloud.color;
        float startAlpha = startColor.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            CannonCloud.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return null; // 다음 프레임까지 대기
        }

        CannonCloud.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        yield return null;
    }





    void GameReady_()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Ready)
        {
            StartCoroutine(GameReady());
        }
    }
    
    
    
    IEnumerator GameReady()
    {

        selectedBall = PlayerPrefs.GetInt("Selected", 0); // 가져옴

        ballSprites = new List<Sprite>
        {
            cannonBall,
            bawlingBall,
            tennisBall,
            basketBall,
            baseBall,
            waterMelon
        };
        selectedSprite = ballSprites[selectedBall]; // 사용할 볼 정함


        IngameScore.SetActive(true); // 스코어 UI
        bgm.Pause();
        bgm.clip = warBGM;
        warHorn.Play(); // 전쟁 나팔 소리 재생
        yield return new WaitWhile(() => warHorn.isPlaying); // 소리 재생이 끝날 때까지 대기
        bgm.Play(); // 전쟁 bgm 재생
        cannonFrame.Play(); // 발사각도 조절 효과음
        yield return StartCoroutine(RotateCannon()); // 대포 각도조절
        cannonFrame.Pause();
        fireAngle = transform.right; // 발사각도 결정됨
        worldLaunchPoint = transform.TransformPoint(launchPoint); // 발사위치 계산 
        
        originalPosition = transform.position; // 대포위치 계산
        recoilPosition = originalPosition + (Vector2)(-transform.right * recoilDistance); // 반동 위치 계산

        // 연기 배치
        
        cloudOriginalPosition = transform.position + transform.right * cloudDistance;
        cannonCloud_.transform.position = cloudOriginalPosition;
        cannonCloud_.transform.rotation = transform.rotation;
        
        GameManager.instance.gameState = GameManager.GameState.Game; // 상태변경
        EventManager.instance.TriggerStateChanged();
    }

    IEnumerator RotateCannon()
    {
        while (currentAngle < targetAngle)
        {
            Vector3 worldRotationPoint = transform.TransformPoint(rotationPoint);
            float angleToRotate = rotationSpeed * Time.deltaTime;
            
            if (currentAngle + angleToRotate > targetAngle) // 목표 회전각을 넘지않도록
            {
                angleToRotate = targetAngle - currentAngle;
            }

            transform.RotateAround(worldRotationPoint, Vector3.forward, angleToRotate); // z축으로 목표 회전각만큼 회전

            currentAngle += angleToRotate;
            
            yield return null; // 다음 프레임까지 대기
        }
    }
}
