using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    public enum GameState
    {
        NoInput,
        Main,
        Ready,
        Game,
    }

    public GameState gameState;

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
}