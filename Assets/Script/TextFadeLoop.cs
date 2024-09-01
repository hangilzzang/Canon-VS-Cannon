using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextFadeLoop : MonoBehaviour
{
    public Text uiText;
    float fadeDuration = 0.4f; // 변환시간
    float stayDuration = 0.15f; // 상태를 유지 시간

    private void Start()
    {
        StartCoroutine(FadeLoop());
    }

    private IEnumerator FadeLoop()
    {
        while (true)
        {
            // 투명해지는 애니메이션
            yield return StartCoroutine(FadeTo(0f, fadeDuration));

            // 잠시 완전히 투명한 상태 유지
            yield return new WaitForSeconds(stayDuration);

            // 다시 불투명해지는 애니메이션
            yield return StartCoroutine(FadeTo(1f, fadeDuration));

            // 잠시 완전히 불투명한 상태 유지
            yield return new WaitForSeconds(stayDuration);
        }
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        Color startColor = uiText.color;
        float startAlpha = startColor.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            uiText.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return null; // 다음 프레임까지 대기
        }

        uiText.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
    }
}
