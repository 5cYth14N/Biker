using System;
using UnityEngine;

public class ScaleUI : MonoBehaviour
{
	private void OnEnable()
	{
		for (int i = 0; i < this.buttons.Length; i++)
		{
			iTween.ScaleFrom(this.buttons[i].gameObject, iTween.Hash(new object[]
			{
				"x",
				0,
				"y",
				0,
				"time",
				this.time,
				"ignoretimescale",
				true,
				"islocal",
				true,
				"delay",
				((float)i + this.delay) * this.buttonQueDelay,
				"easetype",
				this.easetype
			}));
		}
	}

	public GameObject[] buttons;

	public float time = 0.5f;

	public float delay = 1f;

	public float buttonQueDelay = 0.2f;

	public iTween.EaseType easetype = iTween.EaseType.easeOutBack;
}
