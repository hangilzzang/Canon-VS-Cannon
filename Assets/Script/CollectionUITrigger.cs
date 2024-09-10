using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionUITrigger : MonoBehaviour
{
    public Button myButton;
    public GameObject mainUI;
    public GameObject collectionUI;
    public AudioSource switchSound;

    void Start()
    {
        // 버튼 클릭 이벤트에 대한 리스너 추가
        myButton.onClick.AddListener(OnButtonClick);
    }

    // 버튼이 클릭되었을 때 실행될 함수
    void OnButtonClick()
    {
        // 메인 ui 닫기& 콜렉션 ui 열기
        mainUI.SetActive(false);
        collectionUI.SetActive(true);
        switchSound.Play();
    }

    public void CloseCollectionUI() // 닫기 버튼에 의해 실행
    {
        mainUI.SetActive(true);
        collectionUI.SetActive(false);       
        switchSound.Play();
    }
}
