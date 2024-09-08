using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
        int bestScoreValue = PlayerPrefs.GetInt("BestScore", 0);
        bestScore.text = "Best: " + bestScoreValue.ToString();
    }

    public void UpdateBestScore()
    {
        int bestScoreValue = PlayerPrefs.GetInt("BestScore", 0); // 저장된값없으면 0 반환
        if (scoreValue > bestScoreValue)
        {
            PlayerPrefs.SetInt("BestScore", scoreValue);
            PlayerPrefs.Save();
        }
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



    public void AddScore()
    {
       scoreValue += 1;
       scoreText.text = scoreValue.ToString();
    }
}