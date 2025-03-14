using System;
	//using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdController : MonoBehaviour
{
	private void Awake()
	{
		AdController.instance = this;
	}

	private void Start()
	{
		/*this.IsRequestedAdmobRewardBasedVideo = false;
		this.IsRewardVideoRequested = false;
		MobileAds.Initialize(this.AdmobAdUnitID);
		this.AdmobRewardBasedVideo = RewardBasedVideoAd.Instance;
		this.AdmobRewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
		this.AdmobRewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
		this.AdmobRewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
		this.AdmobRewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
		this.AdmobRewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
		this.AdmobRewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
		this.AdmobRewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;
		IronSourceEvents.onInterstitialAdReadyEvent += this.InterstitialAdReadyEvent;
		IronSourceEvents.onInterstitialAdLoadFailedEvent += this.InterstitialAdLoadFailedEvent;
		IronSourceEvents.onInterstitialAdShowSucceededEvent += this.InterstitialAdShowSucceededEvent;
		IronSourceEvents.onInterstitialAdShowFailedEvent += this.InterstitialAdShowFailedEvent;
		IronSourceEvents.onInterstitialAdClickedEvent += this.InterstitialAdClickedEvent;
		IronSourceEvents.onInterstitialAdOpenedEvent += this.InterstitialAdOpenedEvent;
		IronSourceEvents.onInterstitialAdClosedEvent += this.InterstitialAdClosedEvent;
		IronSourceEvents.onRewardedVideoAdOpenedEvent += this.RewardedVideoAdOpenedEvent;
		IronSourceEvents.onRewardedVideoAdClosedEvent += this.RewardedVideoAdClosedEvent;
		IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += this.RewardedVideoAvailabilityChangedEvent;
		IronSourceEvents.onRewardedVideoAdStartedEvent += this.RewardedVideoAdStartedEvent;
		IronSourceEvents.onRewardedVideoAdEndedEvent += this.RewardedVideoAdEndedEvent;
		IronSourceEvents.onRewardedVideoAdRewardedEvent += this.RewardedVideoAdRewardedEvent;
		IronSourceEvents.onRewardedVideoAdShowFailedEvent += this.RewardedVideoAdShowFailedEvent;
		if (Advertisement.isSupported)
		{
			Advertisement.Initialize(this.UnityGameID);
		}
		IronSource.Agent.init(this.IronSourceID, new string[]
		{
			IronSourceAdUnits.REWARDED_VIDEO
		});
		IronSource.Agent.init(this.IronSourceID, new string[]
		{
			IronSourceAdUnits.INTERSTITIAL
		});
		IronSource.Agent.shouldTrackNetworkState(true);*/
	}

	/*public AdRequest CreateAdRequest()
	{
		UnityEngine.Debug.Log("----- Admobe CreateAdRequest");
		return new AdRequest.Builder().Build();
	}

	public void RequestAdmobInterstitial()
	{
		UnityEngine.Debug.Log("----------  Admob RequestInterstitial Ad 11111111");
		if (this.AdmobInterstitial != null)
		{
			UnityEngine.Debug.Log("------ Admob Ad Ready Already(interstitial) so returning");
			return;
		}
		UnityEngine.Debug.Log("---------- Admob RequestInterstitial Load ad call ..");
		this.AdmobInterstitial = new InterstitialAd(this.AdmobAdUnitID);
		this.AdmobInterstitial.OnAdLoaded += this.HandleOnAdLoaded;
		this.AdmobInterstitial.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
		this.AdmobInterstitial.OnAdOpening += this.HandleOnAdOpened;
		this.AdmobInterstitial.OnAdClosed += this.HandleOnAdClosed;
		this.AdmobInterstitial.OnAdLeavingApplication += this.HandleOnAdLeftApplication;
		this.AdmobInterstitial.LoadAd(this.CreateAdRequest());
	}

	public void RequestAdmobRewardBasedVideo()
	{
		if (this.IsRequestedAdmobRewardBasedVideo)
		{
			UnityEngine.Debug.Log("---- Request Admob Reward based video alreay loaded so returing");
			return;
		}
		UnityEngine.Debug.Log("---------- Request Admob RewardBasedVideo Load call..");
		this.IsRequestedAdmobRewardBasedVideo = true;
		this.AdmobRewardBasedVideo.LoadAd(this.CreateAdRequest(), this.AdmobRewardID);
	}

	public void ShowAdmobInterstitial()
	{
		UnityEngine.Debug.Log("---------- Show Admob Interstitial Ad");
		if (this.AdmobInterstitial != null && this.AdmobInterstitial.IsLoaded())
		{
			UnityEngine.Debug.Log("---------- Show Admob Interstitial Ad call..");
			this.AdmobInterstitial.Show();
		}
		else if (this.IsIronSourceInterstitialAvailable())
		{
			UnityEngine.Debug.Log("---------- Show Irsonsource Interstitial Ad call.. as admob not ready");
			this.ShowIronSourceInterstitial();
		}
		else if (this.IsEnableUnityInterstital)
		{
			UnityEngine.Debug.Log("---------- Show Unity Interstitial Ad call.. as admob not ready");
			this.ShowUnityAd();
		}
	}

	public void ShowAdmobRewardBasedVideo()
	{
		UnityEngine.Debug.Log("--------- ShowAdmobRewardBasedVideo isloaded=" + this.AdmobRewardBasedVideo.IsLoaded());
		if (this.IsRewardVideoRequested)
		{
			UnityEngine.Debug.Log("-------- ShowAdmobRewardBasedVideo is returning ISRewardVideoRequested true");
			return;
		}
		if (this.AdmobRewardBasedVideo.IsLoaded())
		{
			UnityEngine.Debug.Log("---------- Show Admob RewardBased Video call.. ");
			this.IsRewardVideoRequested = true;
			this.AdmobRewardBasedVideo.Show();
		}
		else if (this.IsIronSourceRewardVideoAvailable())
		{
			UnityEngine.Debug.Log("---------- Show IronSource RewardBased Video call.. as Admob not ready ");
			this.IsRewardVideoRequested = true;
			IronSource.Agent.showRewardedVideo();
		}
		else if (this.IsEnableUnityReward)
		{
			UnityEngine.Debug.Log("---------- Show Unity RewardBased Video call.. as Admob not ready ");
			this.IsRewardVideoRequested = true;
			this.ShowUnityRewardedVideoAd();
		}
		else
		{
			AdManager.instance.ShowToast("--------- Video not ready to display");
			UnityEngine.Debug.Log("-------- Show Admob Video not ready to display");
		}
	}

	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("------- Admob HandleAdLoaded event received");
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("------- Admob HandleFailedToReceiveAd event received with message: " + args.Message);
		if (this.AdmobInterstitial != null)
		{
			this.AdmobInterstitial.Destroy();
			this.AdmobInterstitial = null;
		}
	}

	public void HandleOnAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("------- Admob HandleAdOpened event received");
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("-------- Admob HandleAdClosed event received");
		if (this.AdmobInterstitial != null)
		{
			this.AdmobInterstitial.Destroy();
			this.AdmobInterstitial = null;
		}
	}

	public void HandleOnAdLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("-------- Admob HandleAdLeftApplication event received");
	}

	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("-------- Admob HandleRewardBasedVideoLoaded event received");
	}

	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("-------- Admob HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
		this.IsRequestedAdmobRewardBasedVideo = false;
		if (this.RequestCount == 0)
		{
			this.RequestAdmobRewardBasedVideo();
			this.RequestCount = 1;
		}
	}

	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{
		this.IsVideoRewarded = false;
		MonoBehaviour.print("-------- Admob HandleRewardBasedVideoOpened event received");
	}

	public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{
		this.IsVideoRewarded = false;
		MonoBehaviour.print("-------- Admob HandleRewardBasedVideoStarted event received");
	}

	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("-------- Admob HandleRewardBasedVideoClosed event received");
		if (!this.IsVideoRewarded)
		{
			AdManager.instance.VideoWatchedSuccessfully(false);
		}
		else
		{
			AdManager.instance.VideoWatchedSuccessfully(true);
		}
		this.RequestCount = 0;
		this.IsRequestedAdmobRewardBasedVideo = false;
		this.IsRewardVideoRequested = false;
		this.RequestAdmobRewardBasedVideo();
	}

	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		string type = args.Type;
		MonoBehaviour.print("-------- Admob HandleRewardBasedVideoRewarded event received for " + args.Amount.ToString() + " " + type);
		this.IsVideoRewarded = true;
		this.RequestCount = 0;
		this.RequestAdmobRewardBasedVideo();
		this.IsRequestedAdmobRewardBasedVideo = false;
		this.IsRewardVideoRequested = false;
	}

	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("-------- Admob HandleRewardBasedVideoLeftApplication event received");
	}

	public void ShowUnityRewardedVideoAd()
	{
		UnityEngine.Debug.Log("-------- Show Unity RewardVideo 11111");
		if (this.IsRewardVideoRequested)
		{
			UnityEngine.Debug.Log("-------- Show Unity RewardVideo returning as IsRewardVideoRequested");
			return;
		}
		UnityEngine.Debug.Log("--------- Show Unity RewardBasedVideo IsUnityVideoRequested=" + this.IsUnityVideoRequested);
		if (!this.IsUnityVideoRequested)
		{
			ShowOptions showOptions = new ShowOptions();
			showOptions.resultCallback = new Action<ShowResult>(this.VideoAdCallbackhandler);
			Advertisement.Show(this.UnityPlacementId, showOptions);
			this.IsUnityVideoRequested = true;
			this.IsRewardVideoRequested = true;
		}
	}

	public void ShowUnityAd()
	{
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"--------- Show UnityAd isRequested=",
			this.IsUnityAdRequested,
			"::IsReady=",
			Advertisement.IsReady()
		}));
		if (!this.IsUnityAdRequested && Advertisement.IsReady())
		{
			Advertisement.Show(new ShowOptions
			{
				resultCallback = new Action<ShowResult>(this.AdCallbackhandler)
			});
			this.IsUnityAdRequested = true;
		}
	}

	private void AdCallbackhandler(ShowResult result)
	{
		this.IsUnityAdRequested = false;
		UnityEngine.Debug.Log("-------- Unity AdCallbackHandler");
		if (result != ShowResult.Finished)
		{
			if (result != ShowResult.Skipped)
			{
				if (result == ShowResult.Failed)
				{
					UnityEngine.Debug.Log("-------- Unity Interstitial Ads Failed trying to call other ads");
					if (this.AdmobInterstitial != null && this.AdmobInterstitial.IsLoaded())
					{
						UnityEngine.Debug.Log("--------show Admob Interstitial call as Unity Failed");
						this.AdmobInterstitial.Show();
					}
					else if (this.IsIronSourceInterstitialAvailable())
					{
						UnityEngine.Debug.Log("-------- Show IronSrouce Interstitial call as Unity Failed");
						this.ShowIronSourceInterstitial();
					}
					else
					{
						UnityEngine.Debug.Log("-------- Unity Ads Failed");
					}
				}
			}
			else
			{
				UnityEngine.Debug.Log("-------- Unity Ad skipped.");
			}
		}
		else
		{
			UnityEngine.Debug.Log("-------- Unity Ad Finished. Rewarding player...");
		}
	}

	private void VideoAdCallbackhandler(ShowResult result)
	{
		this.IsUnityVideoRequested = false;
		this.IsRewardVideoRequested = false;
		UnityEngine.Debug.Log("------- Unity Video AdCallbackhandler");
		if (result != ShowResult.Finished)
		{
			if (result != ShowResult.Skipped)
			{
				if (result == ShowResult.Failed)
				{
					UnityEngine.Debug.Log("-------- Unity Reward Ads Failed trying to call other ads");
					if (this.IsIronSourceRewardVideoAvailable())
					{
						UnityEngine.Debug.Log("-------- Show IronSource Reward video call as Unity Failed..");
						IronSource.Agent.showRewardedVideo();
					}
					else if (this.AdmobRewardBasedVideo.IsLoaded())
					{
						UnityEngine.Debug.Log("-------- Show Admobe Reward video call as Unity Failed..");
						this.AdmobRewardBasedVideo.Show();
					}
					else
					{
						AdManager.instance.ShowToast("Video not ready to display");
						UnityEngine.Debug.Log("-------- Unity Video Not ready to display");
					}
				}
			}
			else
			{
				UnityEngine.Debug.Log("------- Unity Reward Ad skipped.");
				AdManager.instance.VideoWatchedSuccessfully(false);
			}
		}
		else
		{
			UnityEngine.Debug.Log("------- Unity Reward Ad Finished. Rewarding player...");
			AdManager.instance.VideoWatchedSuccessfully(true);
		}
	}

	private void OnApplicationPause(bool isPaused)
	{
		UnityEngine.Debug.Log("----------- OnApplicationPause isPaused=" + isPaused);
		IronSource.Agent.onApplicationPause(isPaused);
	}

	public void ValidateIntegration()
	{
		UnityEngine.Debug.Log("---- IronSource Validate Integration");
		IronSource.Agent.validateIntegration();
	}

	public void LoadIronSourceInterstitial()
	{
		UnityEngine.Debug.Log("---- Load Ironsource Interstitial is available=" + this.IsIronSourceInterstitialAvailable());
		if (!this.IsIronSourceInterstitialAvailable())
		{
			IronSource.Agent.loadInterstitial();
		}
	}

	public void ShowIronSourceInterstitial()
	{
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"---- Show Ironsource Interstitial is Available=",
			this.IsIronSourceInterstitialAvailable(),
			":::IsEnableUnityInterstital=",
			this.IsEnableUnityInterstital
		}));
		if (this.IsIronSourceInterstitialAvailable())
		{
			UnityEngine.Debug.Log("---- Show Ironsource Interstitial call");
			IronSource.Agent.showInterstitial();
		}
		else if (this.AdmobInterstitial != null && this.AdmobInterstitial.IsLoaded())
		{
			UnityEngine.Debug.Log("------ Show Admob Interstitial call as IronSource Failed");
			this.AdmobInterstitial.Show();
		}
		else if (this.IsEnableUnityInterstital)
		{
			UnityEngine.Debug.Log("------ Show Unity Interstitial call as IronSource Failed");
			this.ShowUnityAd();
		}
		else
		{
			UnityEngine.Debug.Log("------- Interstital ads not ready to display");
		}
	}

	public void ShowIronSourceRewardVideo()
	{
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"---- Show IronSource RewardVideo available=",
			this.IsIronSourceRewardVideoAvailable(),
			":::IsEnableUnityReward=",
			this.IsEnableUnityReward
		}));
		if (this.IsRewardVideoRequested)
		{
			UnityEngine.Debug.Log("-------- Show IronSource Reward is returning ISRewardVideoRequested true");
			return;
		}
		if (this.IsIronSourceRewardVideoAvailable())
		{
			UnityEngine.Debug.Log("---- Show IronSource RewardVideo call");
			this.IsRewardVideoRequested = true;
			IronSource.Agent.showRewardedVideo();
		}
		else if (this.AdmobRewardBasedVideo.IsLoaded())
		{
			UnityEngine.Debug.Log("---- Show Admob RewardVideo call as IronSource Reward Failed");
			this.IsRewardVideoRequested = true;
			this.AdmobRewardBasedVideo.Show();
		}
		else if (this.IsEnableUnityReward)
		{
			UnityEngine.Debug.Log("---- Show Unity RewardVideo call as IronSource Reward Failed");
			this.IsRewardVideoRequested = true;
			this.ShowUnityRewardedVideoAd();
		}
		else
		{
			AdManager.instance.ShowToast("Video not ready to display");
			UnityEngine.Debug.Log("----------- Show IronSource Video Not Ready to display");
		}
	}

	public bool IsIronSourceRewardVideoAvailable()
	{
		return IronSource.Agent.isRewardedVideoAvailable();
	}

	public bool IsIronSourceInterstitialAvailable()
	{
		return IronSource.Agent.isInterstitialReady();
	}

	private void InterstitialAdLoadFailedEvent(IronSourceError error)
	{
		UnityEngine.Debug.Log("------- IronSource Interstitial Ad load Failed Event=" + error);
	}

	private void InterstitialAdShowSucceededEvent()
	{
		UnityEngine.Debug.Log("------- IronSource Interstitial Ad Show Success Event");
	}

	private void InterstitialAdShowFailedEvent(IronSourceError error)
	{
		UnityEngine.Debug.Log("------- IronSource Interstitial Ad Show Failed Event");
	}

	private void InterstitialAdClickedEvent()
	{
		UnityEngine.Debug.Log("------- IronSource Interstitial Ad Clicked Event");
	}

	private void InterstitialAdClosedEvent()
	{
		UnityEngine.Debug.Log("------- IronSource Interstitial Ad Closed Event");
	}

	private void InterstitialAdReadyEvent()
	{
		UnityEngine.Debug.Log("------- IronSource Interstitial Ad Ready Event");
	}

	private void InterstitialAdOpenedEvent()
	{
		UnityEngine.Debug.Log("------- IronSource Interstitial Ad Opened Event");
	}

	private void RewardedVideoAdOpenedEvent()
	{
		UnityEngine.Debug.Log("----- IronSource Reward video Opened event");
		this.IsVideoRewarded = false;
	}

	private void RewardedVideoAdClosedEvent()
	{
		UnityEngine.Debug.Log("----- IronSource Reward video Closed event IsVideoRewarded=" + this.IsVideoRewarded);
		this.IsRewardVideoRequested = false;
		if (this.IsVideoRewarded)
		{
			AdManager.instance.VideoWatchedSuccessfully(true);
		}
		else
		{
			AdManager.instance.VideoWatchedSuccessfully(false);
		}
	}

	private void RewardedVideoAvailabilityChangedEvent(bool available)
	{
		UnityEngine.Debug.Log("----- IronSource Reward video Availablity event available=" + available);
	}

	private void RewardedVideoAdStartedEvent()
	{
		UnityEngine.Debug.Log("----- IronSource Reward video Started event");
	}

	private void RewardedVideoAdEndedEvent()
	{
		UnityEngine.Debug.Log("----- IronSource Reward video Ended event");
	}

	private void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
	{
		UnityEngine.Debug.Log("----- IronSource Reward video rewarded event");
		this.IsRewardVideoRequested = false;
		this.IsVideoRewarded = true;
	}

	private void RewardedVideoAdShowFailedEvent(IronSourceError error)
	{
		UnityEngine.Debug.Log("----- IronSource Reward video show failed event");
	}

	private InterstitialAd AdmobInterstitial;

	public RewardBasedVideoAd AdmobRewardBasedVideo;*/

	public string AdmobAdUnitID;

	public string AdmobRewardID;

	public string UnityGameID;

	public string IronSourceID = "7260ef2d";

	[HideInInspector]
	public string UnityPlacementId = "rewardedVideo";

	public static AdController instance;

	private bool IsUnityVideoRequested;

	private bool IsUnityAdRequested;

	private bool IsRequestedAdmobRewardBasedVideo;

	private bool IsRewardVideoRequested;

	private bool IsVideoRewarded;

	[HideInInspector]
	public bool IsEnableUnityInterstital;

	[HideInInspector]
	public bool IsEnableUnityReward;

	private int RequestCount;
}
