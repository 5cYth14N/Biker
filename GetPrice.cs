using System;
using UnityEngine;
using UnityEngine.UI;

public class GetPrice : MonoBehaviour
{
	private void OnEnable()
	{
		if (this.IsNonConsumable)
		{
			if (PlayerPrefs.HasKey("nonConsumableProducts_" + this.Index))
			{
				base.gameObject.GetComponent<Text>().text = PlayerPrefs.GetString("nonConsumableProducts_" + this.Index, "BUY");
			}
		}
		else if (PlayerPrefs.HasKey("consumableProducts_" + this.Index))
		{
			base.gameObject.GetComponent<Text>().text = PlayerPrefs.GetString("consumableProducts_" + this.Index, "BUY");
		}
	}

	public bool IsNonConsumable;

	public int Index;
}
