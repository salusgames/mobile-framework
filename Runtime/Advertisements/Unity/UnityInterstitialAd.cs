using UnityEngine;
using UnityEngine.Advertisements;

namespace SalusGames.MobileFramework.Advertisements.Unity
{
    internal class UnityInterstitialAd : IUnityAdsLoadListener, IUnityAdsShowListener
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

		public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    	{
        	Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        	Time.timeScale = 1;
    	}
 
    	public void OnUnityAdsShowStart(string adUnitId) { }
   	 	public void OnUnityAdsShowClick(string adUnitId) { }
    	public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) 
		{ 
			Time.timeScale = 1;
            Debug.Log("Ad Finished" + _adUnitId);
		}
    }
}
