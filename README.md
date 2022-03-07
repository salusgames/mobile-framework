# Mobile Framework

This package simplifies common used packages and systems used in mobile games

## Installation
Add `https://github.com/salusgames/mobile-framework.git` to the package manager in Unity

### Unity Ads
1. Add the UnityAdsManager.cs script to the first loaded scene in the project and fill in the relevent ID's in the inspector.
2. To show an interstitial advert include `using SalusGames.MobileFramework.Advertisements.Unity;` at the top of your script and use `UnityAdsManager.ShowInterstitialAd();` to show the advert
