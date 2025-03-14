using System;
using UnityEngine;

public class ExitPopUp : MonoBehaviour
{
	private void Awake()
	{
		ExitPopUp.instance = this;
		this.ExitInNoNet.SetActive(false);
	}

	public void NoClick()
	{
		this.ExitInNoNet.SetActive(false);
		this.isNativeExtiOpened = false;
	}

	public void YesClick()
	{
		Application.Quit();
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			if ((AdManager.instance.isWifi_OR_Data_Availble() || WebViewController.instance.showWebView || WebViewController.instance.showExitView) && !this.isNativeExtiOpened && AdManager.instance.ExitLink != string.Empty)
			{
				WebViewController.instance.BackPressedAction();
			}
			else if (!this.isNativeExtiOpened)
			{
				this.ExitInNoNet.SetActive(true);
				iTween.Stop(this.PopUp);
				this.PopUp.transform.localPosition = Vector3.zero;
				iTween.MoveFrom(this.PopUp, iTween.Hash(new object[]
				{
					"y",
					1000,
					"time",
					0.4f,
					"islocal",
					true,
					"easetype",
					iTween.EaseType.spring
				}));
				iTween.Stop(this.YesBtn);
				iTween.Stop(this.NoBtn);
				this.NoBtn.transform.localScale = Vector3.one;
				this.YesBtn.transform.localScale = Vector3.one;
				iTween.ScaleFrom(this.YesBtn, iTween.Hash(new object[]
				{
					"x",
					0,
					"y",
					0,
					"time",
					0.5,
					"delay",
					0.4f,
					"easetype",
					iTween.EaseType.spring
				}));
				iTween.ScaleFrom(this.NoBtn, iTween.Hash(new object[]
				{
					"x",
					0,
					"y",
					0,
					"time",
					0.5,
					"delay",
					0.5f,
					"easetype",
					iTween.EaseType.spring
				}));
				this.isNativeExtiOpened = true;
			}
			else
			{
				this.ExitInNoNet.SetActive(false);
				this.isNativeExtiOpened = false;
			}
		}
	}

	public static ExitPopUp instance;

	public GameObject ExitInNoNet;

	public GameObject PopUp;

	public GameObject YesBtn;

	public GameObject NoBtn;

	private bool isNativeExtiOpened;
}
