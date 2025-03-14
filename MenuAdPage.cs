using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuAdPage : MonoBehaviour
{
	private void Awake()
	{
		MenuAdPage.instance = this;
		base.gameObject.SetActive(false);
	}

	public void Open()
	{
		base.Invoke("showMenuAd", AdManager.instance.menuAdDelay);
	}

	private void showMenuAd()
	{
		base.gameObject.SetActive(true);
		this.MenuAdImg.CrossFadeAlpha(1f, 1f, true);
		this.CloseBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom(this.CloseBtn, iTween.Hash(new object[]
		{
			"x",
			0,
			"y",
			0,
			"time",
			0.4f,
			"delay",
			1f,
			"easetype",
			iTween.EaseType.spring
		}));
	}

	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	public void MenuAdClick()
	{
		UnityEngine.Debug.Log("MenuAdLink=" + AdManager.instance.MenuAdLinkTo);
		Application.OpenURL(AdManager.instance.MenuAdLinkTo);
		this.Close();
	}

	public void SetMenuAdTexture()
	{
		this.MenuAdImg.GetComponent<Image>().sprite = AdManager.instance.menuAdImg;
	}

	public Image MenuAdImg;

	public GameObject CloseBtn;

	public static MenuAdPage instance;
}
