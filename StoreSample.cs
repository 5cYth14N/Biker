using System;
using UnityEngine;

public class StoreSample : MonoBehaviour
{
	private void Awake()
	{
		StoreSample.instance = this;
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
	}

	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	public void BuyItem(int id)
	{
	}

	public static StoreSample instance;

	public GameObject PopUp;
}
