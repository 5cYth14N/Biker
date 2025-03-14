using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private void Awake()
	{
		if (!PlayerPrefs.HasKey(GameManager.help))
		{
			UnityEngine.Debug.Log("show help ");
			PlayerPrefs.SetString(GameManager.help, "true");
		}
		GameManager._instance = this;
		if (PlayerPrefs.GetString(GameManager.help) == "false")
		{
			base.StartCoroutine(this.RaceCountDown(0f));
		}
		else
		{
			this.raceCountDownPopup.SetActive(false);
			this.helpImg.gameObject.SetActive(true);
		}
		if (GameManager.cameFromMenu)
		{
			if (ModeSelection._instance.noOfRacers == ModeSelection.NoOfRacers.two)
			{
				this.PlayVs = GameManager.playVs.twoPlayers;
			}
			else if (ModeSelection._instance.noOfRacers == ModeSelection.NoOfRacers.four)
			{
				this.PlayVs = GameManager.playVs.fourPlayers;
			}
			else if (ModeSelection._instance.noOfRacers == ModeSelection.NoOfRacers.six)
			{
				this.PlayVs = GameManager.playVs.sixPlayers;
			}
		}
		if (this.playBg != null)
		{
			this.playBg = GameObject.Find("BGSoundManager").GetComponent<AudioSource>();
			this.playBg.clip = this.playBgSound;
			this.playBg.Play();
		}
		if (AdManager.instance)
		{
			AdManager.instance.RunActions(AdManager.PageType.InGame, 1, 0);
		}
	}

	private void Start()
	{
		this.SetLevelsAndBikes();
		RenderSettings.fog = true;
	}

	private void SetLevelsAndBikes()
	{
		UnityEngine.Debug.Log("CameFromMenu___" + GameManager.cameFromMenu);
		if (!GameManager.cameFromMenu)
		{
			for (int i = 0; i < this.levels.Length; i++)
			{
				this.levels[i].SetActive(false);
				this.myLevel = this.levels[this.levelNumber - 1];
			}
			for (int j = 0; j < this.playerBikes.Length; j++)
			{
				this.currentPlayerBike = this.playerBikes[this.bikeNumber - 1];
			}
		}
		else
		{
			UnityEngine.Debug.Log("CameFromMenu___" + GameManager.cameFromMenu);
			for (int k = 0; k < this.levels.Length; k++)
			{
				this.levels[k].SetActive(false);
			}
			this.bikeNumber = Upgrade._instance.carIndex;
			this.currentPlayerBike = this.playerBikes[this.bikeNumber];
			this.levelNumber = LevelSelection._instance.levelNumber;
			this.myLevel = this.levels[this.levelNumber - 1];
		}
		this.currentPlayerBike.SetActive(true);
		this.myLevel.SetActive(true);
		base.Invoke("GenerateAiBike", 0f);
	}

	private void InsertPlayerRankImage()
	{
		if (GameManager.cameFromMenu)
		{
			UnityEngine.Debug.Log(" My name : " + PlayerPrefs.GetString("playername"));
			this.currentPlayerBike.GetComponent<BikerDetails>().name = PlayerPrefs.GetString("playername");
		}
		else
		{
			this.currentPlayerBike.GetComponent<BikerDetails>().name = "Siraj";
		}
		this.currentPlayerBike.GetComponent<BikerDetails>().icon = RaceManager._instance.aiIcons[9];
		this.isInsertedAleardy = true;
	}

	private void GenerateAiBike()
	{
		GameObject slotsParent = RaceManager._instance.slotsParent;
		if (this.PlayVs == GameManager.playVs.twoPlayers)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.aiBikes[0].gameObject, new Vector3(this.currentPlayerBike.transform.position.x, this.currentPlayerBike.transform.position.y, this.currentPlayerBike.transform.position.z + 2f), Quaternion.identity);
			GameObject item = UnityEngine.Object.Instantiate<GameObject>(this.startStand.gameObject, new Vector3(this.startStand.transform.position.x, this.startStand.transform.position.y, this.startStand.transform.position.z + 2f), Quaternion.identity);
			this.startStandList.Add(item);
			RaceManager._instance.currentAiBikes.Add(gameObject);
			if (GameManager.cameFromMenu)
			{
				gameObject.GetComponent<BikerDetails>().name = Multiplayerpage._instance.ais[0].name;
			}
			else
			{
				gameObject.GetComponent<BikerDetails>().name = RaceManager._instance.aiPlayerNames[0];
			}
			gameObject.GetComponent<BikerDetails>().icon = RaceManager._instance.aiIcons[0];
			gameObject.GetComponent<BikerDetails>().distanceTxt = RaceManager._instance.distanceTxt[0];
			slotsParent.transform.localPosition = new Vector3(-161f, slotsParent.transform.localPosition.y, slotsParent.transform.localPosition.z);
			if (!this.isInsertedAleardy)
			{
				this.InsertPlayerRankImage();
			}
			this.aiCount = 1;
		}
		else if (this.PlayVs == GameManager.playVs.fourPlayers)
		{
			for (int i = 0; i < 3; i++)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.aiBikes[i].gameObject, new Vector3(this.currentPlayerBike.transform.position.x, this.currentPlayerBike.transform.position.y, this.currentPlayerBike.transform.position.z + (float)(i * 2 + 2)), Quaternion.identity);
				GameObject item2 = UnityEngine.Object.Instantiate<GameObject>(this.startStand.gameObject, new Vector3(this.startStand.transform.position.x, this.startStand.transform.position.y, this.startStand.transform.position.z + (float)(i * 2 + 2)), Quaternion.identity);
				this.startStandList.Add(item2);
				RaceManager._instance.currentAiBikes.Add(gameObject2);
				if (GameManager.cameFromMenu)
				{
					gameObject2.GetComponent<BikerDetails>().name = Multiplayerpage._instance.ais[i].name;
				}
				else
				{
					gameObject2.GetComponent<BikerDetails>().name = RaceManager._instance.aiPlayerNames[i];
				}
				gameObject2.GetComponent<BikerDetails>().icon = RaceManager._instance.aiIcons[i];
				gameObject2.GetComponent<BikerDetails>().distanceTxt = RaceManager._instance.distanceTxt[i];
				if (!this.isInsertedAleardy)
				{
					this.InsertPlayerRankImage();
				}
				SlotsHolder._instance.slotsHolder[i + 1].gameObject.SetActive(true);
				this.aiCount = 3;
			}
		}
		else if (this.PlayVs == GameManager.playVs.sixPlayers)
		{
			for (int j = 0; j < 5; j++)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.aiBikes[j].gameObject, new Vector3(this.currentPlayerBike.transform.position.x, this.currentPlayerBike.transform.position.y, this.currentPlayerBike.transform.position.z + (float)(j * 2 + 2)), Quaternion.identity);
				GameObject item3 = UnityEngine.Object.Instantiate<GameObject>(this.startStand.gameObject, new Vector3(this.startStand.transform.position.x, this.startStand.transform.position.y, this.startStand.transform.position.z + (float)(j * 2 + 2)), Quaternion.identity);
				this.startStandList.Add(item3);
				RaceManager._instance.currentAiBikes.Add(gameObject3);
				if (GameManager.cameFromMenu)
				{
					gameObject3.GetComponent<BikerDetails>().name = Multiplayerpage._instance.ais[j].name;
				}
				else
				{
					gameObject3.GetComponent<BikerDetails>().name = RaceManager._instance.aiPlayerNames[j];
				}
				gameObject3.GetComponent<BikerDetails>().icon = RaceManager._instance.aiIcons[j];
				gameObject3.GetComponent<BikerDetails>().distanceTxt = RaceManager._instance.distanceTxt[j];
				slotsParent.transform.localPosition = new Vector3(134f, slotsParent.transform.localPosition.y, slotsParent.transform.localPosition.z);
				if (!this.isInsertedAleardy)
				{
					this.InsertPlayerRankImage();
				}
				SlotsHolder._instance.slotsHolder[j + 1].gameObject.SetActive(true);
				this.aiCount = 5;
			}
		}
		else if (this.PlayVs == GameManager.playVs.onePlayer)
		{
			this.startStandList.Add(this.startStand);
			this.aiCount = 0;
		}
		RaceManager._instance.InsertAllRacersInList();
		this.Set2PlayerDifficulty();
	}

	private IEnumerator RaceCountDown(float waitTime)
	{
		yield return new WaitForSeconds(0.5f);
		this.raceCountDownTxt.text = "3";
		iTween.ScaleTo(this.raceCountDownTxt.gameObject, iTween.Hash(new object[]
		{
			"x",
			this.textSize,
			"y",
			this.textSize,
			"time",
			1,
			"easetype",
			iTween.EaseType.linear
		}));
		yield return new WaitForSeconds(0.1f);
		this.raceCountDownTxt.CrossFadeAlpha(1f, 0f, true);
		this.raceCountDownTxt.CrossFadeAlpha(0f, 1f, true);
		yield return new WaitForSeconds(1.1f);
		this.raceCountDownTxt.text = "2";
		iTween.ScaleTo(this.raceCountDownTxt.gameObject, iTween.Hash(new object[]
		{
			"x",
			1,
			"y",
			1,
			"time",
			0,
			"easetype",
			iTween.EaseType.linear
		}));
		iTween.ScaleTo(this.raceCountDownTxt.gameObject, iTween.Hash(new object[]
		{
			"x",
			this.textSize,
			"y",
			this.textSize,
			"time",
			1,
			"delay",
			0.1f,
			"easetype",
			iTween.EaseType.linear
		}));
		yield return new WaitForSeconds(0.1f);
		this.raceCountDownTxt.CrossFadeAlpha(1f, 0f, true);
		this.raceCountDownTxt.CrossFadeAlpha(0f, 1f, true);
		yield return new WaitForSeconds(1.1f);
		this.raceCountDownTxt.text = "1";
		iTween.ScaleTo(this.raceCountDownTxt.gameObject, iTween.Hash(new object[]
		{
			"x",
			1,
			"y",
			1,
			"time",
			0,
			"easetype",
			iTween.EaseType.linear
		}));
		iTween.ScaleTo(this.raceCountDownTxt.gameObject, iTween.Hash(new object[]
		{
			"x",
			this.textSize,
			"y",
			this.textSize,
			"time",
			1,
			"delay",
			0.1f,
			"easetype",
			iTween.EaseType.linear
		}));
		yield return new WaitForSeconds(0.1f);
		this.raceCountDownTxt.CrossFadeAlpha(1f, 0f, true);
		this.raceCountDownTxt.CrossFadeAlpha(0f, 1f, true);
		yield return new WaitForSeconds(1.1f);
		this.raceCountDownTxt.text = "GO";
		iTween.ScaleTo(this.raceCountDownTxt.gameObject, iTween.Hash(new object[]
		{
			"x",
			1,
			"y",
			1,
			"time",
			0,
			"easetype",
			iTween.EaseType.linear
		}));
		iTween.ScaleTo(this.raceCountDownTxt.gameObject, iTween.Hash(new object[]
		{
			"x",
			this.textSize,
			"y",
			this.textSize,
			"time",
			1,
			"delay",
			0.1f,
			"easetype",
			iTween.EaseType.linear
		}));
		yield return new WaitForSeconds(0.1f);
		this.raceCountDownTxt.CrossFadeAlpha(1f, 0f, true);
		this.raceCountDownTxt.CrossFadeAlpha(0f, 1f, true);
		yield return new WaitForSeconds(1.1f);
		for (int i = 0; i < this.startStandList.Count; i++)
		{
			this.startStand.transform.GetChild(0).GetComponent<Animator>().enabled = true;
			this.startStandList[i].transform.GetChild(0).GetComponent<Animator>().enabled = true;
		}
		this.raceCountDownPopup.SetActive(false);
		this.raceCountDownTxt.gameObject.SetActive(false);
		RaceManager._instance.StartRace();
		yield break;
	}

	public void HideIngameUI()
	{
		this.stuntBtn.transform.position = new Vector3(1000f, 1000f, 1000f);
		this.topBarUI.transform.position = new Vector3(0f, 1000f, 0f);
	}

	private void Set2PlayerDifficulty()
	{
		if (this.PlayVs == GameManager.playVs.twoPlayers)
		{
			if (this.levelNumber == 1 || this.levelNumber == 2 || this.levelNumber == 3)
			{
				RaceManager._instance.currentAiBikes[0].GetComponent<BikeController>().difficulty = BikeController.Difficulty.easy;
			}
			else if (this.levelNumber == 4 || this.levelNumber == 5 || this.levelNumber == 6)
			{
				RaceManager._instance.currentAiBikes[0].GetComponent<BikeController>().difficulty = BikeController.Difficulty.medium;
			}
			else if (this.levelNumber == 7 || this.levelNumber == 8 || this.levelNumber == 9 || this.levelNumber == 10)
			{
				RaceManager._instance.currentAiBikes[0].GetComponent<BikeController>().difficulty = BikeController.Difficulty.hard;
			}
			else
			{
				RaceManager._instance.currentAiBikes[0].GetComponent<BikeController>().difficulty = BikeController.Difficulty.extrahard;
			}
		}
	}

	public void PerformStunts()
	{
		this.currentPlayerBike.GetComponent<StuntController>().PerformRandomStunt();
	}

	public void PlayRaceWinSound()
	{
		if (this.playBg != null)
		{
			this.playBg.Stop();
			AudioClipManager.Instance.Play(3);
		}
	}

	public void PlayRaceLooseSound()
	{
		this.playBg.Stop();
		AudioClipManager.Instance.Play(4);
	}

	public void HelpContinueClick()
	{
		this.raceCountDownPopup.SetActive(true);
		base.StartCoroutine(this.RaceCountDown(0f));
		this.helpImg.SetActive(false);
		PlayerPrefs.SetString(GameManager.help, "false");
	}

	public void ShowLevelFail()
	{
	}

	public void Reset()
	{
		GameManager._instance.loading.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	public void HideEnvironment()
	{
		this.desertTheme.SetActive(false);
		this.greenTheme.SetActive(false);
	}

	public void EnableEnvironment()
	{
		if (LevelData._instance.theme == LevelData.Theme.desert)
		{
			this.desertTheme.SetActive(true);
		}
		else if (LevelData._instance.theme == LevelData.Theme.jungle)
		{
			this.greenTheme.SetActive(true);
		}
	}

	public static GameManager _instance;

	public GameObject raceCompletePage;

	public Button stuntBtn;

	public GameObject[] playerBikes;

	public GameObject[] aiBikes;

	public GameObject[] levels;

	public int levelNumber;

	public int bikeNumber;

	public GameObject myLevel;

	public GameObject currentPlayerBike;

	public Text raceCountDownTxt;

	public GameObject raceCountDownPopup;

	public int[] levelReward;

	public GameObject loading;

	private BikeController myBikeController;

	public GameObject desertTheme;

	public GameObject greenTheme;

	public GameObject snowTheme;

	public GameObject snowParticleEffect;

	public GameObject startStand;

	public List<GameObject> startStandList = new List<GameObject>();

	public static bool cameFromMenu;

	public GameObject topBarUI;

	public GameObject fireEffect;

	public GameObject resettingImg;

	public GameObject helpImg;

	public int aiCount;

	public GameManager.playVs PlayVs;

	public AudioClip playBgSound;

	public AudioClip raceWinClip;

	public AudioClip racelooseClip;

	public AudioSource playBg;

	public static string help = "help";

	private bool isInsertedAleardy;

	private float textSize = 5f;

	private int[] easycontrollsarray = new int[]
	{
		1,
		2,
		3
	};

	private int[] mediumcontrollsarray = new int[]
	{
		4,
		5,
		6
	};

	private int[] hardcontrollsarray = new int[]
	{
		7,
		8,
		9,
		10
	};

	public enum playVs
	{
		onePlayer,
		twoPlayers,
		fourPlayers,
		sixPlayers
	}
}
