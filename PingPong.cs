using System;
using UnityEngine;

public class PingPong : MonoBehaviour
{
	private void Start()
	{
		base.Invoke("TweenButton", this.startAfter);
	}

	private void TweenButton()
	{
		iTween.ScaleBy(base.gameObject, iTween.Hash(new object[]
		{
			"x",
			this.X,
			"y",
			this.Y,
			"time",
			this.time,
			"loopType",
			iTween.LoopType.pingPong,
			"islocal",
			true,
			"easeType",
			this.effectType
		}));
	}

	public float X = 1.3f;

	public float Y = 1.3f;

	public float time = 0.3f;

	public iTween.EaseType effectType;

	public float startAfter;
}
