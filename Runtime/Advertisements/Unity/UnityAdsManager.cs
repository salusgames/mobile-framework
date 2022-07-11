using UnityEngine;
using UnityEngine.Advertisements;
using SalusGames.MobileFramework.Advertisements.Unity.Config;
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

        /// <summary>
        /// Disable the showing of ads
        /// </summary>
        public static bool AdsDisabled
        {
            set
            {
                ConfigController.DisableAds = value;
				Disabled(value);
            }
            get => ConfigController.DisableAds;
        }

		private void Disabled(bool disabled)
		{
			if(disabled) Debug.Log("Salus Games Unity Ad Manager: Disabling UnityAdsManager, ads won't be shown");
			else
			{
 				Debug.Log("Salus Games Unity Ad Manager: Enabling UnityAdsManager, ads will be shown");
				InitializeAds();
            
            	if(_delayInitialAd > 0) StartInterstitialAdWaitTimer(_delayInitialAd);
			}
		}
        
        private void Start()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            _instance = this;
            DontDestroyOnLoad(gameObject);
            AskForTrackingIos();

            ActiveState(AdsDisabled);
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
            Debug.Log("Salus Games Unity Ad Manager: Unity Ads initialization complete.");
            
            _unityInterstitialAd = new UnityInterstitialAd(_androidInterstitialAdId,_iosInterstitialAdId);
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Salus Games Unity Ad Manager: Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }

        /// <summary>
        /// Show an Interstitial Ad
        /// </summary>
        /// <param name="waitTime">How long to wait before another ad can be shown</param>
        public static void ShowInterstitialAd(int waitTime)
        {
            if (AdsDisabled)
            {
                Debug.Log("Salus Games Unity Ad Manager: Not showing ads as this has been disabled");
                return;
            }
            
            if (_ignoreInterstitialAdCall)
            {
                Debug.Log("Salus Games Unity Ad Manager: Won't show an Ad as not enough time has passed");
                return;
            }
            
            StartInterstitialAdWaitTimer(waitTime);
            _unityInterstitialAd.ShowAd();
        }
        
        private static void StartInterstitialAdWaitTimer(float time)
        {
            _ignoreInterstitialAdCall = true;
            Debug.Log("Salus Games Unity Ad Manager: Won't show Ad for " + time + " seconds");
            _instance.Invoke(nameof(ResetIgnoreInterstitialAdCall), time);
        }

        private void ResetIgnoreInterstitialAdCall()
        {
            _ignoreInterstitialAdCall = false;
            Debug.Log("Salus Games Unity Ad Manager: Will now show Ads again");
        }
    }
}