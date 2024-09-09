using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionUI : MonoBehaviour
{
    // 각 볼의 버튼
    public Button ball2;
    public Button ball3;
    public Button ball4;
    public Button ball5;
    public Button ball6;
    
    // 각 볼 이미지(셀렉 이미지 x)
    public Image ballImage2;
    public Image ballImage3;
    public Image ballImage4;
    public Image ballImage5;
    public Image ballImage6;
    
    // 각 볼의 스프라이트
    public Sprite bawlingBall;
    public Sprite tennisBall;
    public Sprite basketBall;
    public Sprite baseBall;
    public Sprite waterMelon;    

    // 각 볼의 셀렉이미지
    public Image selectImage1;
    public Image selectImage2;
    public Image selectImage3;
    public Image selectImage4;
    public Image selectImage5;
    public Image selectImage6;
    List<Image> selectImages;

    // 언락조건 텍스트
    public GameObject unlockText2;
    public GameObject unlockText3;
    public GameObject unlockText4;
    public GameObject unlockText5;
    public GameObject unlockText6;

    

    // 사용가능한 공들 사용 활성화(기본공은 기본적으로 활성화 되어있기때문에 제외)
    // 이전에 셀렉된 이미지만 활성화
    void Start()
    {
        int BestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (BestScore >= 20)
        {
            AvailableBall(ball2, ballImage2, bawlingBall, unlockText2);
        }
        if (BestScore >= 40)
        {
            AvailableBall(ball3, ballImage3, tennisBall, unlockText3);
        }
        if (BestScore >= 60)
        {
            AvailableBall(ball4, ballImage4, basketBall, unlockText4);
        }
        if (BestScore >= 80)
        {
            AvailableBall(ball5, ballImage5, baseBall, unlockText5);
        }
        if (BestScore >= 100)
        {
            AvailableBall(ball6, ballImage6, waterMelon, unlockText6);
        }

        int selectedBall = PlayerPrefs.GetInt("Selected", 0);

        selectImages = new List<Image>
        {
            selectImage1,
            selectImage2,
            selectImage3,
            selectImage4,
            selectImage5,
            selectImage6
        };

        selectImages[selectedBall].enabled = true; // 이전에 셀렉된 볼 선택됨 표시
    }

    void AvailableBall(Button ball, Image ballImage, Sprite newImage, GameObject text) // 사용가능한 공이면 활성화
    {
        // 버튼 활성화
        ball.interactable = true;
        // 이미지 교체
        ballImage.sprite = newImage;
        // 언락텍스트 비활성화
        text.SetActive(false);
    }
}


