using System;
using UnityEngine;
using UnityEngine.UI;

public class RatePage : MonoBehaviour
{
	public static event RatePage.RateSuccessCheck RateSuccessCallBack;

	private void Awake()
	{
		RatePage.instance = this;
		base.gameObject.SetActive(false);
	}

	public void Open()
	{
		base.gameObject.SetActive(true);
		for (int i = 0; i < this.FillStars.Length; i++)
		{
			this.FillStars[i].SetActive(false);
		}
		this.ThankYouBtn.SetActive(false);
		this.RateBtn.SetActive(false);
		if (AdManager.instance.RateDesc != string.Empty)
		{
			this.Desc.text = AdManager.instance.RateDesc;
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
	}

	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	public void StarClick(int StarCount)
	{
		this.RateBtn.SetActive(false);
		this.ThankYouBtn.SetActive(false);
		for (int i = 0; i < this.FillStars.Length; i++)
		{
			if (i < StarCount)
			{
				this.FillStars[i].SetActive(true);
			}
		}
		if (StarCount > 3)
		{
			this.RateBtn.SetActive(true);
		}
		else
		{
			this.ThankYouBtn.SetActive(true);
		}
	}

	public void ThankYouClick()
	{
		PlayerPrefs.SetString("IsRated", "true");
		if (RatePage.RateSuccessCallBack != null)
		{
			RatePage.RateSuccessCallBack();
		}
		this.Close();
	}

	public void RateClick()
	{
		PlayerPrefs.SetString("IsRated", "true");
		Application.OpenURL("market://details?id=" + Application.identifier);
		if (AdManager.instance.RateCoins > 0)
		{
			AdManager.instance.StartCoroutine(AdManager.instance.AddCoinsWithDelay(2f, AdManager.instance.RateCoins, AdManager.RewardDescType.Rating));
		}
		if (RatePage.RateSuccessCallBack != null)
		{
			RatePage.RateSuccessCallBack();
		}
		this.Close();
	}

	public static RatePage instance;

	public GameObject PopUp;

	public GameObject[] FillStars;

	public GameObject RateBtn;

	public GameObject ThankYouBtn;

	public Text Desc;

	public delegate void RateSuccessCheck();
}
