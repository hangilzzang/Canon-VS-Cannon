using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionBallSellction : MonoBehaviour
{
    public Image ball1;
    public Image ball2;
    public Image ball3;
    public Image ball4;
    public Image ball5;
    public Image ball6;




    Button myButton; // 선택버튼
    Image myImage; // 선택효과
    public int ballNum;
    public AudioSource stoneSound;

    
    
    void Start()
    {
        myImage = GetComponent<Image>();
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnButtonClick);
    }

    // 버튼이 클릭되었을 때 실행될 함수
    void OnButtonClick()
    {
        stoneSound.Play();
        // 모든 셀렉효과를 끈다
        ball1.enabled = false;
        ball2.enabled = false;
        ball3.enabled = false;
        ball4.enabled = false;
        ball5.enabled = false;
        ball6.enabled = false;

        // 내 이미지의 셀렉효과만 킨다
        myImage.enabled = true;

        // 선택사항 저장
        PlayerPrefs.SetInt("Selected", ballNum);
        PlayerPrefs.Save();
    }
}
