using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    public enum GameState
    {
        Reloading,
        Main,
        Ready,
        Game,
        GameOver,
        NoRevive,
    }

    public GameState gameState;
    public Text scoreText;
    public int scoreValue = 0;
    public Text bestScore;
    public RectTransform bestScoreUI;
    float scaleSize = 1.15f;   // 최대 크기
    float duration = 0.6f;    // 애니메이션 지속 시간

    int bestScoreValue;

    public Text currentScore;

    void Awake() 
    {
        gameState = GameState.Main;
        
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.01f;
        Application.targetFrameRate = 60; // fps 60으로 설정


        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // PlayerPrefs.DeleteAll();
        // PlayerPrefs.Save();

        int lastScoreValue = PlayerPrefs.GetInt("ScoreValue", 0);
        int isNewRecord = PlayerPrefs.GetInt("NewRecord", 0);

        currentScore.text = lastScoreValue.ToString();
        
        if (isNewRecord == 1)
        {
            bestScore.text = "New Record!";
            // StartCoroutine(ChangeColorLoop(bestScore, 1f)); // 무지개색깔 변환!!
            // bestScore.color = new Color(255f / 255f, 191f / 255f, 0f / 255f); // 황금색

            bestScoreUI.DOScale(scaleSize, duration)  // 목표 크기로 애니메이션
            .SetLoops(-1, LoopType.Yoyo)        // 무한 반복, Yoyo는 커졌다가 작아지는 효과
            .SetEase(Ease.InOutQuad);  // 애니메이션 지속 시간
            
            PlayerPrefs.SetInt("NewRecord", 0);
            PlayerPrefs.Save();
        }
        else
        {
            bestScoreValue = PlayerPrefs.GetInt("BestScore", 0);
            bestScore.text = "Best score: " + bestScoreValue.ToString();
        }
    }

    IEnumerator ChangeColorLoop(Text text, float speed)
    {
        float hue = 0f;
        while (true)
        {
            Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);
            text.color = rainbowColor;

            hue += Time.deltaTime * speed;
            hue = hue % 1;

            yield return null;
        }
    }

    public void UpdateBestScore()
    {
        if (scoreValue > bestScoreValue)
        {
            PlayerPrefs.SetInt("BestScore", scoreValue);
            PlayerPrefs.SetInt("NewRecord", 1);
            PlayerPrefs.Save();
        }
    }

    public void UpdateCurrentScore()
    {
            PlayerPrefs.SetInt("ScoreValue", scoreValue);
            PlayerPrefs.Save();
    }

    public void AddScore()
    {
       scoreValue += 1;
       scoreText.text = scoreValue.ToString();

    //    if (scoreValue == bestScoreValue + 1)
    //    {

    //    }
    }


    // 위치 변경
    public IEnumerator Move<T>(T objTransform, Vector2 endPosition, float speed) where T : Component
    {
        
        float time = 0f;
        Vector2 startPosition;

        // RectTransform일 경우 anchoredPosition 사용, Transform일 경우 position 사용
        if (objTransform is RectTransform rectTransform)
        {
            startPosition = rectTransform.anchoredPosition;
            while (time < speed)
            {
                time += Time.deltaTime;
                if (time < speed)
                {
                    rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, (time / speed));
                    yield return null;
                }
                else
                {
                    transform.position = endPosition;
                }
            }
        }
        else if (objTransform is Transform transform)
        {
            startPosition = transform.position;
            while (time < speed)
            {
                time += Time.deltaTime;
                if (time < speed)
                {
                    transform.position = Vector2.Lerp(startPosition, endPosition, (time / speed));
                    yield return null;
                }
                else
                {
                    transform.position = endPosition;
                }
            }
        }
        else
        {
            yield break;  // 지원하지 않는 타입일 경우 중단
        }
    }

    // 이미지 알파값 조절 
    public IEnumerator FadeTo(Graphic uiElement, float targetAlpha, float duration)
    {
        Color startColor = uiElement.color;
        float startAlpha = startColor.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            if (time < duration)
            {
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
                uiElement.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
                yield return null; // 다음 프레임까지 대기
            }
            else
            {
                uiElement.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
            }
        }
    }
        // 볼륨 점점 크게 또는 점점 작게
        public IEnumerator FadeVolume(AudioSource audioSource, float targetVolume, float duration)
    {
        float startVolume = audioSource.volume; // 시작 볼륨
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            if (time < duration)
            {
                // Lerp를 사용하여 볼륨을 서서히 변경
                float newVolume = Mathf.Lerp(startVolume, targetVolume, time / duration);
                audioSource.volume = newVolume;
                yield return null; // 다음 프레임까지 대기
            }
            else
            {
                // 마지막에 정확히 targetVolume에 맞춤
                audioSource.volume = targetVolume;
            }
        }
    }


}