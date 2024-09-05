using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveUI : MonoBehaviour
{
    public Slider slider;
    float decreaseSpeed = 0.5f;
    public RectTransform adLogo;
    float scaleTime1 = 1f;
    float scaleTime2 = 1f;
    Vector3 maxScale = new Vector3(1.1f, 1.1f, 1f);
    Vector3 minScale = new Vector3(1f, 1f, 1f);

    void Start()
    {
        StartCoroutine(DecreaseSlider());
        StartCoroutine(ScaleUpAndDown());
    }

    IEnumerator DecreaseSlider()
    {
        while (slider.value > 0)
        {
            slider.value -= decreaseSpeed * Time.deltaTime;
            yield return null;
        }
        // 씬 재시작
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
            // Lerp를 사용해 시간에 따라 스케일을 변화
            adLogo.localScale = Vector3.Lerp(fromScale, toScale, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;  // 프레임마다 대기
        }
        adLogo.localScale = toScale;
    }
}



