using System;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

public class FacebookController : MonoBehaviour
{
	private void Awake()
	{
		FacebookController.instance = this;
		if (!FB.IsInitialized)
		{
			UnityEngine.Debug.Log("------- FB not initialized call Init");
			FB.Init(new InitDelegate(this.InitCallback), new HideUnityDelegate(this.OnHideUnity), null);
		}
		else
		{
			UnityEngine.Debug.Log("------- FB already initialized call activate app");
			FB.ActivateApp();
		}
	}

	private void InitCallback()
	{
		UnityEngine.Debug.Log("------ InitCallback");
		if (FB.IsInitialized)
		{
			UnityEngine.Debug.Log("------- FB initialized successfully");
			FB.ActivateApp();
		}
		else
		{
			UnityEngine.Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity(bool isGameShown)
	{
		UnityEngine.Debug.Log("OnHideUnity isGameShown=" + isGameShown);
		if (!isGameShown)
		{
		}
	}

	public void FBLogIn()
	{
		if (!FB.IsLoggedIn)
		{
			FB.LogInWithReadPermissions(this.perms, new FacebookDelegate<ILoginResult>(this.AuthCallback));
		}
		else
		{
			this.FBLogOut();
		}
	}

	public void FBLogOut()
	{
		FB.LogOut();
	}

	private void AuthCallback(ILoginResult result)
	{
		if (FB.IsLoggedIn)
		{
			UnityEngine.Debug.Log("------ FB login successfull");
			AccessToken currentAccessToken = AccessToken.CurrentAccessToken;
			UnityEngine.Debug.Log(currentAccessToken.UserId);
			foreach (string message in currentAccessToken.Permissions)
			{
				UnityEngine.Debug.Log(message);
			}
		}
		else
		{
			UnityEngine.Debug.Log("User cancelled login");
		}
	}

	public void FBShare()
	{
		FB.ShareLink(new Uri(AdManager.instance.ShareUrl), string.Empty, string.Empty, null, new FacebookDelegate<IShareResult>(this.ShareCallback));
	}

	private void ShareCallback(IShareResult result)
	{
		if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
		{
			UnityEngine.Debug.Log("ShareLink Error: " + result.Error);
		}
		else if (!string.IsNullOrEmpty(result.PostId))
		{
			UnityEngine.Debug.Log("ShareLink Error 222: " + result.PostId);
			UnityEngine.Debug.Log(result.PostId);
		}
		else
		{
			UnityEngine.Debug.Log("-------- ShareLink success!");
		}
	}

	private List<string> perms = new List<string>
	{
		"public_profile",
		"email"
	};

	public static FacebookController instance;
}
