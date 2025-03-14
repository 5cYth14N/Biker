using System;
using UnityEngine;
using UnityEngine.UI;

public class SharePage : MonoBehaviour
{
	private void Awake()
	{
		SharePage.instance = this;
		base.gameObject.SetActive(false);
	}

	public void Open()
	{
		base.gameObject.SetActive(true);
		if (AdManager.instance.ShareDesc != string.Empty)
		{
			this.Desc.text = AdManager.instance.ShareDesc;
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
			2.5f,
			"easetype",
			iTween.EaseType.spring
		}));
	}

	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	public void ShareClick()
	{
		PlayerPrefs.SetString("IsFBShared", "true");
		AdManager.instance.FacebookShare(0);
		if (AdManager.instance.ShareCoins > 0)
		{
			AdManager.instance.StartCoroutine(AdManager.instance.AddCoinsWithDelay(2f, AdManager.instance.ShareCoins, AdManager.RewardDescType.Sharing));
		}
		this.Close();
	}

	public static SharePage instance;

	public GameObject PopUp;

	public GameObject CloseBtn;

	public Text Desc;
}
