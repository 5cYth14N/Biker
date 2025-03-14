using System;
using UnityEngine;
using UnityEngine.UI;

public class Session_N_PageNav_CountDisplay : MonoBehaviour
{
	private void Start()
	{
		base.Invoke("SetNavigationCountDisplay", 0.5f);
	}

	private void SetNavigationCountDisplay()
	{
		if (this.isSessionNavteCount)
		{
			base.GetComponent<Text>().text = "SessionCount=" + PlayerPrefs.GetInt("SessionCount", 1);
		}
		else
		{
			AdManager.PageType pageType = this.pageType;
			if (pageType != AdManager.PageType.Menu)
			{
				if (pageType != AdManager.PageType.LvlSelection)
				{
					if (pageType == AdManager.PageType.Upgrade)
					{
						base.GetComponent<Text>().text = "PageNavigationCount=" + PlayerPrefs.GetInt("UpgradePageNavigationCount", 0);
					}
				}
				else
				{
					base.GetComponent<Text>().text = "PageNavigationCount=" + PlayerPrefs.GetInt("LevelsPageNavigationCount", 0);
				}
			}
			else
			{
				base.GetComponent<Text>().text = "PageNavigationCount=" + PlayerPrefs.GetInt("MenuPageNavigationCount", 0);
			}
		}
	}

	public bool isSessionNavteCount;

	public AdManager.PageType pageType;
}
