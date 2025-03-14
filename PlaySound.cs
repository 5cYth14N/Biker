using System;
using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class PlaySound : MonoBehaviour
{
	private void OnEnable()
	{
		SoundManager.SetVolumeEvent += this.HandleSetVolumeEvent;
		SoundManager.MuteEvent += this.HandleMuteEvent;
	}

	private void OnDisable()
	{
		SoundManager.SetVolumeEvent -= this.HandleSetVolumeEvent;
		SoundManager.MuteEvent -= this.HandleMuteEvent;
	}

	private void OnDestroy()
	{
		SoundManager.SetVolumeEvent -= this.HandleSetVolumeEvent;
		SoundManager.MuteEvent -= this.HandleMuteEvent;
	}

	private void HandleMuteEvent(bool isMute)
	{
		base.GetComponent<AudioSource>().mute = isMute;
	}

	private void HandleSetVolumeEvent(float percent)
	{
		base.GetComponent<AudioSource>().volume = percent;
	}

	public IEnumerator PlayClip(AudioClip audioClip, float volume)
	{
		this.isPlayingClip = true;
		base.GetComponent<AudioSource>().volume = volume;
		base.GetComponent<AudioSource>().PlayOneShot(audioClip);
		yield return new WaitForSeconds(audioClip.length);
		this.isPlayingClip = false;
		yield break;
	}

	public void StopPlaying()
	{
		base.GetComponent<AudioSource>().Stop();
	}

	[HideInInspector]
	public bool isPlayingClip;
}
