using UnityEngine;
using UnityEngine.Advertisements;
#if UNITY_IOS 
using Unity.Advertisement.IosSupport;
#endif

namespace SalusGames.MobileFramework.Advertisements.Unity
{
    public class UnityAdsManager : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] string _androidGameId;
        [SerializeField] string _iosGameId;
        [SerializeField] bool _testMode;
        [SerializeField] bool _enablePerPlacementMode = true;
        [SerializeField] private string _iosInterstitialAdId;
        [SerializeField] private string _androidInterstitialAdId;

        private static UnityInterstitialAd _unityInterstitialAd;
       
        private void Awake()
        {
            AskForTrackingIos();
            InitializeAds();
        }

        private void AskForTrackingIos()
        {
            #if UNITY_IOS 
            if(ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking();
            }
            #endif
        }
        
        private void InitializeAds()
        {
            var gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosGameId : _androidGameId;
            Advertisement.Initialize(gameId, _testMode, _enablePerPlacementMode, this);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
            
            _unityInterstitialAd = new UnityInterstitialAd(_androidInterstitialAdId,_iosInterstitialAdId);
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }

        public static void ShowInterstitialAd()
        {
            _unityInterstitialAd.ShowAd();
        }
    }
}
