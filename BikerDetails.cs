using System;
using UnityEngine;
using UnityEngine.UI;

public class BikerDetails : MonoBehaviour
{
	private void Start()
	{
		if (this.iconHolder != null)
		{
			this.TweenIconZero();
		}
		base.Invoke("ShowNamesIcon", 5f);
	}

	private void ShowNamesIcon()
	{
		if (this.meshText != null)
		{
			this.meshText.text = this.name;
		}
	}

	private void TweenIconZero()
	{
		iTween.ScaleTo(this.iconHolder.gameObject, iTween.Hash(new object[]
		{
			"x",
			0,
			"y",
			0,
			"time",
			0.2f,
			"easetype",
			iTween.EaseType.linear
		}));
		float time = UnityEngine.Random.Range(5f, 15f);
		base.Invoke("TweenIconOne", time);
	}

	private void TweenIconOne()
	{
		iTween.ScaleTo(this.iconHolder.gameObject, iTween.Hash(new object[]
		{
			"x",
			0.3f,
			"y",
			0.3f,
			"time",
			0.2f,
			"easetype",
			iTween.EaseType.linear
		}));
		base.Invoke("TweenIconZero", 2f);
		this.canShowIcon = true;
	}

	private void Update()
	{
		if (!RaceManager._instance.isRaceStarted)
		{
			return;
		}
		if (!this.stopCurrentTime)
		{
			this.currentTime += Time.deltaTime;
		}
		this.distance = Vector3.Distance(base.transform.position, LevelData._instance.finishPoint.transform.position);
		this.distanceTxt.text = this.name + " : " + this.distance.ToString("F0");
	}

	public Sprite icon;

	public new string name;

	public GameObject iconHolder;

	public float distance;

	public Text distanceTxt;

	public bool canShowIcon;

	public TextMesh meshText;

	public float currentTime;

	public bool stopCurrentTime;
}
