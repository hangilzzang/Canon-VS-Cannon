using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "OutOfBounds")
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GameOver" && (GameManager.instance.gameState == GameManager.GameState.Game || GameManager.instance.gameState == GameManager.GameState.Reloading))
        {
            EventManager.instance.TriggerGameOverEvent();
            GameManager.instance.gameState = GameManager.GameState.GameOver;
        }
    }

        void Start()
    {
        // 3초 후에 자기 자신을 제거하는 코루틴을 시작합니다.
        StartCoroutine(DestroyAfterTime(10f));
    }

    IEnumerator DestroyAfterTime(float delay)
    {
        // 지정된 시간만큼 대기합니다.
        yield return new WaitForSeconds(delay);
        
        // 대기 후 자기 자신을 제거합니다.
        Destroy(gameObject);
    }
}
