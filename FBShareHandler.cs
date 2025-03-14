using System;
using UnityEngine;
using UnityEngine.UI;

public class FBShareHandler : MonoBehaviour
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
		AdManager.instance.FacebookShare(this.RewardCoins);
	}

	public int RewardCoins;
}
