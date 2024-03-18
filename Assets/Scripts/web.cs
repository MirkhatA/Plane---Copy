using System;
using UnityEngine;
using UnityEngine.SceneManagement;

interface IPlugin
{
    void OnResult(string recognizedResult);
}

public class web : MonoBehaviour 
{
    private string googleUrl;
    private string url = "aHR0cHM6Ly9hdmlhcGxhbmUuZnVuL2F2aWFzLnBocD8=";

    public string deeplink;

    public string isWebOn;

    private void Start()
    {

    }

    // write base64 decoder method of url
    public string DecodeBase64String(string base64String)
    {
        byte[] data = Convert.FromBase64String(base64String);
        return System.Text.Encoding.UTF8.GetString(data);
    }

    public void StartWeb()
    {
        InAppBrowser.DisplayOptions options = new InAppBrowser.DisplayOptions();
        options.displayURLAsPageTitle = false;
        options.hidesTopBar = true;
        options.hidesHistoryButtons = true;
        options.shouldStickToPortrait = false;
        options.shouldStickToPortrait = false;
        options.shouldStickToPortrait = false;
        options.insets = new InAppBrowser.EdgeInsets { bottom = 0, left = 0 };

        string newUrl = DecodeBase64String(url);
        newUrl += deeplink;

        //GUIUtility.systemCopyBuffer = "url:  " + newUrl;
        //  InAppBrowser.SetUserAgent("Mozilla/5.0 (Linux; Android 10; SM-A920F Build/QP1A.190711.020) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.6261.105 Mobile Safari/537.36");
        InAppBrowser.OpenURL(newUrl, options);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
