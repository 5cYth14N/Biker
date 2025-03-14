using System;
using UnityEngine;

public class MoveMe : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnEnable()
	{
		this.TweenIn();
	}

	private void TweenIn()
	{
		for (int i = 0; i < this.button.Length; i++)
		{
			if (this.tweenTypee == MoveMe.TweenType.movable && this.axis == MoveMe.MoveAxis.X)
			{
				iTween.MoveFrom(this.button[i].gameObject, iTween.Hash(new object[]
				{
					"x",
					this.x,
					"time",
					this.time,
					"delay",
					(double)(i + 1) * 0.2,
					"islocal",
					true,
					"easetype",
					this.easetype
				}));
			}
			if (this.tweenTypee == MoveMe.TweenType.movable && this.axis == MoveMe.MoveAxis.Y)
			{
				iTween.MoveFrom(this.button[i].gameObject, iTween.Hash(new object[]
				{
					"y",
					this.y,
					"time",
					this.time,
					"delay",
					(double)(i + 1) * 0.2,
					"islocal",
					true,
					"easetype",
					this.easetype
				}));
			}
			if (this.tweenTypee == MoveMe.TweenType.scalable)
			{
				iTween.ScaleFrom(this.button[i].gameObject, iTween.Hash(new object[]
				{
					"x",
					0,
					"y",
					0,
					"time",
					this.time,
					"delay",
					(double)(i + 1) * 0.2,
					"easetype",
					this.easetype
				}));
			}
		}
	}

	private void OnDisable()
	{
		UnityEngine.Debug.Log("OnDisable");
		if (this.tweenTypee == MoveMe.TweenType.movable && this.axis == MoveMe.MoveAxis.X)
		{
			UnityEngine.Debug.Log("axisIs" + this.axis);
			this.button[0].transform.localPosition = new Vector3((float)this.x, 0f, 0f);
			this.button[0].transform.localPosition = new Vector3(0f, (float)this.y, 0f);
		}
	}

	public MoveMe.TweenType tweenTypee;

	public MoveMe.MoveAxis axis;

	public int x;

	public int y;

	public float time;

	public GameObject[] button;

	public iTween.EaseType easetype;

	public enum TweenType
	{
		scalable,
		movable
	}

	public enum MoveAxis
	{
		X,
		Y
	}
}
