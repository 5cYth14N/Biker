using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHCT : MonoBehaviour
{
	private void Start()
	{
		SettingsHCT._instance = this;
		this.CheckSound();
	}

	private void OnEnable()
	{
		this.UpdateCoinText();
		if (PageHandler._instance)
		{
			PageHandler._instance.pageType = PageHandler.pageTypeEnum.settings;
		}
	}

	public void UpdateCoinText()
	{
		this.coinsText.text = PlayerPrefs.GetInt(GameEnum.totalCoins).ToString();
	}

	public void CheckSound()
	{
		if (this.soundValue == 0)
		{
			this.soundBtn.sprite = this.soundOff;
			UnityEngine.Debug.Log("SoundOff");
			AudioListener.volume = 0f;
			this.soundValue = 1;
		}
		else if (this.soundValue == 1)
		{
			this.soundBtn.sprite = this.soundOn;
			UnityEngine.Debug.Log("SoundOn");
			AudioListener.volume = 1f;
			this.soundValue = 0;
		}
		UnityEngine.Debug.Log("Value__" + this.soundValue);
	}

	public static SettingsHCT _instance;

	public Text coinsText;

	private int soundValue = 1;

	public Image soundBtn;

	public Sprite soundOn;

	public Sprite soundOff;
}
