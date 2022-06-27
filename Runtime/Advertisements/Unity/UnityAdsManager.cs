using UnityEngine;
using UnityEngine.Advertisements;
#if UNITY_IOS 
using Unity.Advertisement.IosSupport;
#endif

namespace SalusGames.MobileFramework.Advertisements.Unity
{
    public class UnityAdsManager : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] private string _iosGameId;
        [SerializeField] private string _androidGameId;
        [SerializeField] private string _iosInterstitialAdId;
        [SerializeField] private string _androidInterstitialAdId;
        [SerializeField] private bool _testMode;
        [SerializeField] private float _delayInitialAd;

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
            
            StartInterstitialAdWaitTimer(_delayInitialAd);
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
            Advertisement.Initialize(gameId, _testMode, this);
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
        /// <param name="waitTime">How long to wait before another ad can be shown</param>
        public static void ShowInterstitialAd(int waitTime)
        {
            if (_ignoreInterstitialAdCall)
            {
                Debug.Log("Won't show an Ad as not enough time has passed");
                return;
            }
            
            StartInterstitialAdWaitTimer(waitTime);
            _unityInterstitialAd.ShowAd();
        }

        private static void StartInterstitialAdWaitTimer(float time)
        {
            _ignoreInterstitialAdCall = true;
            Debug.Log("Won't show Ad for " + time + " seconds");
            _instance.Invoke(nameof(ResetIgnoreInterstitialAdCall), time);
        }

        private void ResetIgnoreInterstitialAdCall()
        {
            _ignoreInterstitialAdCall = false;
            Debug.Log("Will now show Ads again");
        }
    }
}