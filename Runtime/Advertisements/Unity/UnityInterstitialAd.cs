using UnityEngine;
using UnityEngine.Advertisements;

namespace SalusGames.MobileFramework.Advertisements.Unity
{
    internal class UnityInterstitialAd : IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private readonly string _adUnitId;

        public UnityInterstitialAd(string androidAdUnitId, string iOsAdUnitId)
        {
			_adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? iOsAdUnitId : androidAdUnitId;
            LoadAd();
        }
        
        private void LoadAd()
        {
            Advertisement.Load(_adUnitId, this);
        }
        
        public void ShowAd()
        {
            Debug.Log("Salus Games Unity Ad Manager: Showing Ad. ID " + _adUnitId);
            Advertisement.Show(_adUnitId, this);
            Time.timeScale = 0;
        }
        
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Salus Games Unity Ad Manager: Ad loaded and ready. ID " + _adUnitId);
        }
 
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Salus Games Unity Ad Manager: Error loading Ad. ID {adUnitId} - {error.ToString()} - {message}");
            Time.timeScale = 1;
        }

		public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    	{
        	Debug.Log($"Salus Games Unity Ad Manager: Error showing Ad. ID {adUnitId}: {error.ToString()} - {message}");
        	Time.timeScale = 1;
    	}
 
    	public void OnUnityAdsShowStart(string adUnitId) { }
   	 	public void OnUnityAdsShowClick(string adUnitId) { }
    	public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) 
		{ 
			Time.timeScale = 1;
            Debug.Log("Salus Games Unity Ad Manager: Ad complete. ID " + _adUnitId);
            LoadAd();
		}
    }
}
