using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelFail : MonoBehaviour
{
	private void OnEnable()
	{
		LevelFail.isLevelFail = true;
		if (GameManager._instance.loading)
		{
			GameManager._instance.loading.SetActive(false);
		}
	}

	private void Start()
	{
		LevelFail._instance = this;
		this.UpdateCoinText();
	}

	public void UpdateCoinText()
	{
		this.totalCoinsText.text = PlayerPrefs.GetInt(GameEnum.totalCoins).ToString();
	}

	public static LevelFail _instance;

	public Text totalCoinsText;

	public static bool isLevelFail;
}
