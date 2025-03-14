using System;
using System.Collections.Generic;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayServicesController : MonoBehaviour
{
	private void Awake()
	{
		PlayServicesController.instance = this;
		this.IsCheckedSignIn = false;
	}

	private void Start()
	{
		/*PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.InitializeInstance(configuration);
		PlayGamesPlatform.Activate();
		((PlayGamesPlatform)Social.Active).SetDefaultLeaderboardForUI("CgkIqKX65poQEAIQCg");
		UnityEngine.Debug.Log("---------- play services authenticated=" + PlayGamesPlatform.Instance.localUser.authenticated);
		this.AchievementIds[0] = "CgkIqKX65poQEAIQAA";
		this.AchievementIds[1] = "CgkIqKX65poQEAIQAQ";
		this.AchievementIds[2] = "CgkIqKX65poQEAIQAg";
		this.AchievementIds[3] = "CgkIqKX65poQEAIQAw";
		this.AchievementIds[4] = "CgkIqKX65poQEAIQBA";
		this.AchievementIds[5] = "CgkIqKX65poQEAIQBQ";
		this.AchievementIds[6] = "CgkIqKX65poQEAIQBg";
		this.AchievementIds[7] = "CgkIqKX65poQEAIQBw";
		this.AchievementIds[8] = "CgkIqKX65poQEAIQCA";
		this.AchievementIds[9] = "CgkIqKX65poQEAIQCQ";*/
	}

	public void CheckAutoSignIn()
	{
		if (this.IsCheckedSignIn)
		{
			return;
		}
		UnityEngine.Debug.Log("----------------CheckAutoSignIn 111111");
		if (PlayerPrefs.GetString("IsGoogleAuthenticate", "false") == "false")
		{
			UnityEngine.Debug.Log("----------------CheckAutoSignIn 222222222");
			this.SignIn();
		}
		else
		{
			UnityEngine.Debug.Log("----------------CheckAutoSignIn 333333");
			UnityEngine.Debug.Log("------------ Google Authenticating in background");
		//	PlayGamesPlatform.Instance.Authenticate(new Action<bool>(this.SignInCallback), true);
		}
		this.IsCheckedSignIn = true;
	}

	public void SignIn()
	{
		UnityEngine.Debug.Log("----------- playservices 111111111");
		/*if (!PlayGamesPlatform.Instance.localUser.authenticated)
		{
			UnityEngine.Debug.Log("----------- playservices 222222222");
			PlayGamesPlatform.Instance.Authenticate(new Action<bool>(this.SignInCallback), false);
		}*/
	}

	public void SignInCallback(bool success)
	{
		if (success)
		{
			UnityEngine.Debug.Log(" Signed in!");
			PlayerPrefs.SetString("IsGoogleAuthenticate", "true");
		}
		else
		{
			UnityEngine.Debug.Log("(Lollygagger) Sign-in failed...");
		}
	}

	public void ShowLeaderBoards()
	{
		UnityEngine.Debug.Log("ShowLeaderBoards called");
		/*if (PlayGamesPlatform.Instance.localUser.authenticated)
		{
			UnityEngine.Debug.Log("ShowLeaderBoards UI called instance=" + PlayGamesPlatform.Instance);
			PlayGamesPlatform.Instance.ShowLeaderboardUI();
		}
		else
		{
			this.SignIn();
			UnityEngine.Debug.Log("Cannot show leaderboard: not authenticated");
		}*/
	}

	public void ShowAchievements()
	{
		UnityEngine.Debug.Log("ShowAchievements called");
		/*if (PlayGamesPlatform.Instance.localUser.authenticated)
		{
			UnityEngine.Debug.Log("ShowAchievements UI called=" + PlayGamesPlatform.Instance);
			PlayGamesPlatform.Instance.ShowAchievementsUI();
		}
		else
		{
			this.SignIn();
			UnityEngine.Debug.Log("Cannot show Achievements, not logged in");
		}*/
	}

	public void Check_UnlockAchievement(int LvlNo)
	{
		UnityEngine.Debug.Log("--------------- CheckUnlockAchievement 1111 lvlno=" + LvlNo);
		if (this.UnlockAchievementsInLvls.Contains(LvlNo))
		{
			int num = this.UnlockAchievementsInLvls.IndexOf(LvlNo);
			UnityEngine.Debug.Log("--------------- CheckUnlockAchievement 22222222 indx=" + num);
			if (Social.localUser.authenticated)
			{
				/*PlayGamesPlatform.Instance.ReportProgress(this.AchievementIds[num], 100.0, delegate(bool success)
				{
					UnityEngine.Debug.Log("Achievement unlocked" + success);
				});*/
			}
		}
	}

	public void SubmitScoreToLB(int score)
	{
		/*if (PlayGamesPlatform.Instance.localUser.authenticated)
		{
			PlayGamesPlatform.Instance.ReportScore((long)score, "CgkIqKX65poQEAIQCg", delegate(bool success)
			{
				UnityEngine.Debug.Log("Score submitted to LB" + success);
			});
		}*/
	}

	private bool mAuthenticating;

	public static PlayServicesController instance;

	public List<int> UnlockAchievementsInLvls;

	private string[] AchievementIds = new string[10];

	public bool IsCheckedSignIn;
}
