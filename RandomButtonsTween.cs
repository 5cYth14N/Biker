using System;
using UnityEngine;

public class RandomButtonsTween : MonoBehaviour
{
	private void OnEnable()
	{
		this.randomNum = UnityEngine.Random.Range(0, this.buttons.Length);
		iTween.ScaleTo(this.buttons[this.randomNum], iTween.Hash(new object[]
		{
			"x",
			1.1,
			"y",
			1.1,
			"time",
			0.35f,
			"looptype",
			iTween.LoopType.pingPong,
			"easetype",
			iTween.EaseType.easeOutSine
		}));
	}

	private void OnDisable()
	{
		iTween.Stop(this.buttons[this.randomNum].gameObject);
		this.buttons[this.randomNum].transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public GameObject[] buttons;

	private int randomNum;
}
