using System;
using System.Collections;
using UnityEngine;

public class AudioClipManager : MonoBehaviour
{
	public static AudioClipManager Instance
	{
		get
		{
			if (AudioClipManager._instance == null)
			{
				AudioClipManager._instance = UnityEngine.Object.FindObjectOfType<AudioClipManager>();
			}
			return AudioClipManager._instance;
		}
	}

	public void Play(MenuSounds menuSound)
	{
		switch (menuSound)
		{
		case MenuSounds.BG:
			if (this.canPlay((int)menuSound))
			{
				BGSoundManager.Instance.PlayAudioClip(this.Sounds[(int)menuSound]);
			}
			return;
		case MenuSounds.LoadingIn:
			if (this.canPlay((int)menuSound))
			{
				SoundManager.Instance.PlayAudioClip(this.Sounds[(int)menuSound], 1f);
			}
			return;
		case MenuSounds.LoadingOut:
			if (this.canPlay((int)menuSound))
			{
				SoundManager.Instance.PlayAudioClip(this.Sounds[(int)menuSound], 1f);
			}
			return;
		}
		if (this.canPlay((int)menuSound))
		{
			SoundManager.Instance.PlayAudioClip(this.Sounds[(int)menuSound], 1f);
		}
	}

	public void Play(InGameSounds soundIndex)
	{
		if (soundIndex != InGameSounds.BG)
		{
			if (this.canPlay((int)soundIndex))
			{
				if (soundIndex != InGameSounds.BG)
				{
					if (soundIndex != InGameSounds.LevelComplete)
					{
						SoundManager.Instance.PlayAudioClip(this.Sounds[(int)soundIndex], 1f);
					}
					else
					{
						SoundManager.Instance.PlayAudioClip(this.Sounds[(int)soundIndex], 1f);
					}
				}
				else
				{
					SoundManager.Instance.PlayAudioClip(this.Sounds[(int)soundIndex], 1f);
				}
			}
		}
		else if (this.canPlay((int)soundIndex))
		{
			BGSoundManager.Instance.PlayAudioClip(this.Sounds[(int)soundIndex]);
		}
	}

	private IEnumerator PlaywithDelay(int soundIndex)
	{
		yield return new WaitForSeconds(0f);
		SoundManager.Instance.PlayAudioClip(this.Sounds[soundIndex], 0.5f);
		yield break;
	}

	public void Play(int soundIndex)
	{
		if (soundIndex != 0)
		{
			if (soundIndex != 4)
			{
				if (this.canPlay(soundIndex))
				{
					SoundManager.Instance.PlayAudioClip(this.Sounds[soundIndex], 1f);
				}
			}
			else if (this.canPlay(soundIndex))
			{
				SoundManager.Instance.PlayAudioClip(this.Sounds[soundIndex], 1f);
			}
		}
		else if (this.canPlay(soundIndex))
		{
			BGSoundManager.Instance.PlayAudioClip(this.Sounds[soundIndex]);
		}
	}

	public IEnumerator BGvolumeChanger(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		BGSoundManager.Instance.SetVolume(100f);
		yield break;
	}

	private bool canPlay(int index)
	{
		return index < this.Sounds.Length && this.Sounds[index] != null;
	}

	public AudioClip[] Sounds;

	private static AudioClipManager _instance;
}
