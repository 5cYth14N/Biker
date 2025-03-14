using System;
using UnityEngine;

public class BGSoundManager : MonoBehaviour
{
	public static BGSoundManager Instance
	{
		get
		{
			if (BGSoundManager._instance == null)
			{
				BGSoundManager._instance = UnityEngine.Object.FindObjectOfType<BGSoundManager>();
			}
			if (BGSoundManager._instance == null)
			{
				GameObject gameObject = new GameObject("BGSoundManager");
				BGSoundManager._instance = gameObject.AddComponent<BGSoundManager>();
			}
			return BGSoundManager._instance;
		}
	}

	private void Awake()
	{
		if (!base.GetComponent<AudioSource>())
		{
			base.gameObject.AddComponent<AudioSource>();
		}
		this.SetVolume(100f);
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	public void PlayAudioClip(AudioClip audioClip)
	{
		if (base.GetComponent<AudioSource>().isPlaying)
		{
			base.GetComponent<AudioSource>().Stop();
		}
		base.GetComponent<AudioSource>().clip = audioClip;
		base.GetComponent<AudioSource>().loop = true;
		base.GetComponent<AudioSource>().Play();
	}

	public void StopPlaying()
	{
		if (base.GetComponent<AudioSource>().isPlaying)
		{
			base.GetComponent<AudioSource>().Stop();
		}
	}

	public void SetVolume(float percent)
	{
		base.GetComponent<AudioSource>().volume = percent * 0.01f;
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

	public void ResumeSound()
	{
		base.GetComponent<AudioSource>().Play();
	}

	public void PauseSound()
	{
		base.GetComponent<AudioSource>().Pause();
	}

	private static BGSoundManager _instance;
}
