using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionSample : MonoBehaviour
{
	private void Awake()
	{
		LevelSelectionSample.instance = this;
	}

	private void Start()
	{
		this.CoinsTxt.text = "Coins:" + PlayerPrefs.GetInt("MyCoins", 100);
		AdManager.instance.RunActions(AdManager.PageType.LvlSelection, 1, 0);
	}

	public void BackClick()
	{
		SceneManager.LoadScene("MenuSample");
	}

	public void LevelClick(int LvlIndex)
	{
		InGameSample.CurrentLvl = LvlIndex;
		SceneManager.LoadScene("UpgradeSample");
	}

	public void UnlockAllClick()
	{
	}

	public void StoreClick()
	{
		StoreSample.instance.Open();
	}

	public void WatchVideo()
	{
		AdManager.instance.ShowRewardVideo(1000, AdManager.RewardType.Coins);
	}

	public void AddCoins(int value)
	{
		int num = PlayerPrefs.GetInt("MyCoins", 100);
		num += value;
		PlayerPrefs.SetInt("MyCoins", num);
		this.CoinsTxt.text = "Coins:" + num;
		UnityEngine.Debug.Log("------ Menu Watched video successfully");
	}

	public static LevelSelectionSample instance;

	public Text CoinsTxt;
}
