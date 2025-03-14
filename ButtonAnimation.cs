using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	public void OnPointerDown(PointerEventData eventData)
	{
		this._pressed = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		this._pressed = false;
	}

	private void Update()
	{
		if (this._pressed)
		{
			if (this.mb_btnDown)
			{
				return;
			}
			this.ButtonDownAnim();
		}
		else
		{
			if (!this.mb_btnDown)
			{
				return;
			}
			this.ButtonUpAnim();
		}
		eMovementDirection direction = this.Direction;
		if (direction != eMovementDirection.Up)
		{
		}
	}

	public void ButtonDownAnim()
	{
		this.mb_btnDown = true;
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"x",
			0.9,
			"y",
			0.9,
			"time",
			0.1f,
			"eastype",
			this.effectType,
			"IgnoreTimeScale",
			true
		}));
	}

	public void ButtonUpAnim()
	{
		this.mb_btnDown = false;
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"x",
			1,
			"y",
			1,
			"time",
			0f,
			"eastype",
			this.effectType,
			"IgnoreTimeScale",
			true
		}));
	}

	public iTween.EaseType effectType;

	public eMovementDirection Direction;

	private bool _pressed;

	private bool mb_btnDown;
}
