using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;


public class RewardAdRevive : MonoBehaviour
{
#if UNITY_ANDROID
        private const string _adUnitId = "ca-app-pub-5296572742029352/9986248450";
#elif UNITY_IPHONE
        private const string _adUnitId = "unused";
#else
        private const string _adUnitId = "unused";
#endif

    public static RewardAdRevive instance;
    public RewardedAd _rewardedAd;

    void Awake()
    {
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

        public void LoadAd() // 매 겜 시작할때마다 로드함
        {
            if (_rewardedAd != null) // 이미 광고 로드되어있으면 다시 로드 안함
            {
                return;
            }
            var adRequest = new AdRequest();

            RewardedAd.Load(_adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    return;
                }

                if (ad == null)
                {
                    return;
                }
                _rewardedAd = ad;
            });
        }


        public void ShowAd()
        {
            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardedAd.Show((Reward reward) => 
                {
                   EventManager.instance.TriggerWatchedRewardAdEvent();
                });

                _rewardedAd = null; // 광고 봤으니까 리셋 ㄱㄱ
            }
        }
}
