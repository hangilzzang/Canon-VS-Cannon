using UnityEngine;
using System.Collections;

public class GilGamesLogo : MonoBehaviour
{
    // 비활성화할 게임 오브젝트를 연결할 변수
    public GameObject logo;
    public AudioSource bgm;

    void Start()
    {
        // 코루틴 시작
        StartCoroutine(LogoDeactivateAfter(3f)); // 3초 후에 비활성화
    }

    // 비활성화를 수행할 코루틴
    IEnumerator LogoDeactivateAfter(float delay)
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(delay);

        // 게임 오브젝트를 비활성화
        logo?.SetActive(false);
        bgm.Play();
        GameManager.instance.gameState = GameManager.GameState.Main;
    }
}