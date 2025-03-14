using System;
using UnityEngine;
using UnityEngine.UI;

public class CrossFade : MonoBehaviour
{
	private void OnEnable()
	{
		base.Invoke("FadeIn", this.delayTime);
		if (base.gameObject.GetComponent<Text>())
		{
			this.thisText.CrossFadeAlpha(0f, 0f, true);
		}
		else
		{
			this.thisImg.CrossFadeAlpha(0f, 0f, true);
		}
	}

	private void Awake()
	{
		this.thisImg = base.GetComponent<Image>();
		this.thisText = base.GetComponent<Text>();
	}

	private void FadeIn()
	{
		if (base.gameObject.GetComponent<Text>())
		{
			this.thisText.CrossFadeAlpha(1f, this.duration, true);
		}
		else
		{
			this.thisImg.CrossFadeAlpha(1f, this.duration, true);
		}
		if (this.loop)
		{
			base.Invoke("FadeOut", this.waitTime);
		}
	}

	private void FadeOut()
	{
		if (base.gameObject.GetComponent<Text>())
		{
			this.thisText.CrossFadeAlpha(0f, this.duration, true);
		}
		else
		{
			this.thisImg.CrossFadeAlpha(0f, this.duration, true);
		}
		base.Invoke("FadeIn", this.duration);
	}

	private void OnDisable()
	{
		if (base.gameObject.GetComponent<Text>())
		{
			this.thisText.CrossFadeAlpha(1f, this.duration, true);
		}
		else
		{
			this.thisImg.CrossFadeAlpha(1f, this.duration, true);
		}
	}

	public float duration = 0.5f;

	public float delayTime = 0.5f;

	private Image thisImg;

	private Text thisText;

	public float waitTime = 1f;

	public bool loop;
}
