using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelsInAppPage : MonoBehaviour
{
	private void Awake()
	{
		LevelsInAppPage.instance = this;
		base.gameObject.SetActive(false);
	}

	public void Open()
	{
		base.gameObject.SetActive(true);
		this.UnlockAllPop.SetActive(false);
		this.DiscountPop.SetActive(false);
		if (!this.IsShowDiscountPop)
		{
			this.UnlockAllDesc.text = "Unlock All Levels And Get More Fun!";
			this.UnlockAllPop.SetActive(true);
			this.IsShowDiscountPop = true;
		}
		else
		{
			this.DiscountPop.SetActive(true);
			this.DiscountDesc.text = "Unlock All Levels with 50% Discount";
			if (PlayerPrefs.HasKey("nonConsumableProducts_" + InAppController.instance.UnlockAllLevelsIndex))
			{
				this.OriginalPrice.text = PlayerPrefs.GetString("nonConsumableProducts_" + InAppController.instance.UnlockAllLevelsIndex, "BUY");
			}
			if (PlayerPrefs.HasKey("nonConsumableProducts_" + InAppController.instance.LevelsDiscountIndex))
			{
				this.DiscountPrice.text = PlayerPrefs.GetString("nonConsumableProducts_" + InAppController.instance.LevelsDiscountIndex, "BUY");
			}
			this.IsShowDiscountPop = false;
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
			1.5f,
			"easetype",
			iTween.EaseType.spring
		}));
	}

	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	private void OpenDiscountPop()
	{
		this.Open();
	}

	public static LevelsInAppPage instance;

	public GameObject PopUp;

	public GameObject CloseBtn;

	public Text UnlockAllDesc;

	public Text DiscountDesc;

	public Text OriginalPrice;

	public Text DiscountPrice;

	public GameObject UnlockAllPop;

	public GameObject DiscountPop;

	public bool IsShowDiscountPop;
}
