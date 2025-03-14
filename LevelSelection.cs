using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
	private void Start()
	{
		LevelSelection._instance = this;
		this.multiPlayerConnectingPage.SetActive(false);
		base.Invoke("TweenButton", 3f);
	}

	public void CheckAllInAppButtons()
	{
		if (AdManager.LevelsUnlocked == 1)
		{
			this.unlockAllLevelsBtn.SetActive(false);
		}
	}

	private void TweenButton()
	{
		this.unlockAllLevelsBtn.GetComponent<PingPong>().enabled = true;
	}

	private void OnEnable()
	{
		if (PageHandler._instance)
		{
			PageHandler._instance.pageType = PageHandler.pageTypeEnum.levelselection;
		}
		for (int i = 0; i < this.levels.Length; i++)
		{
			this.levels[i].transform.GetChild(1).gameObject.SetActive(true);
			this.levels[i].sprite = this.lockSprite;
		}
		this.CheckLevelImages();
		this.UpdateCoinText();
		this.CheckAllInAppButtons();
		for (int j = 0; j < this.levels.Length; j++)
		{
			iTween.ScaleFrom(this.levels[j].gameObject, iTween.Hash(new object[]
			{
				"x",
				0,
				"y",
				0,
				"time",
				0.5,
				"islocal",
				true,
				"delay",
				((double)j + 0.1) * 0.1,
				"easetype",
				iTween.EaseType.easeOutBack
			}));
		}
		if (AdManager.instance)
		{
			AdManager.instance.RunActions(AdManager.PageType.LvlSelection, 1, 0);
		}
	}

	private void AllImagesScaleOne()
	{
		for (int i = 0; i < this.levels.Length; i++)
		{
			this.levels[i].transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}

	public void CheckLevelImages()
	{
		UnityEngine.Debug.Log("Images : ");
		if (ModeSelection._instance.noOfRacers == ModeSelection.NoOfRacers.two)
		{
			for (int i = 0; i < PlayerPrefs.GetInt(GameEnum.levelUnlocked_twoPlayers); i++)
			{
				this.levels[i].transform.GetChild(1).gameObject.SetActive(false);
				this.levels[i].sprite = this.unLockSprite;
			}
			if (PlayerPrefs.GetInt(GameEnum.levelUnlocked_twoPlayers) >= 12)
			{
				iTween.MoveTo(this.levelsContent, iTween.Hash(new object[]
				{
					"x",
					-2188,
					"time",
					1.5f,
					"delay",
					1.5f,
					"islocal",
					true,
					"easetype",
					iTween.EaseType.easeOutBack
				}));
			}
			else if (PlayerPrefs.GetInt(GameEnum.levelUnlocked_twoPlayers) >= 8)
			{
				iTween.MoveTo(this.levelsContent, iTween.Hash(new object[]
				{
					"x",
					-1688,
					"time",
					1.5f,
					"delay",
					1f,
					"islocal",
					true,
					"easetype",
					iTween.EaseType.easeOutBack
				}));
			}
			else if (PlayerPrefs.GetInt(GameEnum.levelUnlocked_twoPlayers) >= 5)
			{
				iTween.MoveTo(this.levelsContent, iTween.Hash(new object[]
				{
					"x",
					-855,
					"time",
					1.5f,
					"delay",
					0.5f,
					"islocal",
					true,
					"easetype",
					iTween.EaseType.easeOutBack
				}));
			}
			else if (PlayerPrefs.GetInt(GameEnum.levelUnlocked_twoPlayers) >= 1)
			{
				iTween.MoveTo(this.levelsContent, iTween.Hash(new object[]
				{
					"x",
					0,
					"time",
					1f,
					"delay",
					0.1f,
					"islocal",
					true,
					"easetype",
					iTween.EaseType.easeOutBack
				}));
			}
		}
		else if (ModeSelection._instance.noOfRacers == ModeSelection.NoOfRacers.four)
		{
			for (int j = 0; j < PlayerPrefs.GetInt(GameEnum.levelUnlocked_fourPlayers); j++)
			{
				this.levels[j].transform.GetChild(1).gameObject.SetActive(false);
				this.levels[j].sprite = this.unLockSprite;
			}
			if (PlayerPrefs.GetInt(GameEnum.levelUnlocked_fourPlayers) >= 12)
			{
				iTween.MoveTo(this.levelsContent, iTween.Hash(new object[]
				{
					"x",
					-2188,
					"time",
					1.5f,
					"delay",
					1.5f,
					"islocal",
					true,
					"easetype",
					iTween.EaseType.easeOutBack
				}));
			}
			else if (PlayerPrefs.GetInt(GameEnum.levelUnlocked_fourPlayers) >= 8)
			{
				iTween.MoveTo(this.levelsContent, iTween.Hash(new object[]
				{
					"x",
					-1688,
					"time",
					1.5f,
					"delay",
					1f,
					"islocal",
					true,
					"easetype",
					iTween.EaseType.easeOutBack
				}));
			}
			else if (PlayerPrefs.GetInt(GameEnum.levelUnlocked_fourPlayers) >= 5)
			{
				iTween.MoveTo(this.levelsContent, iTween.Hash(new object[]
				{
					"x",
					-855,
					"time",
					1.5f,
					"delay",
					0.5f,
					"islocal",
					true,
					"easetype",
					iTween.EaseType.easeOutBack
				}));
			}
			else if (PlayerPrefs.GetInt(GameEnum.levelUnlocked_twoPlayers) >= 1)
			{
				iTween.MoveTo(this.levelsContent, iTween.Hash(new object[]
				{
					"x",
					0,
					"time",
					1f,
					"delay",
					0.1f,
					"islocal",
					true,
					"easetype",
					iTween.EaseType.easeOutBack
				}));
			}
		}
		else if (ModeSelection._instance.noOfRacers == ModeSelection.NoOfRacers.six)
		{
			for (int k = 0; k < PlayerPrefs.GetInt(GameEnum.levelUnlocked_sixPlayers); k++)
			{
				this.levels[k].transform.GetChild(1).gameObject.SetActive(false);
				this.levels[k].sprite = this.unLockSprite;
			}
			if (PlayerPrefs.GetInt(GameEnum.levelUnlocked_sixPlayers) >= 12)
			{
				iTween.MoveTo(this.levelsContent, iTween.Hash(new object[]
				{
					"x",
					-2188,
					"time",
					1.5f,
					"delay",
					1.5f,
					"islocal",
					true,
					"easetype",
					iTween.EaseType.easeOutBack
				}));
			}
			else if (PlayerPrefs.GetInt(GameEnum.levelUnlocked_sixPlayers) >= 8)
			{
				iTween.MoveTo(this.levelsContent, iTween.Hash(new object[]
				{
					"x",
					-1688,
					"time",
					1.5f,
					"delay",
					1f,
					"islocal",
					true,
					"easetype",
					iTween.EaseType.easeOutBack
				}));
			}
			else if (PlayerPrefs.GetInt(GameEnum.levelUnlocked_sixPlayers) >= 5)
			{
				iTween.MoveTo(this.levelsContent, iTween.Hash(new object[]
				{
					"x",
					-855,
					"time",
					1.5f,
					"delay",
					0.5f,
					"islocal",
					true,
					"easetype",
					iTween.EaseType.easeOutBack
				}));
			}
			else if (PlayerPrefs.GetInt(GameEnum.levelUnlocked_twoPlayers) >= 1)
			{
				iTween.MoveTo(this.levelsContent, iTween.Hash(new object[]
				{
					"x",
					0,
					"time",
					1f,
					"delay",
					0.1f,
					"islocal",
					true,
					"easetype",
					iTween.EaseType.easeOutBack
				}));
			}
		}
		base.Invoke("AllImagesScaleOne", 1.5f);
	}

	public void SelectLevel(int currentLevel)
	{
		AudioClipManager.Instance.Play(1);
		this.levelNumber = currentLevel;
		if (this.levelNumber <= PlayerPrefs.GetInt(GameEnum.levelUnlocked_twoPlayers) && ModeSelection._instance.noOfRacers == ModeSelection.NoOfRacers.two)
		{
			UnityEngine.Debug.Log("Level Number : " + this.levelNumber);
			base.Invoke("OpenMultiplayerConnectingPage", 0.3f);
		}
		else if (this.levelNumber <= PlayerPrefs.GetInt(GameEnum.levelUnlocked_fourPlayers) && ModeSelection._instance.noOfRacers == ModeSelection.NoOfRacers.four)
		{
			UnityEngine.Debug.Log("Level Number : " + this.levelNumber);
			base.Invoke("OpenMultiplayerConnectingPage", 0.3f);
		}
		else if (this.levelNumber <= PlayerPrefs.GetInt(GameEnum.levelUnlocked_sixPlayers) && ModeSelection._instance.noOfRacers == ModeSelection.NoOfRacers.six)
		{
			UnityEngine.Debug.Log("Level Number : " + this.levelNumber);
			base.Invoke("OpenMultiplayerConnectingPage", 0.3f);
		}
		else
		{
			UnityEngine.Debug.Log(" Level is Locked");
			AdManager.instance.BuyItem(2, true, null);
		}
	}

	private void OpenMultiplayerConnectingPage()
	{
		this.multiPlayerConnectingPage.SetActive(true);
	}

	public void LoadIngame()
	{
		base.Invoke("GoToInGame", 1f);
	}

	private void GoToInGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
	}

	public void UpdateCoinText()
	{
		this.totalCoinText.text = PlayerPrefs.GetInt(GameEnum.totalCoins).ToString();
	}

	public static LevelSelection _instance;

	public int levelNumber;

	public Image[] levels;

	public Text totalCoinText;

	public GameObject unlockAllLevelsBtn;

	public GameObject levelsContent;

	private int moveValue = 1500;

	public GameObject multiPlayerConnectingPage;

	public Sprite lockSprite;

	public Sprite unLockSprite;
}
