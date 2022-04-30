using UnityEngine;
using GoogleMobileAds.Api;


public class Initializer : MonoBehaviour
{
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        DontDestroyOnLoad(gameObject);
    }
}
