using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LCMoreGames : MonoBehaviour
{
	private void Awake()
	{
		LCMoreGames.instance = this;
		this.IsCreatedMGList = false;
		base.gameObject.SetActive(false);
	}

	public void CreateMGList(int count)
	{
		this.IsCreatedMGList = true;
		this.MgListCount = count;
		for (int i = 0; i < AdManager.instance.MgImgList.Count; i++)
		{
			this.MGList[i].GetComponent<Image>().sprite = AdManager.instance.MgImgList[i];
		}
	}

	public void Open()
	{
		if (AdManager.instance.MgImgList.Count > 0)
		{
			base.gameObject.SetActive(true);
			for (int i = 0; i < this.MGList.Count; i++)
			{
				this.MGList[i].SetActive(false);
			}
			this.SetMGList();
		}
	}

	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	private void SetMGList()
	{
		if (this.MGList.Count == 0)
		{
			return;
		}
		int num = PlayerPrefs.GetInt("LastShownMGIndex", -1);
		num++;
		if (num >= this.MgListCount)
		{
			num = 0;
		}
		this.MGList[num].SetActive(true);
		PlayerPrefs.SetInt("LastShownMGIndex", num);
	}

	public void MGBtnClick(int index)
	{
		Application.OpenURL(AdManager.instance.MgLinkToList[index]);
	}

	public List<GameObject> MGList = new List<GameObject>();

	public static LCMoreGames instance;

	public bool IsCreatedMGList;

	private int MgListCount;
}
