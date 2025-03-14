using System;
using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{
	private void Awake()
	{
		LevelCompleteTrigger._instance = this;
	}

	private void Start()
	{
		this.mainCam = Camera.main.transform;
	}

	private void EnableParticleEffect()
	{
		if (this.particleEffs.Length == 0)
		{
			return;
		}
		for (int i = 0; i < this.particleEffs.Length; i++)
		{
			this.particleEffs[i].SetActive(true);
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.transform.CompareTag("Player") || col.transform.CompareTag("Opponent"))
		{
			RaceManager._instance.rank.Add(col.GetComponent<BikerDetails>().name);
			RaceManager._instance.racersTimeOfComplete.Add(col.GetComponent<BikerDetails>().currentTime);
			col.GetComponent<BikeController>().enableControlls = false;
			col.GetComponent<BikeController>().reachedFinish = true;
			this.triggeredCount++;
			UnityEngine.Debug.Log(" racer count who triggered to finish collider : " + this.triggeredCount);
			RaceManager._instance.isRaceEnd = true;
			col.transform.GetComponent<BikerDetails>().stopCurrentTime = true;
		}
		if (col.transform.CompareTag("Player") && col.GetComponent<BikeController>().controltype == BikeController.controlType.PLAYER)
		{
			this.myRank = RaceManager._instance.rank.Count;
			UnityEngine.Debug.LogError("my rank : " + this.myRank);
			this.mainCam.parent.GetComponent<SmoothFollow>().setPlyaerBike = false;
			this.mainCam.parent.GetComponent<SmoothFollow>().playerBike = null;
			this.CamTween();
			RaceManager._instance.TweenOutSLotParent();
			base.Invoke("ShowLevelComplete", 1f);
			GameManager._instance.PlayRaceWinSound();
			this.EnableParticleEffect();
		}
	}

	private void ShowLevelComplete()
	{
		GameManager._instance.raceCompletePage.SetActive(true);
		GameManager._instance.HideIngameUI();
		GameManager._instance.currentPlayerBike.GetComponent<BikeController>().UICamera.gameObject.SetActive(false);
	}

	private void CamTween()
	{
		iTween.MoveTo(this.mainCam.gameObject, iTween.Hash(new object[]
		{
			"position",
			this.finishCamPos.position,
			"time",
			1,
			"easetype",
			iTween.EaseType.linear
		}));
	}

	public Transform finishCamPos;

	private Transform mainCam;

	public static LevelCompleteTrigger _instance;

	public GameObject[] particleEffs;

	public int triggeredCount;

	public int myRank;
}
