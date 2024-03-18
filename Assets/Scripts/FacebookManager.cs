using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class FacebookManager : MonoBehaviour{
    public string deepLink;
    public web webv;

    void Start()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }
        
    void DeepLinkCallback(IAppLinkResult result)
    {
        if (!string.IsNullOrEmpty(result.TargetUrl))
        {
            //var s8 = result.TargetUrl.Substring("myapp://".Length);
            var s8 = result.TargetUrl;

            if (s8 != "null")
            {
                deepLink = $"idywzkjsff={s8}";
                webv.deeplink = deepLink;
                webv.StartWeb();
            }
        } else
        {
            webv.StartWeb();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            Debug.Log("Succeful to Initialize the Facebook SDK");
            FB.Mobile.FetchDeferredAppLinkData(DeepLinkCallback);
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }
    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
        Screen.orientation = ScreenOrientation.Portrait;
    }
}