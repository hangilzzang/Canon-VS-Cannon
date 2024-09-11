using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class IntersitialAd : MonoBehaviour
{
#if UNITY_ANDROID
        private const string _adUnitId = "ca-app-pub-5296572742029352/4043656639";
#elif UNITY_IPHONE
        private const string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        private const string _adUnitId = "unused";
#endif

    public static IntersitialAd instance;
    public InterstitialAd _interstitialAd;
    int adDisplayProbability = 3; // 전면광고 등장 확률 조절하는 변수

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

        public void LoadAd()
        {
            if (_interstitialAd != null)
            {
                return;
            }

            var adRequest = new AdRequest();

            InterstitialAd.Load(_adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    return;
                }
                if (ad == null)
                {
                    return;
                }

                _interstitialAd = ad;
            });
        }

        public void ShowAd() // 확률이 적용됨
        {
            int randomNumber = Random.Range(0, adDisplayProbability);
            if (_interstitialAd != null && _interstitialAd.CanShowAd() && randomNumber == 0)
            {
                _interstitialAd.Show();
                _interstitialAd = null;
            }
        }
}
