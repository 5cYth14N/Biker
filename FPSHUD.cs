using System;
using UnityEngine;
using UnityEngine.UI;

public class FPSHUD : MonoBehaviour
{
	private void Awake()
	{
		base.useGUILayout = false;
	}

	private void Start()
	{
		if (!base.GetComponent<Text>())
		{
			UnityEngine.Debug.Log("UtilityFramesPerSecond needs a GUIText component!");
			base.enabled = false;
			return;
		}
		this.timeleft = this.updateInterval;
	}

	private void Update()
	{
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
		if ((double)this.timeleft <= 0.0)
		{
			float num = this.accum / (float)this.frames;
			string text = string.Format("{0:F0} FPS", num);
			base.GetComponent<Text>().text = text;
			if (num < 30f)
			{
				base.GetComponent<Text>().color = Color.red;
			}
			else
			{
				if (num < 10f)
				{
					base.GetComponent<Text>().color = Color.red;
				}
				else
				{
					base.GetComponent<Text>().color = Color.black;
				}
				this.timeleft = this.updateInterval;
				this.accum = 0f;
				this.frames = 0;
			}
		}
	}

	public float updateInterval = 0.5f;

	private float accum;

	private int frames;

	private float timeleft;
}
