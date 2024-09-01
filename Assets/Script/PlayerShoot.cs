using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject cannonballPrefab;
    Rigidbody2D rb; // 대포알 리지드바디
    Vector2 forceDirection;
    Vector2 LaunchPoint = new Vector2(0f, -3f); // 로컬 좌표계의 발사 지점
    float forceAmount = 30f; // 발사할 힘의 크기
    float localDegree = 90f; // 로컬 좌표계에서의 발사 각도
    float newGravityScale = 1f;

    void Start()
    {
        // 발사 각도를 라디안으로 변환하고 방향 벡터 계산
        float angle = localDegree * Mathf.Deg2Rad;
        forceDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            // 로컬 좌표계의 발사 지점을 월드 좌표계로 변환
            // Vector2 worldLaunchPoint = transform.TransformPoint(localLaunchPoint);

            // 발사 방향도 로컬 좌표계를 기준으로 회전
            // Vector2 worldForceDirection = transform.TransformDirection(forceDirection);

            // 캐논볼 생성 및 발사
            GameObject cannonball = Instantiate(cannonballPrefab, LaunchPoint, Quaternion.identity);
            rb = cannonball.GetComponent<Rigidbody2D>();
            rb.gravityScale = newGravityScale;
            rb.AddForce(forceDirection * forceAmount, ForceMode2D.Impulse);
        }
    }
}
