using UnityEngine;
using UnityEngine.Advertisements;
#if UNITY_IOS 
using Unity.Advertisement.IosSupport;
#endif

namespace SalusGames.MobileFramework.Advertisements.Unity
{
    public class UnityAdsManager : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] string _iosGameId;
        [SerializeField] string _androidGameId;
        [SerializeField] private string _iosInterstitialAdId;
        [SerializeField] private string _androidInterstitialAdId;
        [SerializeField] bool _testMode;
        [SerializeField] bool _enablePerPlacementMode = true;

        private static UnityAdsManager _instance;
        private static UnityInterstitialAd _unityInterstitialAd;
        private static bool _ignoreInterstitialAdCall;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            _instance = this;
            DontDestroyOnLoad(gameObject);
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

        /// <summary>
        /// Show an Interstitial Ad
        /// </summary>
        /// <param name="ignoreTime">How long to ignore successive calls in seconds</param>
        public static void ShowInterstitialAd(int ignoreTime)
        {
            if (_ignoreInterstitialAdCall)
            {
                Debug.Log("Won't show an ad as not enough time has passed since last ad was shown");
                return;
            }

            _ignoreInterstitialAdCall = true;
            _instance.Invoke(nameof(ResetIgnoreInterstitialAdCall), ignoreTime);
            _unityInterstitialAd.ShowAd();
        }

        private void ResetIgnoreInterstitialAdCall()
        {
            _ignoreInterstitialAdCall = false;
        }
    }
}