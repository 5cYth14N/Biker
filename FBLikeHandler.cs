using System;
using UnityEngine;
using UnityEngine.UI;

public class FBLikeHandler : MonoBehaviour
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
		AdManager.instance.FBLike(this.RewardCoins);
	}

	public int RewardCoins;
}
