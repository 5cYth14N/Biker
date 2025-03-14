using System;
using UnityEngine;

public class LevelData : MonoBehaviour
{
	private void Awake()
	{
		LevelData._instance = this;
	}

	private void Start()
	{
		if (this.theme == LevelData.Theme.desert)
		{
			GameManager._instance.greenTheme.SetActive(false);
			GameManager._instance.snowTheme.SetActive(false);
			GameManager._instance.desertTheme.SetActive(true);
		}
		else if (this.theme == LevelData.Theme.jungle)
		{
			GameManager._instance.greenTheme.SetActive(true);
			GameManager._instance.desertTheme.SetActive(false);
			GameManager._instance.snowTheme.SetActive(false);
		}
		else if (this.theme == LevelData.Theme.snow)
		{
			GameManager._instance.greenTheme.SetActive(false);
			GameManager._instance.desertTheme.SetActive(false);
			GameManager._instance.snowTheme.SetActive(true);
			GameManager._instance.snowParticleEffect.SetActive(true);
		}
	}

	private void Update()
	{
	}

	public static LevelData _instance;

	public GameObject finishPoint;

	public LevelData.Theme theme;

	public enum Theme
	{
		desert,
		jungle,
		snow
	}
}
