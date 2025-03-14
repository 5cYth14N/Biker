using System;
using UnityEngine;

public class LoopSoundManager : MonoBehaviour
{
	public static LoopSoundManager Instance
	{
		get
		{
			if (LoopSoundManager._instance == null)
			{
				LoopSoundManager._instance = UnityEngine.Object.FindObjectOfType<LoopSoundManager>();
			}
			if (LoopSoundManager._instance == null)
			{
				GameObject gameObject = new GameObject("LoopSoundManager");
				LoopSoundManager._instance = gameObject.AddComponent<LoopSoundManager>();
			}
			return LoopSoundManager._instance;
		}
	}

	private void Awake()
	{
		if (!base.GetComponent<AudioSource>())
		{
			base.gameObject.AddComponent<AudioSource>();
		}
		base.GetComponent<AudioSource>().loop = true;
	}

	public void Pause()
	{
		if (base.GetComponent<AudioSource>().isPlaying)
		{
			this.isPauseCalled = true;
			base.GetComponent<AudioSource>().Pause();
		}
	}

	public void Resume()
	{
		this.isPauseCalled = false;
		if (!base.GetComponent<AudioSource>().isPlaying)
		{
			base.GetComponent<AudioSource>().Play();
		}
	}

	public void Playclip(AudioClip clip)
	{
		if (this.isPauseCalled && base.GetComponent<AudioSource>().clip)
		{
			this.isPauseCalled = false;
			base.GetComponent<AudioSource>().Play();
			return;
		}
		if (base.GetComponent<AudioSource>().isPlaying)
		{
			base.GetComponent<AudioSource>().Stop();
		}
		if (clip)
		{
			base.GetComponent<AudioSource>().clip = clip;
			base.GetComponent<AudioSource>().Play();
		}
	}

	public void Stop()
	{
		this.isPauseCalled = false;
		if (base.GetComponent<AudioSource>().isPlaying)
		{
			base.GetComponent<AudioSource>().Stop();
		}
	}

	public bool IsMute
	{
		get
		{
			return base.GetComponent<AudioSource>().mute;
		}
		set
		{
			base.GetComponent<AudioSource>().mute = value;
		}
	}

	public void SetVolume(float percent)
	{
		base.GetComponent<AudioSource>().volume = percent / 100f;
	}

	private static LoopSoundManager _instance;

	private bool isPauseCalled;
}
