using System;
using UnityEngine;
using UnityEngine.UI;

public class CoinsAddedPopUp : MonoBehaviour
{
	private void Awake()
	{
		CoinsAddedPopUp.instance = this;
		base.gameObject.SetActive(false);
	}

	public void Open(int Coins, AdManager.RewardDescType rewardType)
	{
		base.gameObject.SetActive(true);
		switch (rewardType)
		{
		case AdManager.RewardDescType.WatchVideo:
			this.Desc.text = "Thank you for Watching!";
			break;
		case AdManager.RewardDescType.Sharing:
			this.Desc.text = "Thank you for Sharing!";
			break;
		case AdManager.RewardDescType.Rating:
			this.Desc.text = "Thank you for Rating!";
			break;
		case AdManager.RewardDescType.Notification:
			this.Desc.text = string.Empty;
			break;
		case AdManager.RewardDescType.Other:
			this.Desc.text = string.Empty;
			break;
		}
		this.RewardDesc.text = "Your reward " + Coins + " Coins";
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
		iTween.Stop(this.CloseBtn);
		this.CloseBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom(this.CloseBtn, iTween.Hash(new object[]
		{
			"x",
			0,
			"y",
			0,
			"time",
			0.5,
			"delay",
			2.5f,
			"easetype",
			iTween.EaseType.spring
		}));
	}

	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	public void WatchVideoClick()
	{
		AdManager.instance.ShowRewardVideoWithCallback(delegate(bool result)
		{
			if (result)
			{
				CallbacksController.instance.AddCoins(AdManager.instance.RewardToWatchAnotherVideo, AdManager.RewardDescType.WatchVideo, AdManager.RewardType.WatchVideoAgain);
				AdManager.instance.ShowToast(AdManager.instance.RewardToWatchAnotherVideo + " coins added successfully");
				this.Close();
				AdManager.instance.ShowToast(AdManager.instance.RewardToWatchAnotherVideo + " Coins added successfully");
			}
		});
	}

	public static CoinsAddedPopUp instance;

	public GameObject PopUp;

	public GameObject CloseBtn;

	public Text Desc;

	public Text RewardDesc;
}
