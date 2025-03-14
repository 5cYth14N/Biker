using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowAchHandler : MonoBehaviour
{
	private void Start()
	{
		base.gameObject.GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.BuyClicked();
		});
	}

	public void BuyClicked()
	{
		UnityEngine.Debug.Log("--- Show Achievements click");
		AdManager.instance.ShowAchievements();
	}
}
