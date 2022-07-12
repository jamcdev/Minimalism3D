using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAdDisplay : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public string myGameIdAndroid = "4835917";
    public string myGameIdIOS = "4835916";
    public string adUnitIdAndroid = "Rewarded_Android";
    public string adUnitIdIOS = "Rewarded_iOS";
    public string myAdUnitId;
    public string myAdStatus = "";
    public bool adStarted;
    public bool adCompleted;
    bool isAdInitialized;

    private bool testMode = true;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_IOS
	        Advertisement.Initialize(myGameIdIOS, testMode, this);
	        myAdUnitId = adUnitIdIOS;
#else
        Advertisement.Initialize(myGameIdAndroid, testMode, this);
        myAdUnitId = adUnitIdAndroid;
#endif
        isAdInitialized = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        Advertisement.Load(myAdUnitId, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        myAdStatus = message;
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
        if (!adStarted)
        {
            Advertisement.Show(myAdUnitId, this);
        }
    }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        myAdStatus = message;
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");

    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        myAdStatus = message;
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        adStarted = true;
        Debug.Log("Ad Started: " + adUnitId);
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
        Debug.Log("Ad Clicked: " + adUnitId);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        adCompleted = showCompletionState == UnityAdsShowCompletionState.COMPLETED;
        Debug.Log("Ad Completed: " + adUnitId);

        PlayerInfo.tokens++;
        adStarted = false;
    }

}