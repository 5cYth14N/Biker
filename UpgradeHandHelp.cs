using System;
using UnityEngine;

public class UpgradeHandHelp : MonoBehaviour
{
	private void Start()
	{
		this.handImg = this.handHelpObj.transform.GetChild(0).transform.gameObject;
		this.handHelpObj.SetActive(false);
		if (!PlayerPrefs.HasKey("handhelp"))
		{
			this.handHelpObj.gameObject.SetActive(true);
			PlayerPrefs.SetInt("handhelp", 1);
			iTween.ScaleTo(this.handImg.gameObject, iTween.Hash(new object[]
			{
				"x",
				0.8,
				"y",
				0.8,
				"time",
				0.5f,
				"looptype",
				iTween.LoopType.pingPong,
				"easetype",
				iTween.EaseType.linear
			}));
			base.Invoke("HideHand", 5f);
		}
		else
		{
			this.handHelpObj.gameObject.SetActive(false);
		}
	}

	private void HideHand()
	{
		this.handHelpObj.gameObject.SetActive(false);
	}

	public GameObject handHelpObj;

	private GameObject handImg;
}
