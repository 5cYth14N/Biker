using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesInAppPage : MonoBehaviour
{
	private void Awake()
	{
		UpgradesInAppPage.instance = this;
		base.gameObject.SetActive(false);
	}

	public void Open()
	{
		base.gameObject.SetActive(true);
		this.UnlockAllPop.SetActive(false);
		this.DiscountPop.SetActive(false);
		if (!this.IsShowDiscountPop)
		{
			this.UnlockAllPop.SetActive(true);
			this.IsShowDiscountPop = true;
		}
		else
		{
			this.DiscountPop.SetActive(true);
			if (PlayerPrefs.HasKey("nonConsumableProducts_" + InAppController.instance.UnlockAllUpgradesIndex))
			{
				this.OriginalPrice.text = PlayerPrefs.GetString("nonConsumableProducts_" + InAppController.instance.UnlockAllUpgradesIndex, "BUY");
			}
			if (PlayerPrefs.HasKey("nonConsumableProducts_" + InAppController.instance.UpgradesDiscountIndex))
			{
				this.DiscountPrice.text = PlayerPrefs.GetString("nonConsumableProducts_" + InAppController.instance.UpgradesDiscountIndex, "BUY");
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
			"delay",
			1,
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
			2f,
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

	public static UpgradesInAppPage instance;

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
