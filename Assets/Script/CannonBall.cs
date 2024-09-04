using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D  other)
    {
        Destroy(gameObject);
    }

        void Start()
    {
        // 3초 후에 자기 자신을 제거하는 코루틴을 시작합니다.
        StartCoroutine(DestroyAfterTime(10.0f));
    }

    IEnumerator DestroyAfterTime(float delay)
    {
        // 지정된 시간만큼 대기합니다.
        yield return new WaitForSeconds(delay);
        
        // 대기 후 자기 자신을 제거합니다.
        Destroy(gameObject);
    }
}
