# Mobile Framework

This package simplifies common used packages and systems used in mobile games

- [Installation](https://github.com/salusgames/mobile-framework#installation)
- [Unity Ads](https://github.com/salusgames/mobile-framework#unity-ads)
  - [Setup](https://github.com/salusgames/mobile-framework#setup)
  - [Showing an intersistial ad](https://github.com/salusgames/mobile-framework#showing-an-intersistial-ad)
  - [Disabling/Enabling Ads](https://github.com/salusgames/mobile-framework#disablingenabling-ads)
- [Store Reviews](https://github.com/salusgames/mobile-framework#store-reviews)

# Installation
To install simply add `https://github.com/salusgames/mobile-framework.git` to the package manager in Unity

## Unity Ads
> Note: This package writes a json file to enable or disable ads in the Application.persistentDataPath folder on the user device. User's with technical know how might be able to disable ads by editing this json file. This doesn't bother me as power to them for the effort but may bother you if you use this package.

### Setup
Add the UnityAdsManager.cs script to a gameobject in the first loaded scene in the project and fill in the relevent ID's in the inspector.

### Showing an intersistial ad
To show an interstitial advert include `using SalusGames.MobileFramework.Advertisements.Unity;` at the top of your script and use `UnityAdsManager.ShowInterstitialAd(float waitTime);` to show the advert and wait for the duration of `waitTime` before showing another interstitial advert 

### Disabling/Enabling Ads
You can disable & enable the showing of ads by changing the `AdsDisabled` property.

## Store Reviews
Include `using SalusGames.MobileFramework.Review;` at the top of your script and use `StoreReview.Request();` to prompt for a review.
