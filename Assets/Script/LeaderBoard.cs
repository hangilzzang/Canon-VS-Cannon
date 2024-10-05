using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;

public class LeaderBoard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication); // 자동로그인 수신 결과 대기   
    }
    void ProcessAuthentication(SignInStatus status) 
    {
        if (status != SignInStatus.Success) 
        {
            gameObject.SetActive(false);
        } 
    }

    public void HandleButtonClick()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI_4O68cYOEAIQAg"); // 점수 리더보드임
    }
}
