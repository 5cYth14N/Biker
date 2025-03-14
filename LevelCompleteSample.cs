using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleteSample : MonoBehaviour
{
	private void Awake()
	{
		LevelCompleteSample.instance = this;
		base.gameObject.SetActive(false);
	}

	public void Open()
	{
		base.gameObject.SetActive(true);
		this.CoinsTxt.text = "Reward:" + this.LvlCompleteCoins;
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
		AdManager.instance.RunActions(AdManager.PageType.LC, InGameSample.CurrentLvl, this.score);
	}

	public void Close()
	{
		SceneManager.LoadScene("MenuSample");
	}

	public void RetryBtnClick()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void NextBtnClick()
	{
		InGameSample.CurrentLvl++;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void WatchVideoDoubleReward()
	{
		AdManager.instance.ShowRewardVideoWithCallback(delegate(bool result)
		{
			this.LvlCompleteCoins *= 2;
			this.CoinsTxt.text = "Reward:" + this.LvlCompleteCoins;
			UnityEngine.Debug.Log("------ Menu Watched video successfully");
		});
	}

	public static LevelCompleteSample instance;

	public GameObject PopUp;

	public Text CoinsTxt;

	private int LvlCompleteCoins = 1000;

	public GameObject DoubleRewardBtn;

	public int score = 1000;
}
