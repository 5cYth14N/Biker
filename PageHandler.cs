using System;
using UnityEngine;

public class PageHandler : MonoBehaviour
{
	private void Awake()
	{
		this.SetPlayerPrefs();
		PageHandler._instance = this;
		UnityEngine.Debug.Log("CameFromIngame__" + PageHandler.isGameManager);
		if (LevelComplete.isLevelComplete || LevelFail.isLevelFail)
		{
			this.Open_ModeSelectionPage();
			LevelComplete.isLevelComplete = false;
		}
		else
		{
			this.Open_Menu();
		}
	}

	private void Start()
	{
		AudioClipManager.Instance.Play(0);
		UnityEngine.Debug.Log("PageHandlerStart");
	}

	public void Open_Menu()
	{
		this.menuPage.SetActive(true);
		this.upgradePage.SetActive(false);
		this.levelSelectionPage.SetActive(false);
		this.storePage.SetActive(false);
		this.settingsPages.SetActive(false);
		this.modeSelectionPage.SetActive(false);
		AudioClipManager.Instance.Sounds[0] = this.MenuSound;
		AudioClipManager.Instance.Play(0);
		this.ShowMenuEnvironment();
	}

	public void Open_ModeSelectionPage()
	{
		this.menuPage.SetActive(false);
		this.upgradePage.SetActive(false);
		this.levelSelectionPage.SetActive(false);
		this.storePage.SetActive(false);
		this.settingsPages.SetActive(false);
		this.modeSelectionPage.SetActive(true);
		this.upgradeEnvironment.SetActive(false);
	}

	public void Open_Upgrade()
	{
		this.menuPage.SetActive(false);
		this.upgradePage.SetActive(true);
		this.levelSelectionPage.SetActive(false);
		this.storePage.SetActive(false);
		this.settingsPages.SetActive(false);
		this.modeSelectionPage.SetActive(false);
		this.ShowUpgradeEnvironment();
	}

	public void Open_LevelSelection()
	{
		this.upgradePage.SetActive(false);
		this.levelSelectionPage.SetActive(true);
		this.storePage.SetActive(false);
		this.settingsPages.SetActive(false);
		this.menuPage.SetActive(false);
		this.modeSelectionPage.SetActive(false);
		this.upgradeEnvironment.SetActive(false);
	}

	public void Open_Store()
	{
		this.storePage.SetActive(true);
		this.upgradePage.SetActive(false);
		this.levelSelectionPage.SetActive(false);
		this.menuPage.SetActive(false);
		this.settingsPages.SetActive(false);
		this.modeSelectionPage.SetActive(false);
		this.upgradeEnvironment.SetActive(false);
	}

	public void Close_Store()
	{
		UnityEngine.Debug.Log("currentPage___" + this.pageType);
		if (this.pageType == PageHandler.pageTypeEnum.upgrade)
		{
			UnityEngine.Debug.Log("Upgrade");
			this.Open_Upgrade();
		}
		if (this.pageType == PageHandler.pageTypeEnum.levelselection)
		{
			UnityEngine.Debug.Log("levelselection");
			this.Open_LevelSelection();
		}
		if (this.pageType == PageHandler.pageTypeEnum.settings)
		{
			UnityEngine.Debug.Log("settings");
			this.Open_Settings();
		}
		if (this.pageType == PageHandler.pageTypeEnum.menu)
		{
			UnityEngine.Debug.Log("menu");
			this.Open_Menu();
		}
	}

	public void Open_Settings()
	{
		this.settingsPages.SetActive(true);
		this.menuPage.SetActive(false);
		this.upgradePage.SetActive(false);
		this.levelSelectionPage.SetActive(false);
		this.storePage.SetActive(false);
		this.modeSelectionPage.SetActive(false);
	}

	public void Close_Settings()
	{
		this.menuPage.SetActive(true);
		this.settingsPages.SetActive(false);
		this.storePage.SetActive(false);
	}

	private void ShowMenuEnvironment()
	{
		this.menuEnvironment.SetActive(true);
		this.upgradeEnvironment.SetActive(false);
	}

	private void ShowUpgradeEnvironment()
	{
		UnityEngine.Debug.Log("show upgrade ");
		this.menuEnvironment.SetActive(false);
		this.upgradeEnvironment.SetActive(true);
	}

	private void SetPlayerPrefs()
	{
		if (!PlayerPrefs.HasKey(GameEnum.totalCoins))
		{
			PlayerPrefs.SetInt(GameEnum.totalCoins, 0);
		}
		if (!PlayerPrefs.HasKey(GameEnum.vehiclePurchased))
		{
			PlayerPrefs.SetString(GameEnum.vehiclePurchased, "1000000000");
		}
		if (!PlayerPrefs.HasKey(GameEnum.levelUnlocked_twoPlayers))
		{
			PlayerPrefs.SetInt(GameEnum.levelUnlocked_twoPlayers, 1);
		}
		if (!PlayerPrefs.HasKey(GameEnum.levelUnlocked_fourPlayers))
		{
			PlayerPrefs.SetInt(GameEnum.levelUnlocked_fourPlayers, 1);
		}
		if (!PlayerPrefs.HasKey(GameEnum.levelUnlocked_sixPlayers))
		{
			PlayerPrefs.SetInt(GameEnum.levelUnlocked_sixPlayers, 1);
		}
		if (!PlayerPrefs.HasKey(GameEnum.isShareDone))
		{
			PlayerPrefs.SetString(GameEnum.isShareDone, "false");
			UnityEngine.Debug.Log("value__" + PlayerPrefs.GetString(GameEnum.isShareDone));
		}
	}

	public static PageHandler _instance;

	public PageHandler.pageTypeEnum pageType;

	public GameObject menuPage;

	public GameObject upgradePage;

	public GameObject levelSelectionPage;

	public GameObject storePage;

	public GameObject settingsPages;

	public GameObject modeSelectionPage;

	public GameObject menuEnvironment;

	public GameObject upgradeEnvironment;

	public static bool isGameManager;

	public AudioClip MenuSound;

	public enum pageTypeEnum
	{
		menu,
		settings,
		upgrade,
		levelselection,
		levelcomplete,
		ingame
	}
}
