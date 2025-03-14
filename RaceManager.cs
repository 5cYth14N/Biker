using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
	private void Awake()
	{
		RaceManager._instance = this;
		this.isRaceStarted = false;
		this.isRaceEnd = false;
	}

	private void Start()
	{
	}

	public void TweenOutSLotParent()
	{
		iTween.MoveTo(this.slotsParent.gameObject, iTween.Hash(new object[]
		{
			"y",
			500,
			"time",
			0.2f,
			"islocal",
			true,
			"easetype",
			iTween.EaseType.linear
		}));
	}

	public void InsertAllRacersInList()
	{
		for (int i = 0; i < this.currentAiBikes.Count; i++)
		{
			this.allRacers.Add(this.currentAiBikes[i]);
		}
		this.allRacers.Insert(0, GameManager._instance.currentPlayerBike);
		RankManager.instance.InsertNamesInTextField();
		this.myPlayer = GameManager._instance.currentPlayerBike.GetComponent<BikerDetails>();
	}

	public void EndRace()
	{
		BikeController[] array = UnityEngine.Object.FindObjectsOfType(typeof(BikeController)) as BikeController[];
		foreach (BikeController bikeController in array)
		{
			if (bikeController.GetComponent<BikeController>().controltype == BikeController.controlType.PLAYER)
			{
				UnityEngine.Debug.Log("player win");
				RaceManager._instance.isRaceStarted = false;
			}
			else if (bikeController.GetComponent<BikeController>().controltype == BikeController.controlType.AI)
			{
				UnityEngine.Debug.Log("ai win");
				RaceManager._instance.isRaceStarted = false;
			}
		}
	}

	public void StartRace()
	{
		BikeController[] array = UnityEngine.Object.FindObjectsOfType(typeof(BikeController)) as BikeController[];
		foreach (BikeController bikeController in array)
		{
			bikeController.enableControlls = true;
			this.isRaceStarted = true;
		}
		this.racer = (UnityEngine.Object.FindObjectsOfType(typeof(BikerDetails)) as BikerDetails[]);
		base.InvokeRepeating("CheckRankingUpdate", 0f, 0.5f);
	}

	private void CheckRankingUpdate()
	{
		foreach (BikerDetails bikerDetails in this.racer)
		{
			RankManager.instance.SetRank(new Player
			{
				distance = bikerDetails.distance,
				targetDistance = 0f,
				name = bikerDetails.name
			});
			for (int j = 0; j < RankManager.instance.txtRanks.Length; j++)
			{
				Image component = RankManager.instance.txtRanks[j].transform.parent.transform.GetChild(1).GetComponent<Image>();
				component.sprite = bikerDetails.icon;
			}
			if (bikerDetails.name == this.myPlayer.name)
			{
				for (int k = 0; k < RankManager.instance.txtRanks.Length; k++)
				{
					if (RankManager.instance.txtRanks[k].text == k + 1 + " . " + this.myPlayer.name)
					{
						RankManager.instance.txtRanks[k].transform.parent.GetComponent<Image>().color = new Color32(8, 100, 0, byte.MaxValue);
					}
					else
					{
						RankManager.instance.txtRanks[k].transform.parent.GetComponent<Image>().color = new Color(0f, 0f, 0f, 255f);
					}
				}
			}
		}
	}

	public static RaceManager _instance;

	public Sprite[] aiIcons;

	public string[] aiPlayerNames = new string[]
	{
		"amar",
		"akbar",
		"anthony",
		"john",
		"janardhan"
	};

	public List<GameObject> currentAiBikes = new List<GameObject>();

	public List<string> rank = new List<string>();

	public List<float> racersTimeOfComplete = new List<float>();

	public bool isRaceStarted;

	public bool isRaceEnd;

	public Text[] distanceTxt;

	public int targetDistance;

	public List<GameObject> allRacers = new List<GameObject>();

	public GameObject slotsParent;

	private BikerDetails myPlayer;

	private Text myPlayerSlotTxt;

	private Image myPlayerSlot;

	private BikerDetails[] racer;
}
