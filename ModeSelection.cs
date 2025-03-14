using System;
using UnityEngine;

public class ModeSelection : MonoBehaviour
{
	private void Awake()
	{
		ModeSelection._instance = this;
	}

	public void TwoPlayers()
	{
		this.noOfRacers = ModeSelection.NoOfRacers.two;
		PageHandler._instance.Open_Upgrade();
		AudioClipManager.Instance.Play(1);
	}

	public void FourPlayers()
	{
		this.noOfRacers = ModeSelection.NoOfRacers.four;
		PageHandler._instance.Open_Upgrade();
		AudioClipManager.Instance.Play(1);
	}

	public void SixPlayers()
	{
		this.noOfRacers = ModeSelection.NoOfRacers.six;
		PageHandler._instance.Open_Upgrade();
		AudioClipManager.Instance.Play(1);
	}

	public static ModeSelection _instance;

	public ModeSelection.NoOfRacers noOfRacers;

	public enum NoOfRacers
	{
		two,
		four,
		six
	}
}
