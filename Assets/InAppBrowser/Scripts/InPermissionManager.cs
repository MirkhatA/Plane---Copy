using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class InPermissionManager : MonoBehaviour
{
    private object callbacks;
    public TMP_Text load;

    public void InPermissionManager_CheckPermission(string permissionName)
    {
        bool result = Permission.HasUserAuthorizedPermission("android.permission." + permissionName);

        if (!result)
        {
            var callbacks = new PermissionCallbacks();
            callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
            callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
            callbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;

            Permission.RequestUserPermission("android.permission." + permissionName, callbacks);
            result = Permission.HasUserAuthorizedPermission("android.permission." + permissionName);
        }
        else GetInAppBrowserHelper().CallStatic("djfgf", true);
    }

    public void InPermissionManager_OpenGame(string permissionName)
    {
        StartCoroutine(OpenGameAfterDelay(permissionName));
    }

    private IEnumerator OpenGameAfterDelay(string permissionName)
    {
        yield return new WaitForSeconds(5); // 5-second delay

        InAppBrowser.CloseBrowser();
        SceneManager.LoadScene("MainMenu");
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void GetGaidCallBack(string gaid)
    {

    }

    private static AndroidJavaObject GetInAppBrowserHelper()
    {
        var helper = new AndroidJavaClass("inappbrowser.kokosoft.com.InAppBrowserDialogFragment");
        return helper;
    }

    internal void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
    {
        System.Console.WriteLine("gfdgf #0");
        GetInAppBrowserHelper().CallStatic("djfgf", false);
    }

    internal void PermissionCallbacks_PermissionGranted(string permissionName)
    {
        System.Console.WriteLine("gfdgf #1");
        GetInAppBrowserHelper().CallStatic("djfgf", true);
    }

    internal void PermissionCallbacks_PermissionDenied(string permissionName)
    {
        System.Console.WriteLine("gfdgf #2");
        GetInAppBrowserHelper().CallStatic("djfgf", false);
    }
}