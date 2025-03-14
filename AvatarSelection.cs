using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AvatarSelection : MonoBehaviour
{
	private void Awake()
	{
		AvatarSelection.instance = this;
		if (!PlayerPrefs.HasKey("playerimage"))
		{
			PlayerPrefs.SetInt("playerimage", 0);
		}
		if (PlayerPrefs.GetInt("playerimage") != 0)
		{
			this.myImage = this.allSprites[PlayerPrefs.GetInt("playerimage") - 1];
		}
	}

	public void SelectAvatar(int number)
	{
		Image component = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
		this.myImage = component;
		this.tickMark.transform.position = this.myImage.transform.position;
		this.tickMark.transform.parent = this.myImage.transform;
		this.imgInt = number;
	}

	public void ContinueBtnClick()
	{
		UnityEngine.Debug.Log("text__" + this.playerName.text);
		if (this.playerName.text == string.Empty)
		{
			UnityEngine.Debug.Log("text__" + this.playerName.text);
			base.StartCoroutine(this.EnableErrorStrip(0));
			this.errorTxt.text = "Please enter your name and click continue.";
		}
		else if (this.myImage == null)
		{
			base.StartCoroutine(this.EnableErrorStrip(0));
			this.errorTxt.text = "Please select avatar and click continue.";
		}
		else
		{
			PlayerPrefs.SetInt("entername", 1);
			this.SavePlayerDetails();
			Menu._instance.avatarSelectionPage.SetActive(false);
		}
	}

	private void OpenVsPage()
	{
		PlayerPrefs.SetInt("entername", 1);
		PageHandler._instance.Open_ModeSelectionPage();
		this.SavePlayerDetails();
	}

	private void SavePlayerDetails()
	{
		PlayerPrefs.SetInt("playerimage", this.imgInt);
		if (!PlayerPrefs.HasKey("playername"))
		{
			PlayerPrefs.SetString("playername", this.playerName.text);
		}
	}

	private IEnumerator EnableErrorStrip(int time)
	{
		yield return new WaitForSeconds(0f);
		this.errorStrip.SetActive(true);
		yield return new WaitForSeconds(5f);
		this.errorStrip.SetActive(false);
		yield break;
	}

	public static AvatarSelection instance;

	public Image myImage;

	public GameObject tickMark;

	public Text playerName;

	public Image[] allSprites;

	public GameObject errorStrip;

	public Text errorTxt;

	private int imgInt;
}
