using System;
using UnityEngine;

public class IngameButtonEffect : MonoBehaviour
{
	private void OnMouseDown()
	{
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"x",
			0.6,
			"y",
			0.6,
			"time",
			0.1f,
			"easetype",
			iTween.EaseType.spring
		}));
	}

	private void OnMouseUp()
	{
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"x",
			0.8,
			"y",
			0.8,
			"time",
			0.1f,
			"easetype",
			iTween.EaseType.spring
		}));
	}
}
