using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
	private void OnEnable()
	{
		for (int i = 0; i < this.rankNames.Count; i++)
		{
			this.rankNames[i].transform.parent.gameObject.SetActive(false);
		}
		base.Invoke("TweenInNames", 0.1f);
		LevelComplete.isLevelComplete = true;
		this.levelCoins = GameManager._instance.levelReward[GameManager._instance.levelNumber - 1];
		UnityEngine.Debug.Log("levelocins :" + this.levelCoins);
		this.CheckPlayerRank();
	}

	private void Start()
	{
		this.myPlayerName = GameManager._instance.currentPlayerBike.GetComponent<BikerDetails>().name;
		LevelComplete._instance = this;
		this.ChangeButtonPos();
		base.Invoke("UnlockButtonPingPong", 1.5f);
	}

	private void UnlockButtonPingPong()
	{
		this.unlockAllLevelsBtn.GetComponent<PingPong>().enabled = true;
	}

	private void ChangeButtonPos()
	{
	}

	private void GiveRewards(int reward)
	{
		PlayerPrefs.SetInt(GameEnum.totalCoins, PlayerPrefs.GetInt(GameEnum.totalCoins) + reward);
		this.UpdateCoinText(reward);
	}

	private void CallRunActionLC()
	{
		if (AdManager.instance)
		{
			AdManager.instance.RunActions(AdManager.PageType.LC, GameManager._instance.levelNumber, this.levelCoins);
		}
	}

	private void CallRunActionLF()
	{
		if (AdManager.instance)
		{
			AdManager.instance.RunActions(AdManager.PageType.LF, GameManager._instance.levelNumber, 0);
		}
	}

	private void CheckPlayerRank()
	{
		UnityEngine.Debug.Log("My Rank :" + RaceManager._instance.rank.Count);
		if (GameManager._instance.PlayVs == GameManager.playVs.twoPlayers)
		{
			if (LevelCompleteTrigger._instance.myRank == 1)
			{
				this.UpdateLevelsUnlock();
				this.completeHelpTxt.gameObject.SetActive(false);
				this.GiveRewards(this.levelCoins);
				UnityEngine.Debug.Log("iam first");
				this.CallRunActionLC();
			}
			else
			{
				UnityEngine.Debug.Log("iam not first");
				this.completeHelpTxt.text = "YOU MUST COME \n 1st POSITION TO UNLOCK NEXT LEVEL.";
				this.continueBtnTxt.text = "RETRY";
				this.GiveRewards(this.levelCoins / 3);
				this.CallRunActionLF();
			}
		}
		else if (GameManager._instance.PlayVs == GameManager.playVs.fourPlayers)
		{
			if (LevelCompleteTrigger._instance.myRank == 1 || LevelCompleteTrigger._instance.myRank == 2)
			{
				this.UpdateLevelsUnlock();
				this.completeHelpTxt.gameObject.SetActive(false);
				this.GiveRewards(this.levelCoins);
				UnityEngine.Debug.Log("iam first");
				this.CallRunActionLC();
			}
			else
			{
				UnityEngine.Debug.Log("iam not first");
				this.completeHelpTxt.text = "YOU MUST COME ATLEAST\n 2nd POSITION TO UNLOCK NEXT LEVEL.";
				this.continueBtnTxt.text = "RETRY";
				this.GiveRewards(this.levelCoins / 3);
				this.CallRunActionLF();
			}
		}
		else if (GameManager._instance.PlayVs == GameManager.playVs.sixPlayers)
		{
			UnityEngine.Debug.Log("Rank Count :" + RaceManager._instance.rank.Count);
			if (LevelCompleteTrigger._instance.myRank == 1 || LevelCompleteTrigger._instance.myRank == 2 || LevelCompleteTrigger._instance.myRank == 3)
			{
				this.UpdateLevelsUnlock();
				this.completeHelpTxt.gameObject.SetActive(false);
				this.GiveRewards(this.levelCoins);
				this.CallRunActionLC();
				UnityEngine.Debug.Log("iam first");
			}
			else
			{
				UnityEngine.Debug.Log("iam not first");
				this.completeHelpTxt.text = "YOU MUST COME ATLEAST\n 3rd POSITION TO UNLOCK NEXT LEVEL.";
				this.continueBtnTxt.text = "RETRY";
				this.GiveRewards(this.levelCoins / 3);
				this.CallRunActionLF();
			}
		}
	}

	private void TweenInNames()
	{
		for (int i = 0; i < this.rankNames.Count; i++)
		{
			iTween.MoveTo(this.rankNames[i].transform.parent.gameObject, iTween.Hash(new object[]
			{
				"x",
				0,
				"time",
				1f,
				"islocal",
				true,
				"delay",
				(float)(i + 1) * 0.01f,
				"easetype",
				iTween.EaseType.easeInBack
			}));
		}
	}

	private void LateUpdate()
	{
		if (!RaceManager._instance.isRaceEnd)
		{
			return;
		}
		if (LevelCompleteTrigger._instance.triggeredCount >= RaceManager._instance.allRacers.Count + 1)
		{
			return;
		}
		for (int i = 0; i < RaceManager._instance.rank.Count; i++)
		{
			for (int j = 0; j < RaceManager._instance.rank.Count; j++)
			{
				this.rankNames[j].transform.parent.gameObject.SetActive(true);
			}
			this.rankNames[i].text = i + 1 + " . " + RaceManager._instance.rank[i];
			this.timeDetails[i].text = RaceManager._instance.racersTimeOfComplete[i].ToString("F2") + " S";
			if (this.rankNames[i].text == i + 1 + " . " + this.myPlayerName)
			{
				this.rankNames[i].transform.parent.gameObject.GetComponent<Image>().color = new Color32(105, 236, 97, 171);
			}
		}
	}

	public void UpdateCoinText(int coins)
	{
		this.totalCoinsText.text = PlayerPrefs.GetInt(GameEnum.totalCoins).ToString();
		this.levelRewardTxt.text = coins.ToString();
	}

	public void DoubleScore()
	{
		AdManager.instance.ShowRewardVideoWithCallback(delegate(bool result)
		{
			if (result)
			{
				Store.AddCoins(this.levelCoins);
			}
		});
	}

	private void UpdateLevelsUnlock()
	{
		UnityEngine.Debug.Log(" player prefs :" + PlayerPrefs.GetInt(GameEnum.levelUnlocked_twoPlayers + ". Levelnumber :" + GameManager._instance.levelNumber));
		UnityEngine.Debug.Log("Levelnumber :" + GameManager._instance.levelNumber);
		if (GameManager._instance.PlayVs == GameManager.playVs.twoPlayers)
		{
			if (PlayerPrefs.GetInt(GameEnum.levelUnlocked_twoPlayers) <= GameManager._instance.levelNumber)
			{
				PlayerPrefs.SetInt(GameEnum.levelUnlocked_twoPlayers, PlayerPrefs.GetInt(GameEnum.levelUnlocked_twoPlayers) + 1);
				UnityEngine.Debug.Log(string.Concat(new object[]
				{
					"____PlayerPrefLevels 2 players__",
					PlayerPrefs.GetInt(GameEnum.levelUnlocked_twoPlayers),
					"___levelnumberCar___",
					GameManager._instance.levelNumber
				}));
			}
		}
		else if (GameManager._instance.PlayVs == GameManager.playVs.fourPlayers)
		{
			if (PlayerPrefs.GetInt(GameEnum.levelUnlocked_fourPlayers) <= GameManager._instance.levelNumber)
			{
				PlayerPrefs.SetInt(GameEnum.levelUnlocked_fourPlayers, PlayerPrefs.GetInt(GameEnum.levelUnlocked_fourPlayers) + 1);
				UnityEngine.Debug.Log(string.Concat(new object[]
				{
					"____PlayerPrefLevels 2 players__",
					PlayerPrefs.GetInt(GameEnum.levelUnlocked_fourPlayers),
					"___levelnumberCar___",
					GameManager._instance.levelNumber
				}));
			}
		}
		else if (GameManager._instance.PlayVs == GameManager.playVs.sixPlayers && PlayerPrefs.GetInt(GameEnum.levelUnlocked_sixPlayers) <= GameManager._instance.levelNumber)
		{
			PlayerPrefs.SetInt(GameEnum.levelUnlocked_sixPlayers, PlayerPrefs.GetInt(GameEnum.levelUnlocked_sixPlayers) + 1);
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"____PlayerPrefLevels 2 players__",
				PlayerPrefs.GetInt(GameEnum.levelUnlocked_sixPlayers),
				"___levelnumberCar___",
				GameManager._instance.levelNumber
			}));
		}
	}

	public void ContinueBtnClick()
	{
		AudioClipManager.Instance.Play(1);
		GameManager._instance.loading.SetActive(true);
		base.Invoke("LoadMainMenu", 4f);
	}

	private void LoadMainMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
	}

	public void WatchVideoButtonClick()
	{
		UnityEngine.Debug.Log("WatchVideo200");
		AdManager.instance.ShowRewardVideoWithCallback(delegate(bool result)
		{
			if (result)
			{
				Store.AddCoins(1000);
			}
		});
	}

	public static LevelComplete _instance;

	public Text totalCoinsText;

	public Text levelRewardTxt;

	public static bool isLevelComplete;

	private int levelCoins;

	private string myPlayerName;

	public Text completeHelpTxt;

	public List<Text> rankNames = new List<Text>();

	public List<Text> timeDetails = new List<Text>();

	public Text continueBtnTxt;

	public Image bG;

	public GameObject continueBtn;

	public GameObject unlockAllLevelsBtn;

	public static int btnIndex;
}
