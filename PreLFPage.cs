using System;
using UnityEngine;
using UnityEngine.UI;

public class PreLFPage : MonoBehaviour
{
	private void Awake()
	{
		PreLFPage.instance = this;
		base.gameObject.SetActive(false);
	}

	public void Open()
	{
		AdManager.instance.RunActions(AdManager.PageType.PreLF, 1, 0);
		base.gameObject.SetActive(true);
		this.ContinueBtn.SetActive(false);
		this.ResumeAlwaysBtn.SetActive(false);
		this.WatchToContinueBtn.SetActive(false);
		string @string = PlayerPrefs.GetString("ResumeAlwaysPurchased", "false");
		if (@string == "true")
		{
			this.Desc.text = "Click To Continue To Resume.";
			this.ContinueBtn.SetActive(true);
		}
		else
		{
			this.Desc.text = "You Crashed";
			this.ResumeAlwaysBtn.SetActive(true);
			this.WatchToContinueBtn.SetActive(true);
		}
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
		iTween.ScaleFrom(this.CloseBtn, iTween.Hash(new object[]
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
	}

	public void Close()
	{
		base.gameObject.SetActive(false);
		LevelFailSample.instance.Open();
	}

	public void WatchVideoBtnClick()
	{
		base.gameObject.SetActive(false);
	}

	public void ContinueBtnClick()
	{
		base.gameObject.SetActive(false);
	}

	public static PreLFPage instance;

	public GameObject PopUp;

	public GameObject CloseBtn;

	public Text Desc;

	public GameObject WatchToContinueBtn;

	public GameObject ResumeAlwaysBtn;

	public GameObject ContinueBtn;
}
