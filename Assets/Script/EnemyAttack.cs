using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject cannonballPrefab;
    public AudioSource EmemyCannon;
    Rigidbody2D rb;
    float launchInterval = 3f; // 3초마다 발사
    Trajectory selectedTrajectory;

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

    List<Trajectory> trajectories = new List<Trajectory>()
    {
        new Trajectory(new Vector2(15, 1), 15f, 35f),
        new Trajectory(new Vector2(15, 1), 16f, 25f),
        new Trajectory(new Vector2(15, 2), 16f, 25f),
        new Trajectory(new Vector2(15, 1), 16f, 30f),
        new Trajectory(new Vector2(15, -1), 16f, 35f),
        new Trajectory(new Vector2(15, 0), 16f, 35f),
        new Trajectory(new Vector2(15, -2), 16f, 40f),
        new Trajectory(new Vector2(15, -1), 16f, 40f),
        new Trajectory(new Vector2(15, -3), 16f, 45f),
        new Trajectory(new Vector2(15, -2), 16f, 45f),
        new Trajectory(new Vector2(15, -3), 16f, 50f),
        new Trajectory(new Vector2(15, 3), 17f, 15f),
        new Trajectory(new Vector2(15, 3), 17f, 20f),
        new Trajectory(new Vector2(15, 2), 17f, 20f),
        new Trajectory(new Vector2(15, 2), 17f, 25f),
        new Trajectory(new Vector2(15, 1), 17f, 25f),
        new Trajectory(new Vector2(15, 0), 17f, 25f),
        new Trajectory(new Vector2(15, 1), 17f, 30f),
        new Trajectory(new Vector2(15, 0), 17f, 30f),
        new Trajectory(new Vector2(15, -1), 17f, 30f),
        new Trajectory(new Vector2(15, -1), 17f, 35f),
        new Trajectory(new Vector2(15, -2), 17f, 35f),
        new Trajectory(new Vector2(15, -3), 17f, 35f),
        new Trajectory(new Vector2(15, -2), 17f, 40f),
        new Trajectory(new Vector2(15, -3), 17f, 40f),
        new Trajectory(new Vector2(15, -4), 17f, 40f),
        new Trajectory(new Vector2(15, -3), 17f, 45f),
        new Trajectory(new Vector2(15, -4), 17f, 45f),
        new Trajectory(new Vector2(15, -5), 17f, 45f),
        new Trajectory(new Vector2(15, -5), 17f, 50f),
        new Trajectory(new Vector2(15, -4), 17f, 50f),
        new Trajectory(new Vector2(15, -5), 17f, 55f),
        new Trajectory(new Vector2(15, 4), 18f, 10f),
        new Trajectory(new Vector2(15, 3), 18f, 15f),
        new Trajectory(new Vector2(15, 2), 18f, 15f),
        new Trajectory(new Vector2(15, 2), 18f, 20f),
        new Trajectory(new Vector2(15, 1), 18f, 20f),
        new Trajectory(new Vector2(15, 1), 18f, 25f),
        new Trajectory(new Vector2(15, 0), 18f, 25f),
        new Trajectory(new Vector2(15, -1), 18f, 25f),
        new Trajectory(new Vector2(15, 0), 18f, 30f),
        new Trajectory(new Vector2(15, -1), 18f, 30f),
        new Trajectory(new Vector2(15, -2), 18f, 30f),
        new Trajectory(new Vector2(15, -3), 18f, 30f),
        new Trajectory(new Vector2(15, -2), 18f, 35f),
        new Trajectory(new Vector2(15, -3), 18f, 35f),
        new Trajectory(new Vector2(15, -4), 18f, 35f),
        new Trajectory(new Vector2(15, -3), 18f, 40f),
        new Trajectory(new Vector2(15, -4), 18f, 40f),
        new Trajectory(new Vector2(15, -4), 18f, 45f),
        new Trajectory(new Vector2(15, 4), 19f, 10f),
        new Trajectory(new Vector2(15, 3), 19f, 10f),
        new Trajectory(new Vector2(15, 2), 19f, 10f),
        new Trajectory(new Vector2(15, 3), 19f, 15f),
        new Trajectory(new Vector2(15, 2), 19f, 15f),
        new Trajectory(new Vector2(15, 1), 19f, 15f),
        new Trajectory(new Vector2(15, 2), 19f, 20f),
        new Trajectory(new Vector2(15, 1), 19f, 20f),
        new Trajectory(new Vector2(15, 0), 19f, 20f),
        new Trajectory(new Vector2(15, -1), 19f, 20f),
        new Trajectory(new Vector2(15, 1), 19f, 25f),
        new Trajectory(new Vector2(15, 0), 19f, 25f),
        new Trajectory(new Vector2(15, -1), 19f, 25f),
        new Trajectory(new Vector2(15, -2), 19f, 25f),
        new Trajectory(new Vector2(15, -3), 19f, 25f),
        new Trajectory(new Vector2(15, -1), 19f, 30f),
        new Trajectory(new Vector2(15, -2), 19f, 30f),
        new Trajectory(new Vector2(15, -3), 19f, 30f),
        new Trajectory(new Vector2(15, -4), 19f, 30f),
        new Trajectory(new Vector2(15, -3), 19f, 35f),
        new Trajectory(new Vector2(15, -4), 19f, 35f),
        new Trajectory(new Vector2(15, -4), 19f, 40f),
        new Trajectory(new Vector2(15, 4), 20f, 5f),
        new Trajectory(new Vector2(15, 4), 20f, 10f),
        new Trajectory(new Vector2(15, 3), 20f, 10f),
        new Trajectory(new Vector2(15, 2), 20f, 10f),
        new Trajectory(new Vector2(15, 3), 20f, 15f),
        new Trajectory(new Vector2(15, 2), 20f, 15f),
        new Trajectory(new Vector2(15, 1), 20f, 15f),
        new Trajectory(new Vector2(15, 0), 20f, 15f),
        new Trajectory(new Vector2(15, 1), 20f, 20f),
        new Trajectory(new Vector2(15, 0), 20f, 20f),
        new Trajectory(new Vector2(15, -1), 20f, 20f),
        new Trajectory(new Vector2(15, -2), 20f, 20f),
        new Trajectory(new Vector2(15, 0), 20f, 25f),
        new Trajectory(new Vector2(15, -1), 20f, 25f),
        new Trajectory(new Vector2(15, -2), 20f, 25f),
        new Trajectory(new Vector2(15, -3), 20f, 25f),
        new Trajectory(new Vector2(15, -4), 20f, 25f),
        new Trajectory(new Vector2(15, -2), 20f, 30f),
        new Trajectory(new Vector2(15, -3), 20f, 30f),
        new Trajectory(new Vector2(15, -4), 20f, 30f),
        new Trajectory(new Vector2(15, -4), 20f, 35f),
        new Trajectory(new Vector2(15, 4), 21f, 5f),
        new Trajectory(new Vector2(15, 3), 21f, 5f),
        new Trajectory(new Vector2(15, 4), 21f, 10f),
        new Trajectory(new Vector2(15, 3), 21f, 10f),
        new Trajectory(new Vector2(15, 2), 21f, 10f),
        new Trajectory(new Vector2(15, 1), 21f, 10f),
        new Trajectory(new Vector2(15, 3), 21f, 15f),
        new Trajectory(new Vector2(15, 2), 21f, 15f),
        new Trajectory(new Vector2(15, 1), 21f, 15f),
        new Trajectory(new Vector2(15, 0), 21f, 15f),
        new Trajectory(new Vector2(15, -1), 21f, 15f),
        new Trajectory(new Vector2(15, 1), 21f, 20f),
        new Trajectory(new Vector2(15, 0), 21f, 20f),
        new Trajectory(new Vector2(15, -1), 21f, 20f),
        new Trajectory(new Vector2(15, -2), 21f, 20f),
        new Trajectory(new Vector2(15, -1), 21f, 25f),
        new Trajectory(new Vector2(15, -2), 21f, 25f),
        new Trajectory(new Vector2(15, -3), 21f, 25f),
        new Trajectory(new Vector2(15, -3), 21f, 30f),
        new Trajectory(new Vector2(15, -4), 21f, 30f),
    };


    void Start()   // 이벤트 등록
    {   
        EventManager.instance.StateChangeEvent += EnemyAttackStart;
        EventManager.instance.GameOverEvent += EnemyAttackStop;
    }
    void OnDisable() // 이벤트 해제
    {
        EventManager.instance.StateChangeEvent -= EnemyAttackStart;
        EventManager.instance.GameOverEvent -= EnemyAttackStop;
    }


    void EnemyAttackStop()
    {
        CancelInvoke("LaunchCannonball");
    }

    void EnemyAttackStart()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Game)
        {
            InvokeRepeating("LaunchCannonball", 0.3f, launchInterval);
        }
    }

    void LaunchCannonball()
    {
        if (GameManager.instance.scoreValue < 10)
        {
            selectedTrajectory = trajectories[Random.Range(0, 11)];
        }
        else if (GameManager.instance.scoreValue < 20)
        {
            selectedTrajectory = trajectories[Random.Range(0, 32)];
        }
        else if (GameManager.instance.scoreValue < 30)
        {
            selectedTrajectory = trajectories[Random.Range(0, 50)];
        }
        else if (GameManager.instance.scoreValue < 40)
        {
            selectedTrajectory = trajectories[Random.Range(0, 72)];
        }
        else if (GameManager.instance.scoreValue < 50)
        {
            selectedTrajectory = trajectories[Random.Range(0, 93)];
        }
        else
        {
            selectedTrajectory = trajectories[Random.Range(0, trajectories.Count)];
        }
        
        // selectedTrajectory = trajectories[Random.Range(0, trajectories.Count)];
        // 각도를 라디안으로 변환
        float angle = (-(selectedTrajectory.degree - 90f) + 90f) * Mathf.Deg2Rad;
        Vector2 forceDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // 캐논볼 생성 및 발사
        GameObject cannonball = Instantiate(cannonballPrefab, selectedTrajectory.launchPoint, Quaternion.identity);
        rb = cannonball.GetComponent<Rigidbody2D>();
        rb.AddForce(forceDirection * selectedTrajectory.forceAmount, ForceMode2D.Impulse);
        EmemyCannon.Play();
    }
}
