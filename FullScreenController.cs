using System;
using UnityEngine;

public class FullScreenController : MonoBehaviour
{
	private void Awake()
	{
		if (PlayerPrefs.GetInt("EnableFullScreen", 0) == 0)
		{
			Screen.fullScreen = false;
			base.Invoke("ShowFullScreen", 1f);
		}
	}

	private void ShowFullScreen()
	{
		Screen.fullScreen = true;
		PlayerPrefs.SetInt("EnableFullScreen", 1);
	}
}
