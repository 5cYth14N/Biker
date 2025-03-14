using System;
using UnityEngine;

public class SETTINGUTILS : MonoBehaviour
{
	public static void GetSaved_MusicStatus()
	{
		if (PlayerPrefs.HasKey(SETTINGUTILS.musicKEY))
		{
			if (PlayerPrefs.GetInt(SETTINGUTILS.musicKEY) == 1)
			{
				SETTINGUTILS.music = true;
			}
			else
			{
				SETTINGUTILS.music = false;
			}
		}
		else
		{
			SETTINGUTILS.music = true;
			SETTINGUTILS.saveMusicKey();
		}
		SETTINGUTILS.SetSoundStatus();
	}

	public static void GetSaved_SoundStatus()
	{
		if (PlayerPrefs.HasKey(SETTINGUTILS.soundKEY))
		{
			if (PlayerPrefs.GetInt(SETTINGUTILS.soundKEY) == 1)
			{
				SETTINGUTILS.sound = true;
			}
			else
			{
				SETTINGUTILS.sound = false;
			}
		}
		else
		{
			SETTINGUTILS.sound = true;
			SETTINGUTILS.saveSoundKey();
		}
		SETTINGUTILS.SetSoundStatus();
	}

	public static void saveMusicKey()
	{
		if (SETTINGUTILS.music)
		{
			PlayerPrefs.SetInt(SETTINGUTILS.musicKEY, 1);
		}
		else
		{
			PlayerPrefs.SetInt(SETTINGUTILS.musicKEY, 0);
		}
		SETTINGUTILS.SetSoundStatus();
	}

	public static void saveSoundKey()
	{
		if (SETTINGUTILS.sound)
		{
			PlayerPrefs.SetInt(SETTINGUTILS.soundKEY, 1);
		}
		else
		{
			PlayerPrefs.SetInt(SETTINGUTILS.soundKEY, 0);
		}
		SETTINGUTILS.SetSoundStatus();
	}

	public static void SetSoundStatus()
	{
		BGSoundManager.Instance.IsMute = !SETTINGUTILS.music;
		LoopSoundManager.Instance.IsMute = !SETTINGUTILS.sound;
		SoundManager.Instance.IsMute = !SETTINGUTILS.sound;
	}

	public static void GetSaved_HintStatus()
	{
		if (PlayerPrefs.HasKey(SETTINGUTILS.HintKEY))
		{
			if (PlayerPrefs.GetInt(SETTINGUTILS.HintKEY) == 1)
			{
				SETTINGUTILS.Hint = true;
			}
			else
			{
				SETTINGUTILS.Hint = false;
			}
		}
		else
		{
			SETTINGUTILS.Hint = true;
		}
	}

	public static bool GetSaved_Notificationtatus()
	{
		if (PlayerPrefs.HasKey(SETTINGUTILS.NotifKey))
		{
			if (PlayerPrefs.GetInt(SETTINGUTILS.NotifKey) == 1)
			{
				SETTINGUTILS.Notification = true;
			}
			else
			{
				SETTINGUTILS.Notification = false;
			}
		}
		else
		{
			SETTINGUTILS.Notification = true;
		}
		return SETTINGUTILS.Notification;
	}

	public static void saveHintKey()
	{
		if (SETTINGUTILS.Hint)
		{
			PlayerPrefs.SetInt(SETTINGUTILS.HintKEY, 1);
		}
		else
		{
			PlayerPrefs.SetInt(SETTINGUTILS.HintKEY, 0);
		}
	}

	public static void saveNotifytKey()
	{
		if (SETTINGUTILS.Notification)
		{
			PlayerPrefs.SetInt(SETTINGUTILS.NotifKey, 1);
		}
		else
		{
			PlayerPrefs.SetInt(SETTINGUTILS.NotifKey, 0);
		}
	}

	public static bool music;

	public static bool sound;

	public static bool Notification;

	public static bool Hint;

	private static string musicKEY = "mkey";

	private static string soundKEY = "skey";

	public static string NotifKey = "NotifKey";

	private static string HintKEY = "Hintkey";

	public const string LIVES_KEY = "GameLifes";
}
