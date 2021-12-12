using UnityEngine;
using UnityEngine.Advertisements;

namespace SalusGames.MobileFramework.Advertisements.Unity
{
    internal class UnityInterstitialAd : IUnityAdsLoadListener, IUnityAdsListener
    {
        private readonly string _adUnitId;

        public UnityInterstitialAd(string androidAdUnitId, string iOsAdUnitId)
        {
            Advertisement.AddListener(this);
            
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? iOsAdUnitId : androidAdUnitId;
            LoadAd();
        }
        
        private void LoadAd()
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }
        
        public void ShowAd()
        {
            Debug.Log("Showing Ad: " + _adUnitId);
            Advertisement.Show(_adUnitId);
            Time.timeScale = 0;
            LoadAd();
        }
        
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Loaded Ad: " + _adUnitId);
        }
 
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
            Time.timeScale = 1;
        }

        public void OnUnityAdsReady(string placementId)
        {
            Debug.Log("Ad Ready: " + _adUnitId);
        }

        public void OnUnityAdsDidError(string message)
        {
            Debug.Log("Ad error:" + _adUnitId);
            Time.timeScale = 1;
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            Time.timeScale = 1;
            Debug.Log("Ad Finished" + _adUnitId);
        }
        
        public void OnUnityAdsDidStart(string placementId) { }
    }
}
