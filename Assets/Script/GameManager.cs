using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    public enum GameState
    {
        MainScene
    }

    public GameState gameState;

    void Awake() 
    {
        // PlayerPrefs.DeleteAll();
        // PlayerPrefs.Save(); // 변경사항 저장
        Application.targetFrameRate = 60; // fps 60으로 설정


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }
}