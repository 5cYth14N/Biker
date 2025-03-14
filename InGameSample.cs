using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameSample : MonoBehaviour
{
	private void Awake()
	{
		this.LCBtn.GetComponent<Button>().interactable = false;
		this.LFBtn.GetComponent<Button>().interactable = false;
	}

	private void Start()
	{
		this.LvlNo.text = "Level" + InGameSample.CurrentLvl;
		AdManager.instance.RunActions(AdManager.PageType.InGame, InGameSample.CurrentLvl, 0);
		base.Invoke("EnableBtns", 3f);
	}

	private void EnableBtns()
	{
		this.LCBtn.GetComponent<Button>().interactable = true;
		this.LFBtn.GetComponent<Button>().interactable = true;
	}

	public void LCClick()
	{
		LevelCompleteSample.instance.Open();
	}

	public void LFClick()
	{
		PreLFPage.instance.Open();
	}

	public Text LvlNo;

	public static int CurrentLvl;

	public GameObject LCBtn;

	public GameObject LFBtn;
}
