using System;
using UnityEngine;

public class WebViewController : MonoBehaviour
{
	private void Awake()
	{
		WebViewController.instance = this;
	}

	private void Start()
	{
		this.SetToastObj();
	}

	public void LoadDummyUrls()
	{
		/*using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.timuz.webviewhandler.WebViewActivity"))
		{
			androidJavaClass.CallStatic("LoadDummyExitView", new object[]
			{
				AdManager.instance.ExitLink
			});
			androidJavaClass.CallStatic("LoadDummyMoreGamesView", new object[]
			{
				AdManager.instance.MgLink
			});
		}*/
	}

	public void SetToastObj()
	{
		/*using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.timuz.webviewhandler.WebViewActivity"))
		{
			androidJavaClass.CallStatic("SetToastObj", new object[0]);
		}*/
	}

	public void ShowToastMsg(string msg)
	{
		UnityEngine.Debug.Log("----------- callToShowToastMsg");
		/*using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.timuz.webviewhandler.WebViewActivity"))
		{
			androidJavaClass.CallStatic("ShowToastMessage", new object[]
			{
				msg
			});
		}*/
	}

	public void ShowMoreGames()
	{
		this.showWebView = true;
		/*using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.timuz.webviewhandler.WebViewActivity"))
		{
			androidJavaClass.CallStatic("showMoreGamesView", new object[]
			{
				AdManager.instance.MgLink,
				Application.identifier
			});
		}*/
	}

	public void BackPressedAction()
	{
		/*using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.timuz.webviewhandler.WebViewActivity"))
		{
			if (this.showWebView)
			{
				androidJavaClass.CallStatic("hideWebView", new object[0]);
				this.showWebView = false;
			}
			else if (this.showExitView)
			{
				androidJavaClass.CallStatic("hideWebView", new object[0]);
				this.showExitView = false;
			}
			else
			{
				androidJavaClass.CallStatic("showExitView", new object[]
				{
					AdManager.instance.ExitLink,
					Application.identifier
				});
				this.showExitView = true;
			}
		}*/
	}

	public void OpenLink(string str)
	{
		UnityEngine.Debug.Log("---------- OpenLink=" + str);
		Application.OpenURL("market://details?id=" + str);
	}

	public void ExitApp(string str)
	{
		UnityEngine.Debug.Log("----------ExitApp---");
		Application.Quit();
	}

	public void unityhideWebView(string str)
	{
		UnityEngine.Debug.Log("----------unityhidewebview---");
		this.showWebView = false;
		this.showExitView = false;
	}

	private AndroidJavaObject activityContext;

	[HideInInspector]
	public bool showWebView;

	[HideInInspector]
	public bool showExitView;

	public static WebViewController instance;
}
