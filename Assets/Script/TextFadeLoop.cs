using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextFadeLoop : MonoBehaviour
{
    public Text uiText;
    float fadeDuration = 0.4f; // 변환시간
    float stayDuration = 0.15f; // 상태를 유지 시간
    bool started = false;
    private void Start()
    {
        StartCoroutine(FadeLoop());
        started = true;
    }

    private void OnEnable()
    {
        // 게임 오브젝트가 활성화될 때 코루틴을 시작
        if (started == true)
        {
            StartCoroutine(FadeLoop());
        }
    }

    private void OnDisable()
    {
        // 게임 오브젝트가 비활성화될 때 코루틴을 중지
        StopCoroutine(FadeLoop());
    }

    private IEnumerator FadeLoop()
    {
        while (true)
        {
            // 투명해지는 애니메이션
            yield return StartCoroutine(GameManager.instance.FadeTo(uiText, 0f, fadeDuration));

            // 잠시 완전히 투명한 상태 유지
            yield return new WaitForSeconds(stayDuration);

            // 다시 불투명해지는 애니메이션
            yield return StartCoroutine(GameManager.instance.FadeTo(uiText, 1f, fadeDuration));

            // 잠시 완전히 불투명한 상태 유지
            yield return new WaitForSeconds(stayDuration);
        }
    }
}
