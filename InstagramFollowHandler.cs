using System;
using UnityEngine;
using UnityEngine.UI;

public class InstagramFollowHandler : MonoBehaviour
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
		UnityEngine.Debug.Log("--- Show Leaderboards click");
		AdManager.instance.InstagramFollow(this.RewardCoins);
	}

	public int RewardCoins;
}
