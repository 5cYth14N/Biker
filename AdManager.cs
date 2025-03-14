using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class AdManager : MonoBehaviour
{
	public static int UpgradeUnlocked
	{
		get
		{
			return PlayerPrefs.GetInt(AdManager._sUpgradeUnlocked, 0);
		}
		set
		{
			PlayerPrefs.SetInt(AdManager._sUpgradeUnlocked, value);
		}
	}

	public static int LevelsUnlocked
	{
		get
		{
			return PlayerPrefs.GetInt(AdManager._sLevelsUnlocked, 0);
		}
		set
		{
			PlayerPrefs.SetInt(AdManager._sLevelsUnlocked, value);
		}
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += this.OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= this.OnSceneLoaded;
	}

	private void Awake()
	{
		AdManager.instance = this;
		this.LastAdShownTime = (float)(-(float)this.AdDelay);
		switch (this.Game_Category)
		{
		case AdManager.GameCategory.Driving:
			this.MenuAdUrl = "https://s3-us-west-2.amazonaws.com/ads2018/Driving.xml";
			break;
		case AdManager.GameCategory.Simulation:
			this.MenuAdUrl = "https://s3-us-west-2.amazonaws.com/ads2018/Simulation.xml";
			break;
		case AdManager.GameCategory.Action:
			this.MenuAdUrl = "https://s3-us-west-2.amazonaws.com/ads2018/Action.xml";
			break;
		case AdManager.GameCategory.Racing:
			this.MenuAdUrl = "https://s3-us-west-2.amazonaws.com/ads2018/Racing.xml";
			break;
		case AdManager.GameCategory.Casual:
			this.MenuAdUrl = "https://s3-us-west-2.amazonaws.com/ads2018/Casual.xml";
			break;
		case AdManager.GameCategory.Tapping:
			this.MenuAdUrl = "https://s3-us-west-2.amazonaws.com/ads2018/Tapping.xml";
			break;
		case AdManager.GameCategory.Match3:
			this.MenuAdUrl = "https://s3-us-west-2.amazonaws.com/ads2018/Match3.xml";
			break;
		case AdManager.GameCategory.Other:
			this.MenuAdUrl = "https://s3-us-west-2.amazonaws.com/ads2018/Innovative.xml";
			break;
		}
	}

	private void Start()
	{
		this.Loading.SetActive(true);
		base.Invoke("LoadingAnimation", 4f);
		this.SessionCount = PlayerPrefs.GetInt("SessionCount", 0);
		PlayerPrefs.SetInt("SessionCount", this.SessionCount + 1);
		base.gameObject.name = "AdManager";
		this.SetInitialAdIndex();
	}

	private void LoadingAnimation()
	{
		this.LoadFirstScene();
	}

	public void SetInitialAdIndex()
	{
		UnityEngine.Debug.Log("---- SetInitialAdIndex");
		this.AdIndexAtRAdsList = -1;
		this.AdIndexAtVideoRAdsList = -1;
		this.RotationAdCheckCount = 0;
		this.setNextInterstitialAdIndex();
		this.RotationVideoAdCheckCount = 0;
		this.setNextVideoAdIndex();
	}

	public void RunActions(AdManager.PageType CurrentPage, int LvlNo = 1, int score = 0)
	{
		UnityEngine.Debug.Log("----- RunActions pageType=" + CurrentPage);
		switch (CurrentPage)
		{
		case AdManager.PageType.Menu:
			PlayServicesController.instance.CheckAutoSignIn();
			if (this.IsOpenLevelDiscountFrmPush)
			{
				this.CheckLevelsInAppPopUp(CurrentPage);
			}
			else
			{
				this.CheckUpgradeInAppPopUp(CurrentPage);
			}
			if (this.AdInPages[1] == 2 || this.AdInPages[2] == 3)
			{
				this.RequestAd();
			}
			this.RequestRewardVideo();
			break;
		case AdManager.PageType.LvlSelection:
			this.RequestRewardVideo();
			this.CheckLevelsInAppPopUp(CurrentPage);
			if (this.AdInPages[1] == 2)
			{
				base.Invoke("ShowAd", this.LsAdDelay);
			}
			break;
		case AdManager.PageType.Upgrade:
			this.RequestRewardVideo();
			this.CheckUpgradeInAppPopUp(CurrentPage);
			if (this.AdInPages[2] == 3)
			{
				base.Invoke("ShowAd", this.LsAdDelay);
			}
			break;
		case AdManager.PageType.InGame:
			this.RequestRewardVideo();
			if (this.AdInPages[3] == 4 || this.AdInPages[4] == 5 || this.AdInPages[5] == 6)
			{
				this.RequestAd();
			}
			break;
		case AdManager.PageType.LC:
			this.isPopUpOpened = this.CheckAndDisplayPopUp(LvlNo);
			if (this.AdInPages[3] == 4)
			{
				base.Invoke("ShowAd", this.LcAdDelay);
			}
			PlayServicesController.instance.Check_UnlockAchievement(LvlNo);
			if (score > 0)
			{
				this.SubmitScore(score);
			}
			break;
		case AdManager.PageType.LF:
			if (this.AdInPages[5] == 6)
			{
				base.Invoke("ShowAd", this.LfAdDelay);
			}
			break;
		case AdManager.PageType.PreLF:
			if (this.AdInPages[4] == 5)
			{
				base.Invoke("ShowAd", this.PreLfAdDelay);
			}
			break;
		}
	}

	private void RequestAd()
	{
		/*UnityEngine.Debug.Log("-------Gameconcontroller RequestAd");
		if (PlayerPrefs.GetString("NoAds", "false") == "true")
		{
			UnityEngine.Debug.Log("----NoAds purchased so returning..");
			return;
		}
		if (this.isWifi_OR_Data_Availble())
		{
			base.CancelInvoke("RequestAdWithDelay");
			if (Time.time < this.LastAdShownTime + (float)this.AdDelay)
			{
				base.Invoke("RequestAdWithDelay", (this.LastAdShownTime + (float)this.AdDelay - Time.time) * 0.5f);
			}
			else
			{
				this.RequestAdWithDelay();
			}
			UnityEngine.Debug.Log("------- Request Ad IsEnableUnityInterstitial=" + AdController.instance.IsEnableUnityInterstital);
		}*/
	}

	private void RequestAdWithDelay()
	{
		/*if (this.RotationAdsList.Count >= 2 && this.RotationAdsList[1] != 0 && this.RotationAdsList[this.AdIndexAtRAdsList] == 2)
		{
			AdController.instance.RequestAdmobInterstitial();
		}
		if (this.RotationAdsList.Count >= 1 && this.RotationAdsList[0] != 0 && this.RotationAdsList[this.AdIndexAtRAdsList] == 1)
		{
			AdController.instance.LoadIronSourceInterstitial();
		}
		for (int i = 0; i < this.RotationAdsList.Count; i++)
		{
			if (this.RotationAdsList[i] == 3)
			{
				AdController.instance.IsEnableUnityInterstital = true;
				break;
			}
		}*/
		UnityEngine.Debug.Log("------- Request Ad IsEnableUnityInterstitial=" + AdController.instance.IsEnableUnityInterstital);
	}

	private void RequestRewardVideo()
	{
		/*for (int i = 0; i < this.RotationVideoAdsList.Count; i++)
		{
			if (this.RotationVideoAdsList[i] == 2)
			{
				AdController.instance.RequestAdmobRewardBasedVideo();
			}
		}
		for (int j = 0; j < this.RotationVideoAdsList.Count; j++)
		{
			if (this.RotationVideoAdsList[j] == 3)
			{
				AdController.instance.IsEnableUnityReward = true;
				break;
			}
			UnityEngine.Debug.Log("------- Request Ad IsEnableUnityReward=" + AdController.instance.IsEnableUnityReward);
		}*/
	}

	public void ShowAd()
	{
		/*UnityEngine.Debug.Log("------------ Show Ad ----------- AdIndexAtRAdsList=" + this.AdIndexAtRAdsList);
		if (PlayerPrefs.GetString("NoAds", "false") == "true")
		{
			UnityEngine.Debug.Log("----NoAds purchased so returning..");
			return;
		}
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"ShowAd time=",
			Time.time,
			"ReqTime To AdDisplay=",
			this.LastAdShownTime + (float)this.AdDelay,
			":::Adindex=",
			this.AdIndexAtRAdsList,
			":::AdType=",
			this.RotationAdsList[this.AdIndexAtRAdsList],
			":::count=",
			this.RotationAdsList.Count
		}));
		if (this.AdIndexAtRAdsList >= 0 && this.RotationAdsList.Count > 0 && Time.time >= this.LastAdShownTime + (float)this.AdDelay)
		{
			if (this.RotationAdsList[this.AdIndexAtRAdsList] == 1)
			{
				UnityEngine.Debug.Log("----- Show Iron source Interstitial Ad");
				AdController.instance.ShowIronSourceInterstitial();
			}
			else if (this.RotationAdsList[this.AdIndexAtRAdsList] == 2)
			{
				UnityEngine.Debug.Log("----- Show Admobe Interstitial Ad");
				AdController.instance.ShowAdmobInterstitial();
			}
			else if (this.RotationAdsList[this.AdIndexAtRAdsList] == 3)
			{
				UnityEngine.Debug.Log("----- Show Unity interstitial Ad");
				AdController.instance.ShowUnityAd();
			}
			else
			{
				UnityEngine.Debug.LogError("----------- Ads Not Activated");
			}
			this.LastAdShownTime = Time.time;
			this.RotationAdCheckCount = 0;
			this.setNextInterstitialAdIndex();
		}
		else
		{
			UnityEngine.Debug.LogError("------- Wait for addelay or Ads Not Activated");
		}*/
	}

	private void setNextInterstitialAdIndex()
	{
		this.RotationAdCheckCount++;
		this.AdIndexAtRAdsList++;
		if (this.AdIndexAtRAdsList >= this.RotationAdsList.Count)
		{
			this.AdIndexAtRAdsList = 0;
		}
		if (this.RotationAdsList[this.AdIndexAtRAdsList] == 0 && this.RotationAdCheckCount < 3)
		{
			this.setNextInterstitialAdIndex();
		}
	}

	private void setNextVideoAdIndex()
	{
		this.RotationVideoAdCheckCount++;
		this.AdIndexAtVideoRAdsList++;
		if (this.AdIndexAtVideoRAdsList >= this.RotationVideoAdsList.Count)
		{
			this.AdIndexAtVideoRAdsList = 0;
		}
		if (this.RotationVideoAdsList[this.AdIndexAtVideoRAdsList] == 0 && this.RotationVideoAdCheckCount < 3)
		{
			this.setNextVideoAdIndex();
		}
	}

	public void ShowRewardVideo(int coins = 1000, AdManager.RewardType type = AdManager.RewardType.Coins)
	{
		/*UnityEngine.Debug.Log("------------ ShowRewardVideo Ad ----------- AdIndexAtVideoRAdsList=" + this.AdIndexAtVideoRAdsList);
		this.VideoRewardCoins = coins;
		this.VideoRewardType = type;
		if (this.AdIndexAtVideoRAdsList >= 0 && this.RotationVideoAdsList.Count > 0)
		{
			if (this.RotationVideoAdsList[this.AdIndexAtVideoRAdsList] == 1)
			{
				UnityEngine.Debug.Log("-------- Show Iron Source reward based video");
				AdController.instance.ShowIronSourceRewardVideo();
			}
			else if (this.RotationVideoAdsList[this.AdIndexAtVideoRAdsList] == 2)
			{
				UnityEngine.Debug.Log("-------- Show Admob reward based video");
				AdController.instance.ShowAdmobRewardBasedVideo();
			}
			else if (this.RotationVideoAdsList[this.AdIndexAtVideoRAdsList] == 3)
			{
				UnityEngine.Debug.Log("-------- Show Unity reward based video");
				AdController.instance.ShowUnityRewardedVideoAd();
			}
			this.RotationVideoAdCheckCount = 0;
			this.setNextVideoAdIndex();
		}*/
	}

	public void ShowRewardVideoWithCallback(AdManager.RewardVideoCallback SuccessCallback)
	{
		UnityEngine.Debug.Log("------------ ShowRewardVideoWithCallback Ad ----------- AdIndexAtVideoRAdsList=" + this.AdIndexAtVideoRAdsList);
		/*this.RewardSuccessEvent = SuccessCallback;
		if (this.AdIndexAtVideoRAdsList >= 0 && this.RotationVideoAdsList.Count > 0)
		{
			if (this.RotationVideoAdsList[this.AdIndexAtVideoRAdsList] == 1)
			{
				UnityEngine.Debug.Log("-------- Show Iron Source reward based video");
				AdController.instance.ShowIronSourceRewardVideo();
			}
			else if (this.RotationVideoAdsList[this.AdIndexAtVideoRAdsList] == 2)
			{
				UnityEngine.Debug.Log("-------- Show Admob reward based video");
				AdController.instance.ShowAdmobRewardBasedVideo();
			}
			else if (this.RotationVideoAdsList[this.AdIndexAtVideoRAdsList] == 3)
			{
				UnityEngine.Debug.Log("-------- Show Unity reward based video");
				AdController.instance.ShowUnityRewardedVideoAd();
			}
			this.RotationVideoAdCheckCount = 0;
			this.setNextVideoAdIndex();
		}*/
	}

	public void VideoWatchedSuccessfully(bool isRewarded = true)
	{
		if (this.RewardSuccessEvent != null)
		{
			this.RewardSuccessEvent(isRewarded);
			this.RewardSuccessEvent = null;
		}
		else
		{
			CallbacksController.instance.VideoRewardCallback(isRewarded);
		}
	}

	private void CheckUpgradeInAppPopUp(AdManager.PageType CurrentPage)
	{
		if (!this.isWifi_OR_Data_Availble())
		{
			return;
		}
		if (AdManager.UpgradeUnlocked == 1)
		{
			return;
		}
		this.SessionCount = PlayerPrefs.GetInt("SessionCount", 1);
		if (CurrentPage != AdManager.PageType.Menu)
		{
			if (CurrentPage == AdManager.PageType.Upgrade)
			{
				this.PageNavigateCount = PlayerPrefs.GetInt("UpgradePageNavigationCount", 0);
				this.PageNavigateCount++;
				PlayerPrefs.SetInt("UpgradePageNavigationCount", this.PageNavigateCount);
				if (this.UnlockPopIn_UP_LS.Length > 0 && this.PageNavigateCount == this.UnlockPopIn_UP_LS[0])
				{
					UpgradesInAppPage.instance.IsShowDiscountPop = false;
					UpgradesInAppPage.instance.Open();
					PlayerPrefs.SetInt("UpgradePageNavigationCount", 0);
				}
			}
		}
		else
		{
			if (this.IsUpgradeShownInMenu)
			{
				return;
			}
			this.PageNavigateCount = PlayerPrefs.GetInt("MenuPageNavigationCount", 0);
			this.PageNavigateCount++;
			PlayerPrefs.SetInt("MenuPageNavigationCount", this.PageNavigateCount);
			if (this.IsOpenUpgradeDiscountFrmPush || (this.DiscountPopInMenu.Count > 0 && this.DiscountPopInMenu.Contains(this.SessionCount)))
			{
				UpgradesInAppPage.instance.IsShowDiscountPop = true;
				UpgradesInAppPage.instance.Open();
				this.IsUpgradeShownInMenu = true;
				if (this.IsOpenUpgradeDiscountFrmPush)
				{
					this.IsOpenUpgradeDiscountFrmPush = false;
				}
			}
		}
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"---CheckUpgradeInAppPopUp SessionCount=",
			this.SessionCount,
			":::pageNavigationCount=",
			this.PageNavigateCount
		}));
	}

	private void CheckLevelsInAppPopUp(AdManager.PageType CurrentPage)
	{
		if (!this.isWifi_OR_Data_Availble())
		{
			return;
		}
		if (AdManager.LevelsUnlocked == 1)
		{
			return;
		}
		this.SessionCount = PlayerPrefs.GetInt("SessionCount", 1);
		if (CurrentPage != AdManager.PageType.Menu)
		{
			if (CurrentPage == AdManager.PageType.LvlSelection)
			{
				this.PageNavigateCount = PlayerPrefs.GetInt("LevelsPageNavigationCount", 0);
				this.PageNavigateCount++;
				PlayerPrefs.SetInt("LevelsPageNavigationCount", this.PageNavigateCount);
				if (this.UnlockPopIn_UP_LS.Length > 1 && this.PageNavigateCount == this.UnlockPopIn_UP_LS[1])
				{
					LevelsInAppPage.instance.IsShowDiscountPop = false;
					LevelsInAppPage.instance.Open();
					PlayerPrefs.SetInt("LevelsPageNavigationCount", 0);
				}
			}
		}
		else
		{
			if (this.IsUpgradeShownInMenu)
			{
				return;
			}
			this.PageNavigateCount = PlayerPrefs.GetInt("MenuPageNavigationCount", 0);
			this.PageNavigateCount++;
			PlayerPrefs.SetInt("MenuPageNavigationCount", this.PageNavigateCount);
			if (this.IsOpenLevelDiscountFrmPush)
			{
				LevelsInAppPage.instance.IsShowDiscountPop = true;
				LevelsInAppPage.instance.Open();
				this.IsUpgradeShownInMenu = true;
				this.IsOpenLevelDiscountFrmPush = false;
			}
		}
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"---CheckLevelsInAppPopUp SessionCount=",
			this.SessionCount,
			":::pageNavigationCount=",
			this.PageNavigateCount
		}));
	}

	private bool CheckAndDisplayPopUp(int LvlNo)
	{
		if (this.isWifi_OR_Data_Availble() && this.RatingPopInLevels.Contains(LvlNo) && PlayerPrefs.GetString("IsRated", "false") == "false")
		{
			base.Invoke("ShowRatePopUp", this.PopUpDelayTime);
			return true;
		}
		if (this.isWifi_OR_Data_Availble() && this.SharingPopInLevels.Contains(LvlNo) && PlayerPrefs.GetString("IsFBShared", "false") == "false")
		{
			base.Invoke("ShowSharePopUp", this.PopUpDelayTime);
			return true;
		}
		return false;
	}

	public void BuyItem(int index, bool IsNonConsumable = false, GameObject BuyBtn = null)
	{
		InAppController.instance.BuyProductID(index, IsNonConsumable, BuyBtn);
	}

	public void GoToPush()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("PushController");
	}

	public void FacebookShare(int rewardCoins = 0)
	{
		/*using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.timuz.webviewhandler.WebViewActivity"))
		{
			androidJavaClass.CallStatic("FBShare", new object[]
			{
				this.ShareUrl
			});
		}*/
		if (rewardCoins > 0)
		{
			base.StartCoroutine(this.AddCoinsWithDelay(2f, rewardCoins, AdManager.RewardDescType.Sharing));
		}
	}

	public void ShareIT()
	{
		/*using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.timuz.webviewhandler.WebViewActivity"))
         		{
         			androidJavaClass.CallStatic("shareIt", new object[]
         			{
         				AdManager.RewardDescType.Sharing
         			});
         		}*/
	}

	public void YoutubeSubscribe(int rewardCoins = 0)
	{
		Application.OpenURL("https://www.youtube.com/user/gamesgeni/feed");
		PlayerPrefs.SetString("YoutubeUsed", "true");
		if (rewardCoins > 0)
		{
			base.StartCoroutine(this.AddCoinsWithDelay(2f, rewardCoins, AdManager.RewardDescType.Sharing));
		}
	}

	public void TwitterFollow(int rewardCoins = 0)
	{
		Application.OpenURL("https://twitter.com/timuzgames");
		PlayerPrefs.SetString("TwitterUsed", "true");
		if (rewardCoins > 0)
		{
			base.StartCoroutine(this.AddCoinsWithDelay(2f, rewardCoins, AdManager.RewardDescType.Sharing));
		}
	}

	public void FBLike(int rewardCoins = 0)
	{
		Application.OpenURL("https://www.facebook.com/timuzsolutions/");
		PlayerPrefs.SetString("FacebookUsed", "true");
		if (rewardCoins > 0)
		{
			base.StartCoroutine(this.AddCoinsWithDelay(2f, rewardCoins, AdManager.RewardDescType.Sharing));
		}
	}

	public void InstagramFollow(int rewardCoins = 0)
	{
		Application.OpenURL("https://www.instagram.com/timuzgamesofficial/");
		PlayerPrefs.SetString("InstagramUsed", "true");
		if (rewardCoins > 0)
		{
			base.StartCoroutine(this.AddCoinsWithDelay(2f, rewardCoins, AdManager.RewardDescType.Sharing));
		}
	}

	public IEnumerator AddCoinsWithDelay(float waitTime, int coins, AdManager.RewardDescType TypeofDesc)
	{
		yield return new WaitForSeconds(waitTime);
		CallbacksController.instance.AddCoins(coins, TypeofDesc, AdManager.RewardType.Coins);
		yield break;
	}

	public void ShowRatePopUp()
	{
		RatePage.instance.Open();
	}

	public void ShowSharePopUp()
	{
		SharePage.instance.Open();
	}

	public void ShowLeaderBoards()
	{
		PlayServicesController.instance.ShowLeaderBoards();
	}

	public void ShowAchievements()
	{
		PlayServicesController.instance.ShowAchievements();
	}

	public void ShowMoreGames()
	{
		WebViewController.instance.ShowMoreGames();
	}

	public void SubmitScore(int score)
	{
		PlayServicesController.instance.SubmitScoreToLB(score);
	}

	public void UnlockAchievements(int LvlNo)
	{
		PlayServicesController.instance.Check_UnlockAchievement(LvlNo);
	}

	public void ShowSocialBtns()
	{
		SocialBtnsController.instance.Open();
	}

	public void HideSocialBtns()
	{
		SocialBtnsController.instance.Close();
	}

	public void ShowLCMoreGames()
	{
		LCMoreGames.instance.Open();
	}

	public void HideLCMoreGames()
	{
		LCMoreGames.instance.Close();
	}

	public void LoadFirstScene()
	{
		this.Loading.SetActive(false);
		if (!PlayerPrefs.HasKey("IsGotWelcomeGift"))
		{
			UnityEngine.Debug.Log("------Give welcome gift");
			PlayerPrefs.SetString("IsGotWelcomeGift", "true");
			WelcomeGiftPage.instance.Open();
		}
		else
		{
			if (this.IsMenuLoaded)
			{
				return;
			}
			UnityEngine.Debug.Log("------OpenMenuScene");
			this.Loading.SetActive(true);
			if (this.menuAdImg != null)
			{
				base.StartCoroutine(this.OpenMenuScene(0f, false));
			}
			else
			{
				base.StartCoroutine(this.OpenMenuScene(8f, false));
			}
		}
	}

	public IEnumerator OpenMenuScene(float waitTime, bool IsFromXml = false)
	{
		yield return new WaitForSeconds(waitTime);
		if (this.IsMenuLoaded)
		{
			yield break;
		}
		UnityEngine.Debug.Log("-------- CallToOpenMenuScene IsFromXml=" + IsFromXml);
		base.CancelInvoke("OpenMenuScene");
		SceneManager.LoadScene(1);
		this.IsMenuLoaded = true;
		yield break;
	}

	public bool isWifi_OR_Data_Availble()
	{
		return Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
	}

	public void ShowToast(string msg)
	{
		WebViewController.instance.ShowToastMsg(msg);
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		UnityEngine.Debug.Log(mode);
		int buildIndex = scene.buildIndex;
		if (buildIndex == 1)
		{
			this.Loading.SetActive(false);
			if (this.AdInPages[0] == 1 && this.menuAdImg != null && !this.IsMenuAdOpened)
			{
				MenuAdPage.instance.Open();
				this.IsMenuAdOpened = true;
			}
		}
	}

	public GameObject Loading;

	public AdManager.GameCategory Game_Category;

	public float menuAdDelay = 1f;

	public string AllCommonUrl = "https://s3-us-west-2.amazonaws.com/ads2018/allcomman.xml";

	public List<int> RatingPopInLevels = new List<int>();

	public List<int> SharingPopInLevels = new List<int>();

	public string ShareUrl = "http://upon.in/eurotrainsimulatortimuz/fb";

	private bool IsMenuAdOpened;

	private bool IsUpgradeShownInMenu;

	private float PopUpDelayTime = 1f;

	private int PageNavigateCount;

	private int SessionCount;

	private int AdIndexAtRAdsList;

	private int AdIndexAtVideoRAdsList;

	[Header("Edit Below Variables Ony If Mandatory")]
	public int WelcomeGiftReward;

	public int RewardToWatchAnotherVideo;

	public string RateDesc;

	public string ShareDesc;

	public int[] AdInPages = new int[]
	{
		1,
		0,
		0,
		4,
		0,
		6
	};

	public List<int> RotationAdsList = new List<int>
	{
		1,
		2,
		3
	};

	public List<int> RotationVideoAdsList = new List<int>
	{
		1,
		2,
		3
	};

	public int AdDelay = 90;

	public float LcAdDelay;

	public float LfAdDelay;

	public float PreLfAdDelay;

	public float LsAdDelay;

	public float UpgradeAdDelay;

	public List<int> DiscountPopInMenu = new List<int>
	{
		2,
		3
	};

	public int[] UnlockPopIn_UP_LS = new int[]
	{
		2,
		3
	};

	public int RateCoins;

	public int ShareCoins;

	public int VideoRewardCoins;

	public AdManager.RewardType VideoRewardType;

	public int VideoRewardPriority;

	public float LastAdShownTime;

	public Sprite menuAdImg;

	public string MenuAdImgLink;

	public string MenuAdLinkTo;

	public string MgLink;

	public string ExitLink;

	public bool IsMenuLoaded;

	public bool IsOpenUpgradeDiscountFrmPush;

	public bool IsOpenLevelDiscountFrmPush;

	public string MenuAdUrl = string.Empty;

	public List<Sprite> MgImgList = new List<Sprite>();

	public List<string> MgImgLinkList = new List<string>();

	public List<string> MgLinkToList = new List<string>();

	private static string _sUpgradeUnlocked = "UpgradeUnlockedA";

	private static string _sLevelsUnlocked = "LevelsUnlockedA";

	public static AdManager instance;

	public AdManager.RewardVideoCallback RewardSuccessEvent;

	private bool isPopUpOpened;

	private int RotationAdCheckCount;

	private int RotationVideoAdCheckCount;

	public enum GameCategory
	{
		Driving,
		Simulation,
		Action,
		Racing,
		Casual,
		Tapping,
		Match3,
		Other
	}

	public enum PageType
	{
		Menu,
		LvlSelection,
		Upgrade,
		InGame,
		LC,
		LF,
		PreLF
	}

	public enum RewardType
	{
		WelcomeGift,
		Coins,
		DoubleCoins,
		Resume,
		WatchVideoAgain
	}

	public enum RewardDescType
	{
		WelcomeGift,
		WatchVideo,
		Sharing,
		Rating,
		Notification,
		Other
	}

	public delegate void RewardVideoCallback(bool rewarded);
}
