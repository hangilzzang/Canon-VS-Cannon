using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ReviveUI : MonoBehaviour
{
    public Slider slider;
    float decreaseSpeed = 0.24f * 5;
    public RectTransform adLogo;
    float scaleTime1 = 0.3f * 0.2f;
    float scaleTime2 = 0.4f * 0.2f;
    Vector3 maxScale = new Vector3(1.2f, 1.2f, 1f);
    Vector3 minScale = new Vector3(1f, 1f, 1f);
    public Image image;
    public Coroutine scaleCoroutine;

    void Start()
    {
        // 타임스케일 설정
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = 0.01f * Time.timeScale;

        Color imageColor = image.color;
        image.color = new Color(imageColor.r, imageColor.g, imageColor.b, 0.83f); 

        StartCoroutine(DecreaseSlider());
        scaleCoroutine  = StartCoroutine(ScaleUpAndDown());
    }

    public void StopAdIconLoop()
    {
        StopCoroutine(scaleCoroutine);
    }

    IEnumerator DecreaseSlider()
    {
        while (slider.value > 0)
        {
            // Time.unscaledDeltaTime을 사용해 타임스케일의 영향을 받지 않도록 설정
            slider.value -= decreaseSpeed * Time.deltaTime;
            yield return null;
        }

        GameManager.instance.gameState = GameManager.GameState.NoRevive; // 상태변경
        EventManager.instance.TriggerStateChanged();
    }

    IEnumerator ScaleUpAndDown()
    {
        while (true)
        {
            // 크기를 키우는 과정
            yield return StartCoroutine(ScaleOverTime(minScale, maxScale, scaleTime1));

            // 크기를 줄이는 과정
            yield return StartCoroutine(ScaleOverTime(maxScale, minScale, scaleTime2));
        }
    }

    IEnumerator ScaleOverTime(Vector3 fromScale, Vector3 toScale, float duration)
    {
        float currentTime = 0f;

        while (currentTime < duration)
        {
            // Lerp를 사용해 시간에 따라 스케일을 변화시키고, unscaledDeltaTime 사용
            adLogo.localScale = Vector3.Lerp(fromScale, toScale, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;  // 프레임마다 대기
        }
        adLogo.localScale = toScale;
    }

}
