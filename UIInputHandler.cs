using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInputHandler : MonoBehaviour
{
	private void Start()
	{
	}

	public void StringName(string name)
	{
		this.buttonName = name;
		base.Invoke("ButtonClick", 0.4f);
		this.buttonGameObject = EventSystem.current.currentSelectedGameObject;
		this.ButtonDownAnim();
		AudioClipManager.Instance.Play(1);
	}

	private void ButtonDownAnim()
	{
		iTween.ScaleTo(this.buttonGameObject.gameObject, iTween.Hash(new object[]
		{
			"x",
			0.8,
			"y",
			0.8,
			"time",
			0.05f,
			"eastype",
			this.effectType,
			"IgnoreTimeScale",
			true
		}));
		base.Invoke("ButtonUpAnim", 0.1f);
	}

	private void ButtonUpAnim()
	{
		iTween.ScaleTo(this.buttonGameObject.gameObject, iTween.Hash(new object[]
		{
			"x",
			1,
			"y",
			1,
			"time",
			0f,
			"eastype",
			this.effectType,
			"IgnoreTimeScale",
			true
		}));
	}

	public void ButtonClick()
	{
		string text = this.buttonName;
		switch (text)
		{
		case "Play":
			UnityEngine.Debug.Log("Play");
			PageHandler._instance.Open_Upgrade();
			break;
		case "FreeGift":
			UnityEngine.Debug.Log("Free Gift");
			break;
		case "LeaderBoard":
			UnityEngine.Debug.Log("LeaderBoard");
			break;
		case "Achievement":
			UnityEngine.Debug.Log("Achievement");
			break;
		case "GShare":
			UnityEngine.Debug.Log("FShare");
			break;
		case "Back":
			UnityEngine.Debug.Log("Back");
			PageHandler._instance.Open_Menu();
			break;
		case "NextCar":
			Upgrade._instance.NextClick();
			break;
		case "PreviousCar":
			UnityEngine.Debug.Log("PreviousClick");
			Upgrade._instance.PreviousClick();
			break;
		case "CarSelect":
			PageHandler._instance.Open_LevelSelection();
			break;
		case "CarBuy":
			Upgrade._instance.BuyClick(Upgrade._instance.carIndex);
			break;
		case "Store":
			PageHandler._instance.Open_Store();
			break;
		case "StoreClose":
			PageHandler._instance.Close_Store();
			break;
		case "BackBtn":
			UnityEngine.Debug.Log("levelselectionBack");
			PageHandler._instance.Open_Upgrade();
			break;
		case "SettingsOpen":
			PageHandler._instance.Open_Settings();
			break;
		case "SettingsClose":
			UnityEngine.Debug.Log("settings Close::");
			PageHandler._instance.Close_Settings();
			break;
		case "Sound":
			UnityEngine.Debug.Log("Sound__");
			SettingsHCT._instance.CheckSound();
			break;
		case "Rate":
			UnityEngine.Debug.Log("Rate__");
			break;
		case "PromoCode":
			UnityEngine.Debug.Log("Promo Code__");
			break;
		case "MoreGames":
			AdManager.instance.ShowMoreGames();
			break;
		case "LevelComplete":
			UnityEngine.Debug.Log("LC");
			break;
		case "LevelFail":
			UnityEngine.Debug.Log("LF");
			break;
		case "Share":
			UnityEngine.Debug.Log("Share");
			AdManager.instance.FacebookShare(0);
			break;
		case "WatchVideo200":
			UnityEngine.Debug.Log("WatchVideo200");
			AdManager.instance.ShowRewardVideoWithCallback(delegate(bool result)
			{
				if (result)
				{
					Store.AddCoins(1000);
				}
			});
			break;
		case "Home":
			UnityEngine.Debug.Log("Home");
			GameManager._instance.loading.SetActive(true);
			UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
			break;
		case "NoAds":
			UnityEngine.Debug.Log("NoAds");
			break;
		case "UnlockAllTrucks":
			UnityEngine.Debug.Log("UnlockAllTrucks");
			break;
		case "UnlockAllLevels":
			UnityEngine.Debug.Log("UnlockAllLevels");
			break;
		case "UnlockAllLevelsAndTrucks":
			UnityEngine.Debug.Log("UnlockAllLevelsAndTrucks");
			break;
		}
	}

	private GameObject buttonGameObject;

	public iTween.EaseType effectType;

	private string buttonName;
}
