using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFailSample : MonoBehaviour
{
	private void Awake()
	{
		LevelFailSample.instance = this;
		base.gameObject.SetActive(false);
	}

	public void Open()
	{
		base.gameObject.SetActive(true);
		this.PopUp.transform.localPosition = Vector3.zero;
		iTween.MoveFrom(this.PopUp, iTween.Hash(new object[]
		{
			"y",
			1000,
			"time",
			0.4f,
			"islocal",
			true,
			"easetype",
			iTween.EaseType.spring
		}));
		AdManager.instance.RunActions(AdManager.PageType.LF, InGameSample.CurrentLvl, 0);
	}

	public void Close()
	{
		SceneManager.LoadScene("MenuSample");
	}

	public void RetryBtnClick()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public static LevelFailSample instance;

	public GameObject PopUp;
}
