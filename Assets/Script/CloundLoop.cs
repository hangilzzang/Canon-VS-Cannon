using UnityEngine;

public class CloudLoop : MonoBehaviour
{
    float speed = 0.8f; // 구름이 이동하는 속도
    float resetPositionX = -37f; // 구름이 왼쪽에서 생성될 위치
    float endPositionX = 20f; // 구름이 오른쪽 끝에서 사라질 위치

    void Update()
    {
        // 구름이 오른쪽으로 이동하게 한다.
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // 구름이 화면 끝에 도달했을 때 위치를 리셋한다.
        if (transform.position.x >= endPositionX)
        {
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        // 구름을 왼쪽으로 이동시켜 화면 밖에서 다시 등장하게 만든다.
        Vector3 newPosition = transform.position;
        newPosition.x = resetPositionX;
        transform.position = newPosition;
    }
}
