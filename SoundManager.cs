using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static event SoundManager.SetVolumeDelegate SetVolumeEvent;

	public static event SoundManager.MuteDelegate MuteEvent;

	public static SoundManager Instance
	{
		get
		{
			if (SoundManager._instance == null)
			{
				SoundManager._instance = UnityEngine.Object.FindObjectOfType<SoundManager>();
			}
			if (SoundManager._instance == null)
			{
				GameObject gameObject = new GameObject("SoundManager");
				SoundManager._instance = gameObject.AddComponent<SoundManager>();
			}
			return SoundManager._instance;
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(this);
		if (this.AudioSources == null)
		{
			this.AudioSources = new List<PlaySound>();
		}
	}

	private PlaySound CreateAudioSource()
	{
		AudioSource audioSource;
		if (this.CustomAudioSourcePrefab)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.CustomAudioSourcePrefab);
			audioSource = gameObject.GetComponent<AudioSource>();
		}
		else
		{
			GameObject gameObject2 = new GameObject();
			audioSource = gameObject2.AddComponent<AudioSource>();
		}
		audioSource.transform.parent = base.transform;
		PlaySound playSound = audioSource.gameObject.AddComponent<PlaySound>();
		audioSource.mute = this.IsMute;
		audioSource.volume = 1f;
		this.AudioSources.Add(playSound);
		audioSource.name = "AudioSource" + this.AudioSources.Count;
		return playSound;
	}

	public void PlayAudioClip(AudioClip clip, float volume = 1f)
	{
		if (!clip)
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < this.AudioSources.Count; i++)
		{
			if (!this.AudioSources[i].isPlayingClip)
			{
				flag = true;
				base.StartCoroutine(this.AudioSources[i].PlayClip(clip, volume));
				break;
			}
		}
		if (!flag)
		{
			UnityEngine.Debug.Log("Creating new AudioSource");
			base.StartCoroutine(this.CreateAudioSource().PlayClip(clip, volume));
		}
	}

	public void PlayAudioClip(int clipNo)
	{
		this.PlayAudioClip(this.Clips[clipNo], 1f);
	}

	public void PlayAudioClip(GameObject customAudioSourcePrefab, AudioClip clip)
	{
		this.CustomAudioSourcePrefab = customAudioSourcePrefab;
		this.PlayAudioClip(clip, 1f);
	}

	public void PlayAudioClip(GameObject customAudioSourcePrefab, int clipNo)
	{
		this.CustomAudioSourcePrefab = customAudioSourcePrefab;
		this.PlayAudioClip(clipNo);
	}

	public void SetVolume(float percent)
	{
		if (SoundManager.SetVolumeEvent != null)
		{
			SoundManager.SetVolumeEvent(percent * 0.01f);
		}
	}

	public bool IsMute
	{
		get
		{
			return this.isMute;
		}
		set
		{
			this.isMute = value;
			if (SoundManager.MuteEvent != null)
			{
				SoundManager.MuteEvent(value);
			}
		}
	}

	private void OnLevelWasLoaded()
	{
		for (int i = 0; i < this.AudioSources.Count; i++)
		{
			this.AudioSources[i].StopPlaying();
		}
	}

	[HideInInspector]
	public int AudioSourcesLength = 1;

	public GameObject CustomAudioSourcePrefab;

	public List<AudioClip> Clips;

	private List<PlaySound> AudioSources;

	private bool isMute;

	private static SoundManager _instance;

	public delegate void SetVolumeDelegate(float percent);

	public delegate void MuteDelegate(bool isMute);
}
