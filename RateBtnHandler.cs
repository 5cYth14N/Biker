using System;
using UnityEngine;
using UnityEngine.UI;

public class RateBtnHandler : MonoBehaviour
{
	private void OnEnable()
	{
		RatePage.RateSuccessCallBack += this.SuccessCallBack;
	}

	private void OnDisable()
	{
		RatePage.RateSuccessCallBack -= this.SuccessCallBack;
	}

	private void Start()
	{
		if (PlayerPrefs.GetString("IsRated", "false") == "true")
		{
			base.gameObject.GetComponent<Button>().interactable = false;
		}
		else
		{
			base.gameObject.GetComponent<Button>().interactable = true;
			base.gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.RateClicked();
			});
		}
	}

	public void RateClicked()
	{
		AdManager.instance.ShowRatePopUp();
	}

	public void SuccessCallBack()
	{
		UnityEngine.Debug.Log("-------------- InAppSuccessCallBack");
		base.gameObject.GetComponent<Button>().interactable = false;
	}

	public void FailCallBack()
	{
		UnityEngine.Debug.Log("-------------- InAppFailedCallBack");
		base.gameObject.GetComponent<Button>().interactable = true;
	}
}
