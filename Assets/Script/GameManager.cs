using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    public enum GameState
    {
        Reloading,
        Main,
        Ready,
        Game,
        GameOver,
        NoRevive,
    }

    public GameState gameState;
    public Text scoreText;
    public int scoreValue = 0;
    public GameObject reviveUI;
    public AudioSource bgm;
    public Image image;

    void Start()
    {
        EventManager.instance.StateChangeEvent += NoRevive_;
    }

    void OnDisable()
    {
        // 이벤트 해지
        EventManager.instance.StateChangeEvent -= NoRevive_;
    }

    void Awake() 
    {
        gameState = GameState.Main;
        
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.01f;
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

    public void AddScore()
    {
       scoreValue += 1;
       scoreText.text = scoreValue.ToString();
    }

    void NoRevive_()
    {
        if (GameManager.instance.gameState == GameManager.GameState.NoRevive)
        {
            StartCoroutine(NoRevive());
        }
    }
    
    
    IEnumerator NoRevive()
    {
        // reviveUI 끄기
        reviveUI.SetActive(false);
        
        // 시간 정상화
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.01f;
        yield return null;
        

        // 컬러 정상화
        Color imageColor = image.color;
        image.color = new Color(imageColor.r, imageColor.g, imageColor.b, 0f); 
        
        // bgm 정상화
        bgm.volume = 1f;
        
        yield return new WaitForSeconds(2); 



        //점수 내려갔다 올라가기
        // 화면 꺼매지기
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}