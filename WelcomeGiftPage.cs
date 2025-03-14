using System;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeGiftPage : MonoBehaviour
{
	private void Awake()
	{
		WelcomeGiftPage.instance = this;
		base.gameObject.SetActive(false);
	}

	public void Open()
	{
		base.gameObject.SetActive(true);
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
		AdManager.instance.LoadFirstScene();
	}

	public void claimBtnClick()
	{
		CallbacksController.instance.AddCoins(AdManager.instance.WelcomeGiftReward, AdManager.RewardDescType.WelcomeGift, AdManager.RewardType.WelcomeGift);
		this.ClaimBtn.GetComponent<Button>().interactable = false;
		this.Close();
	}

	public static WelcomeGiftPage instance;

	public GameObject PopUp;

	public GameObject ClaimBtn;

	public Text Desc;
}
