using UnityEngine;

#if UNITY_ANDROID
using System.Collections;
using Google.Play.Review;
#endif

#if UNITY_IOS
using UnityEngine.iOS;
#endif

namespace SalusGames.MobileFramework.Review
{
    public class RequestReview : MonoBehaviour
    {
        #if UNITY_ANDROID
        private ReviewManager _reviewManager;
        private PlayReviewInfo _playReviewInfo;
        #endif

        public void RequestStoreReview()
        {
            #if UNITY_ANDROID
            StartCoroutine(RequestGoogleStoreReview());
            #endif
            
            #if UNITY_IOS
            Device.RequestStoreReview();
            #endif
        }

        #if UNITY_ANDROID
        private IEnumerator RequestGoogleStoreReview()
        {
            _reviewManager = new ReviewManager();
        
            var requestFlowOperation = _reviewManager.RequestReviewFlow();
            yield return requestFlowOperation;
            if (requestFlowOperation.Error != ReviewErrorCode.NoError)
            {
                // Log error. For example, using requestFlowOperation.Error.ToString().
                Debug.Log(requestFlowOperation.Error.ToString());
                yield break;
            }
            _playReviewInfo = requestFlowOperation.GetResult();

            var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
            yield return launchFlowOperation;
            _playReviewInfo = null; // Reset the object
            if (launchFlowOperation.Error != ReviewErrorCode.NoError)
            {
                // Log error. For example, using requestFlowOperation.Error.ToString().
                Debug.Log(requestFlowOperation.Error.ToString());
                yield break;
            }
        }
        #endif
    }
}