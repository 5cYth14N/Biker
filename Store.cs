using System;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
	private void Start()
	{
		Store._instance = this;
	}

	private void OnEnable()
	{
		this.UpdateCoinText();
		this.CheckAllBuyButtons();
	}

	public void CheckAllBuyButtons()
	{
		if (PlayerPrefs.GetString(GameEnum.vehiclePurchased) == "1111111111")
		{
			this.unlockalltrucksBtn.SetActive(false);
			this.unlockalltrucksBtn.gameObject.transform.parent.GetComponent<Button>().interactable = false;
		}
		if (AdManager.LevelsUnlocked == 1)
		{
			this.unlockAllLevelsBtn.SetActive(false);
			this.unlockAllLevelsBtn.gameObject.transform.parent.GetComponent<Button>().interactable = false;
		}
		if (AdManager.LevelsUnlocked == 1 && AdManager.UpgradeUnlocked == 1)
		{
			this.unlockAllTrucksAndLevelsBtn.SetActive(false);
			this.unlockAllTrucksAndLevelsBtn.gameObject.transform.parent.GetComponent<Button>().interactable = false;
		}
		if (PlayerPrefs.GetString("NoAds") == "true")
		{
			this.noadsBtn.SetActive(false);
			this.noadsBtn.gameObject.transform.parent.GetComponent<Button>().interactable = false;
		}
	}

	public static void AddCoins(int coins)
	{
		PlayerPrefs.SetInt(GameEnum.totalCoins, PlayerPrefs.GetInt(GameEnum.totalCoins) + coins);
		UnityEngine.Debug.Log("TotalCoins::" + PlayerPrefs.GetInt(GameEnum.totalCoins));
		if (Menu._instance)
		{
			Menu._instance.UpdateCoinText();
		}
		if (LevelSelection._instance)
		{
			LevelSelection._instance.UpdateCoinText();
		}
		if (SettingsHCT._instance)
		{
			SettingsHCT._instance.UpdateCoinText();
		}
		if (Upgrade._instance)
		{
			Upgrade._instance.UpdateCoinText();
		}
		if (LevelComplete._instance)
		{
			LevelComplete._instance.UpdateCoinText(coins);
		}
		if (LevelFail._instance)
		{
			LevelFail._instance.UpdateCoinText();
		}
		if (Store._instance)
		{
			Store._instance.UpdateCoinText();
		}
	}

	public void UpdateCoinText()
	{
		this.coinsText.text = PlayerPrefs.GetInt(GameEnum.totalCoins).ToString();
	}

	public void NoAds()
	{
	}

	public void UnlockAllTrucks()
	{
		PlayerPrefs.SetString(GameEnum.vehiclePurchased, "111111111111111111111111111111");
	}

	public void UnlockAllLevels()
	{
		PlayerPrefs.SetInt(GameEnum.levelUnlocked_twoPlayers, 25);
	}

	public void UnlockAllLevelsAndTrucks()
	{
		this.UnlockAllLevels();
		this.UnlockAllTrucks();
	}

	public static Store _instance;

	public Text coinsText;

	public GameObject noadsBtn;

	public GameObject unlockalltrucksBtn;

	public GameObject unlockAllLevelsBtn;

	public GameObject unlockAllTrucksAndLevelsBtn;

	public Text[] priceTexts;
}
