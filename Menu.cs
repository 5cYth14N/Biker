using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	private void Awake()
	{
		Menu._instance = this;
		if (!PlayerPrefs.HasKey("entername"))
		{
			UnityEngine.Debug.Log(" showing asp");
			this.avatarSelectionPage.SetActive(true);
		}
		else
		{
			UnityEngine.Debug.Log(" Not showing asp");
			this.avatarSelectionPage.SetActive(false);
		}
		base.Invoke("TweenBikes", 1f);
	}

	private void TweenBikes()
	{
		for (int i = 0; i < this.bikes.Length; i++)
		{
			iTween.MoveBy(this.bikes[i].gameObject, iTween.Hash(new object[]
			{
				"y",
				3,
				"time",
				2,
				"looptype",
				iTween.LoopType.pingPong,
				"easetype",
				iTween.EaseType.linear
			}));
		}
	}

	private void OnEnable()
	{
		if (PageHandler._instance)
		{
			PageHandler._instance.pageType = PageHandler.pageTypeEnum.menu;
		}
		this.UpdateCoinText();
		if (AdManager.instance)
		{
			AdManager.instance.RunActions(AdManager.PageType.Menu, 1, 0);
		}
	}

	private void Start()
	{
		GameManager.cameFromMenu = true;
		UnityEngine.Debug.Log("Came From menu???" + GameManager.cameFromMenu);
	}

	public void UpdateCoinText()
	{
		this.totalCoinsText.text = PlayerPrefs.GetInt(GameEnum.totalCoins).ToString();
	}

	public void PlayClick()
	{
		AudioClipManager.Instance.Play(1);
		PageHandler._instance.Invoke("Open_ModeSelectionPage", 0.2f);
	}

	public static Menu _instance;

	public Text totalCoinsText;

	public GameObject title;

	public GameObject avatarSelectionPage;

	public GameObject[] bikes;
}
