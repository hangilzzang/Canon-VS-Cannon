using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallEnemy : MonoBehaviour
{
    public GameObject cannonballPrefab;
    Rigidbody2D rb;

    public class Trajectory
    {
        public Vector2 launchPoint;
        public float forceAmount;
        public float degree;

        public Trajectory(Vector2 launchPoint, float forceAmount, float degree)
        {
            this.launchPoint = launchPoint;
            this.forceAmount = forceAmount;
            this.degree = degree;
        }
    }

    // 10가지 발사 궤적표 정의
    List<Trajectory> trajectories = new List<Trajectory>()
    {
        new Trajectory(new Vector2(15, 4), 14f, 180f),
        new Trajectory(new Vector2(15, 3), 10f, 180f),
        new Trajectory(new Vector2(15, 2), 8f, 180f),
        new Trajectory(new Vector2(15, 1), 9f, 180f),
        new Trajectory(new Vector2(15, 0), 14f, 180f),
        new Trajectory(new Vector2(15, -1), 8f, 180f),
        new Trajectory(new Vector2(15, 1), 13f, 180f),
        new Trajectory(new Vector2(15, 4), 15f, 180f),
        new Trajectory(new Vector2(15, 3), 16f, 180f),
        new Trajectory(new Vector2(15, 2), 18f, 185f)
    };

    float newGravityScale = 0f;
    float launchInterval = 3f; // 3초마다 발사

    void Start()
    {
        InvokeRepeating("LaunchCannonball", 0f, launchInterval);
    }

    void LaunchCannonball()
    {
        // 랜덤한 발사 궤적 선택
        Trajectory selectedTrajectory = trajectories[Random.Range(0, trajectories.Count)];
        

        // 각도를 라디안으로 변환
        float angle = selectedTrajectory.degree * Mathf.Deg2Rad;
        Vector2 forceDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // 캐논볼 생성 및 발사
        GameObject cannonball = Instantiate(cannonballPrefab, selectedTrajectory.launchPoint, Quaternion.identity);
        rb = cannonball.GetComponent<Rigidbody2D>();
        rb.gravityScale = newGravityScale;
        rb.AddForce(forceDirection * selectedTrajectory.forceAmount, ForceMode2D.Impulse);
    }
}
