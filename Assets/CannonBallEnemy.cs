using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallEnemy : MonoBehaviour
{
    public GameObject cannonballPrefab;
    Rigidbody2D rb;
    Vector2 forceDirection;
    Vector2 launchPoint = new Vector2(30, -3);
    float forceAmount = 5f; // 발사할 힘의 크기
    float degree = 150;
    float newGravityScale = 0.05f;
    void Start()
    {
        float angle = degree * Mathf.Deg2Rad; // 도를 라디안으로 변환
        forceDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject cannonball = Instantiate(cannonballPrefab, launchPoint, Quaternion.identity);
            rb = cannonball.GetComponent<Rigidbody2D>();
            rb.gravityScale = newGravityScale;
            rb.AddForce(forceDirection * forceAmount, ForceMode2D.Impulse);
        }
    }
}