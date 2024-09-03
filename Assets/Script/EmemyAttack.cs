using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyAttack : MonoBehaviour
{
    public GameObject cannonballPrefab;
    Rigidbody2D rb;
    public Vector2 launchPoint = new Vector2(15,-3);
    public float forceAmount = 13f;
    public float fireAngle = 45f; // 왼쪽방향 기준



void Update()
{
    if (Input.GetMouseButtonDown(1))
    {
        LaunchEnemyCannonBall();
    }
}



    void LaunchEnemyCannonBall()
    {
        float angle = (-(fireAngle - 90) + 90f) * Mathf.Deg2Rad;
        Vector2 forceDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        GameObject cannonball = Instantiate(cannonballPrefab, launchPoint, Quaternion.identity);
        rb = cannonball.GetComponent<Rigidbody2D>();
        rb.AddForce(forceDirection * forceAmount, ForceMode2D.Impulse);
    }
}
