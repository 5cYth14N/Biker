using System;
using UnityEngine;

public class JoyStickTest : MonoBehaviour
{
	private void Awake()
	{
		if (this.isNone)
		{
			return;
		}
		if (JoyStickTest.joystickActivated)
		{
			if (this.isJouStick)
			{
				base.gameObject.SetActive(true);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
		else if (this.isJouStick)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			base.gameObject.SetActive(true);
		}
	}

	private void OnMouseDown()
	{
		if (this.isJouStick)
		{
			return;
		}
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"x",
			1,
			"y",
			1,
			"time",
			0.1f,
			"easetype",
			iTween.EaseType.spring
		}));
	}

	private void OnMouseUp()
	{
		if (this.isJouStick)
		{
			return;
		}
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

	public static bool joystickActivated;

	public bool isJouStick;

	public bool isLean;

	public bool isNone;
}
