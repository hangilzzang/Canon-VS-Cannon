using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    public enum GameState
    {
        Reloading,
        Main,
        Ready,
        Game,
    }

    public GameState gameState;
    public Text scoreText;
    public int scoreValue = 0;

    void Awake() 
    {
        Application.targetFrameRate = 60; // fps 60으로 설정


        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameState = GameState.Main;
    }

    public void AddScore()
    {
       scoreValue += 1;
       scoreText.text = scoreValue.ToString();
    }
}