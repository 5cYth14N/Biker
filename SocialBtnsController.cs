using System;
using UnityEngine;
using UnityEngine.UI;

public class SocialBtnsController : MonoBehaviour
{
	private void Awake()
	{
		SocialBtnsController.instance = this;
	}

	private void OnEnable()
	{
		this.Open();
	}

	public void Open()
	{
		base.gameObject.SetActive(true);
		this.FBCoinsTxt.text = this.FBRewardCoins + string.Empty;
		this.YoutubeCoinsTxt.text = this.YoutubeRewardCoins + string.Empty;
		this.TwitterCoinsTxt.text = this.TwitterRewardCoins + string.Empty;
		this.InstagramCoinsTxt.text = this.InstagramRewardCoins + string.Empty;
		this.BtnType = PlayerPrefs.GetInt("ScocialBtnType", 0);
		this.hideAllBtns();
		this.GetBtnDisplay();
	}

	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	private void hideAllBtns()
	{
		this.FacebookBtn.SetActive(false);
		this.YoutubeBtn.SetActive(false);
		this.TwitterBtn.SetActive(false);
		this.InstagramBtn.SetActive(false);
	}

	public void GetBtnDisplay()
	{
		this.hideAllBtns();
		this.IsFoundPossibility = false;
		if (PlayerPrefs.GetString("FacebookUsed", "false") == "false" || PlayerPrefs.GetString("YoutubeUsed", "false") == "false" || PlayerPrefs.GetString("TwitterUsed", "false") == "false" || PlayerPrefs.GetString("InstagramUsed", "false") == "false")
		{
			this.BtnType = PlayerPrefs.GetInt("ScocialBtnType", 0);
			switch (this.BtnType)
			{
			case 0:
				if (PlayerPrefs.GetString("FacebookUsed", "false") == "false")
				{
					this.FacebookBtn.SetActive(true);
					this.IsFoundPossibility = true;
				}
				else
				{
					this.IsFoundPossibility = false;
				}
				break;
			case 1:
				if (PlayerPrefs.GetString("YoutubeUsed", "false") == "false")
				{
					this.YoutubeBtn.SetActive(true);
					this.IsFoundPossibility = true;
				}
				else
				{
					this.IsFoundPossibility = false;
				}
				break;
			case 2:
				if (PlayerPrefs.GetString("TwitterUsed", "false") == "false")
				{
					this.TwitterBtn.SetActive(true);
					this.IsFoundPossibility = true;
				}
				else
				{
					this.IsFoundPossibility = false;
				}
				break;
			case 3:
				if (PlayerPrefs.GetString("InstagramUsed", "false") == "false")
				{
					this.InstagramBtn.SetActive(true);
					this.IsFoundPossibility = true;
				}
				else
				{
					this.IsFoundPossibility = false;
				}
				break;
			}
			if (this.IsFoundPossibility)
			{
				this.BtnType++;
				PlayerPrefs.SetInt("ScocialBtnType", this.BtnType);
			}
			else
			{
				this.BtnType++;
				this.BtnType = ((this.BtnType <= 3) ? this.BtnType : 0);
				PlayerPrefs.SetInt("ScocialBtnType", this.BtnType);
				this.GetBtnDisplay();
			}
		}
	}

	public void FacebookBtnClick()
	{
		AdManager.instance.FBLike(this.FBRewardCoins);
		this.GetBtnDisplay();
	}

	public void YoutubeBtnClick()
	{
		AdManager.instance.YoutubeSubscribe(this.YoutubeRewardCoins);
		this.GetBtnDisplay();
	}

	public void TwitterBtnClick()
	{
		AdManager.instance.TwitterFollow(this.TwitterRewardCoins);
		this.GetBtnDisplay();
	}

	public void InstagramBtnClick()
	{
		AdManager.instance.InstagramFollow(this.InstagramRewardCoins);
		this.GetBtnDisplay();
	}

	public GameObject FacebookBtn;

	public GameObject YoutubeBtn;

	public GameObject TwitterBtn;

	public GameObject InstagramBtn;

	public Text FBCoinsTxt;

	public Text YoutubeCoinsTxt;

	public Text TwitterCoinsTxt;

	public Text InstagramCoinsTxt;

	public int FBRewardCoins;

	public int YoutubeRewardCoins;

	public int TwitterRewardCoins;

	public int InstagramRewardCoins;

	public static SocialBtnsController instance;

	private int BtnType;

	private int currentBtn;

	private bool IsFoundPossibility;
}
