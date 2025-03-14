using System;
using UnityEngine;

public class CallbacksController : MonoBehaviour
{
	private void Awake()
	{
		CallbacksController.instance = this;
	}

	public void VideoRewardCallback(bool isRewarded)
	{
		if (isRewarded)
		{
			AdManager.RewardType videoRewardType = AdManager.instance.VideoRewardType;
			if (videoRewardType != AdManager.RewardType.Coins)
			{
				if (videoRewardType != AdManager.RewardType.DoubleCoins)
				{
					if (videoRewardType == AdManager.RewardType.Resume)
					{
						UnityEngine.Debug.Log("----- Resume Game play");
					}
				}
				else
				{
					UnityEngine.Debug.Log("----- Give Double coins as reward");
				}
			}
			else
			{
				UnityEngine.Debug.Log("------ Give coins as reward");
				this.AddCoins(AdManager.instance.VideoRewardCoins, AdManager.RewardDescType.WatchVideo, AdManager.RewardType.Coins);
			}
		}
	}

	public void InAppCallBacks(string productID)
	{
		PlayerPrefs.SetString("NoAds", "true");
		if (string.Equals(productID, InAppController.instance.NonConsumableProducts[0], StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log("Purchase Success product ID=" + productID);
		}
		else if (string.Equals(productID, InAppController.instance.NonConsumableProducts[1], StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log("Purchase Success product ID=" + productID);
			this.UnlockAllBikes();
		}
		else if (string.Equals(productID, InAppController.instance.NonConsumableProducts[2], StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log("Purchase Success product ID=" + productID);
			this.UnlockAllLevels();
		}
		else if (string.Equals(productID, InAppController.instance.NonConsumableProducts[3], StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log("Purchase Success product ID=" + productID);
			this.UnlockAll();
		}
		else if (string.Equals(productID, InAppController.instance.NonConsumableProducts[4], StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log("Purchase Success product ID=" + productID);
			this.UnlockAllBikes();
		}
		else if (string.Equals(productID, InAppController.instance.NonConsumableProducts[5], StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log("Purchase Success product ID=" + productID);
			this.UnlockAllLevels();
		}
	}

	public void AddCoins(int coins, AdManager.RewardDescType rewardDescType = AdManager.RewardDescType.WatchVideo, AdManager.RewardType rewardType = AdManager.RewardType.Coins)
	{
		Store.AddCoins(coins);
		if (rewardType != AdManager.RewardType.WatchVideoAgain && rewardType != AdManager.RewardType.WelcomeGift)
		{
			CoinsAddedPopUp.instance.Open(coins, rewardDescType);
		}
	}

	private void UnlockAllBikes()
	{
		PlayerPrefs.SetString(GameEnum.vehiclePurchased, "1111111111");
		AdManager.UpgradeUnlocked = 1;
	}

	private void UnlockAllLevels()
	{
		PlayerPrefs.SetInt(GameEnum.levelUnlocked_twoPlayers, 15);
		PlayerPrefs.SetInt(GameEnum.levelUnlocked_fourPlayers, 15);
		PlayerPrefs.SetInt(GameEnum.levelUnlocked_sixPlayers, 15);
		AdManager.LevelsUnlocked = 1;
		if (LevelSelection._instance)
		{
			LevelSelection._instance.CheckLevelImages();
		}
	}

	private void UnlockAll()
	{
		this.UnlockAllLevels();
		this.UnlockAllBikes();
	}

	public static CallbacksController instance;
}
