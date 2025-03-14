using System;
using System.Collections;
using System.Xml;
using UnityEngine;

public class XMLReader : MonoBehaviour
{
	private void Start()
	{
		base.StartCoroutine("LoadAllCommonXML");
		base.StartCoroutine("LoadMenuAdXML");
	}

	private IEnumerator LoadAllCommonXML()
	{
		if (AdManager.instance.isWifi_OR_Data_Availble())
		{
			UnityEngine.Debug.LogError("------------------------XML Reader Start");
			WWW xmlData = new WWW(AdManager.instance.AllCommonUrl);
			yield return xmlData;
			if (xmlData.error != null)
			{
				UnityEngine.Debug.LogError("---------- error DoSomething");
			}
			else
			{
				UnityEngine.Debug.LogError("implement code");
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					xmlDocument.LoadXml(xmlData.text);
				}
				catch (Exception arg)
				{
					UnityEngine.Debug.LogError("----------------Error loading :\n" + arg);
				}
				finally
				{
					UnityEngine.Debug.LogError("------------------------ loaded");
					this.MainNode = xmlDocument.SelectSingleNode("GameData");
					this.ReadAllCommonXmlData();
				}
			}
		}
		else
		{
			UnityEngine.Debug.Log("No Internet Connection to get XML data");
		}
		yield break;
	}

	private IEnumerator LoadMenuAdXML()
	{
		if (AdManager.instance.isWifi_OR_Data_Availble())
		{
			UnityEngine.Debug.LogError("------------------------MenuAdXML Reader Start");
			WWW xmlData = new WWW(AdManager.instance.MenuAdUrl);
			yield return xmlData;
			if (xmlData.error != null)
			{
				UnityEngine.Debug.LogError("---------- error DoSomething");
			}
			else
			{
				UnityEngine.Debug.LogError("implement code");
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					xmlDocument.LoadXml(xmlData.text);
				}
				catch (Exception arg)
				{
					UnityEngine.Debug.LogError("----------------Error loading :\n" + arg);
					//NotificationController.instance.initOneSignalLocally();
				}
				finally
				{
					UnityEngine.Debug.LogError("------------------------ loaded");
					this.MainMenuAdNode = xmlDocument.SelectSingleNode("games");
					this.ReadMenuAdXmlData();
				}
			}
		}
		else
		{
			UnityEngine.Debug.Log("No Internet Connection to get XML data");
		}
		yield break;
	}

	private void ReadAllCommonXmlData()
	{
		UnityEngine.Debug.LogError("---------- ReadingXmlData ------------");
		XmlNode xmlNode = this.MainNode.SelectSingleNode("PopUpLvls");
		string value = xmlNode.Attributes.GetNamedItem("RateInLvls").Value;
		string[] array = value.Split(new char[]
		{
			'_'
		});
		AdManager.instance.RatingPopInLevels.Clear();
		for (int i = 0; i < array.Length; i++)
		{
			AdManager.instance.RatingPopInLevels.Add(int.Parse(array[i]));
		}
		string value2 = xmlNode.Attributes.GetNamedItem("ShareInLvls").Value;
		string[] array2 = value2.Split(new char[]
		{
			'_'
		});
		AdManager.instance.SharingPopInLevels.Clear();
		for (int j = 0; j < array2.Length; j++)
		{
			AdManager.instance.SharingPopInLevels.Add(int.Parse(array2[j]));
		}
		XmlNode xmlNode2 = this.MainNode.SelectSingleNode("Coins");
		string value3 = xmlNode2.Attributes.GetNamedItem("RateCoins").Value;
		AdManager.instance.RateCoins = int.Parse(value3);
		string value4 = xmlNode2.Attributes.GetNamedItem("ShareCoins").Value;
		AdManager.instance.ShareCoins = int.Parse(value4);
		XmlNode xmlNode3 = this.MainNode.SelectSingleNode("DiscountPop");
		string value5 = xmlNode3.Attributes.GetNamedItem("Menu").Value;
		string[] array3 = value5.Split(new char[]
		{
			'_'
		});
		AdManager.instance.DiscountPopInMenu.Clear();
		for (int k = 0; k < array3.Length; k++)
		{
			AdManager.instance.DiscountPopInMenu.Add(int.Parse(array3[k]));
		}
		string value6 = xmlNode3.Attributes.GetNamedItem("UPLS").Value;
		string[] array4 = value6.Split(new char[]
		{
			'_'
		});
		for (int l = 0; l < array4.Length; l++)
		{
			AdManager.instance.UnlockPopIn_UP_LS[l] = int.Parse(array4[l]);
		}
		XmlNode xmlNode4 = this.MainNode.SelectSingleNode("AdType");
		string value7 = xmlNode4.Attributes.GetNamedItem("RotationType").Value;
		string[] array5 = value7.Split(new char[]
		{
			'_'
		});
		AdManager.instance.RotationAdsList.Clear();
		for (int m = 0; m < array5.Length; m++)
		{
			AdManager.instance.RotationAdsList.Add(int.Parse(array5[m]));
		}
		string value8 = xmlNode4.Attributes.GetNamedItem("RotationRewardType").Value;
		string[] array6 = value8.Split(new char[]
		{
			'_'
		});
		AdManager.instance.RotationVideoAdsList.Clear();
		for (int n = 0; n < array6.Length; n++)
		{
			AdManager.instance.RotationVideoAdsList.Add(int.Parse(array6[n]));
		}
		AdManager.instance.SetInitialAdIndex();
		XmlNode xmlNode5 = this.MainNode.SelectSingleNode("Desc");
		AdManager.instance.RateDesc = xmlNode5.Attributes.GetNamedItem("RateDesc").Value;
		AdManager.instance.ShareDesc = xmlNode5.Attributes.GetNamedItem("ShareDesc").Value;
		XmlNode xmlNode6 = this.MainNode.SelectSingleNode("Addelay");
		AdManager.instance.LcAdDelay = float.Parse(xmlNode6.Attributes.GetNamedItem("lcaddelay").Value);
		AdManager.instance.LfAdDelay = float.Parse(xmlNode6.Attributes.GetNamedItem("lfaddelay").Value);
		AdManager.instance.PreLfAdDelay = float.Parse(xmlNode6.Attributes.GetNamedItem("prelfdelay").Value);
		AdManager.instance.LsAdDelay = float.Parse(xmlNode6.Attributes.GetNamedItem("upldelay").Value);
		AdManager.instance.UpgradeAdDelay = float.Parse(xmlNode6.Attributes.GetNamedItem("upldelay").Value);
		AdManager.instance.AdDelay = int.Parse(xmlNode6.Attributes.GetNamedItem("Addelay").Value);
		AdManager.instance.LastAdShownTime = (float)(-(float)AdManager.instance.AdDelay);
		XmlNode xmlNode7 = this.MainNode.SelectSingleNode("shareLink");
		AdManager.instance.ShareUrl = xmlNode7.Attributes.GetNamedItem("urlFB").Value;
		XmlNode xmlNode8 = this.MainNode.SelectSingleNode("AdIn");
		string value9 = xmlNode8.Attributes.GetNamedItem("pages").Value;
		string[] array7 = value9.Split(new char[]
		{
			'_'
		});
		for (int num = 0; num < array7.Length; num++)
		{
			AdManager.instance.AdInPages[num] = int.Parse(array7[num]);
		}
		if (this.MainNode.SelectSingleNode("Notification") != null)
		{
			XmlNode xmlNode9 = this.MainNode.SelectSingleNode("Notification");
			string value10 = xmlNode9.Attributes.GetNamedItem("onesignal").Value;
			string[] array8 = value10.Split(new char[]
			{
				'_'
			});
			//NotificationController.instance.OneSignalAppID = array8[0];
			//NotificationController.instance.GoogleProjectID = array8[1];
		}
		if (this.MainNode.SelectSingleNode("moregames") != null)
		{
			this.MoreGamesNode = this.MainNode.SelectSingleNode("moregames");
			AdManager.instance.MgImgLinkList.Clear();
			for (int num2 = 0; num2 < 10; num2++)
			{
				if (this.MoreGamesNode.Attributes.GetNamedItem("mgImgLink" + (num2 + 1)) != null)
				{
					string value11 = this.MoreGamesNode.Attributes.GetNamedItem("mgImgLink" + (num2 + 1)).Value;
					AdManager.instance.MgImgLinkList.Add(value11);
				}
			}
			AdManager.instance.MgLinkToList.Clear();
			for (int num3 = 0; num3 < AdManager.instance.MgImgLinkList.Count; num3++)
			{
				if (this.MoreGamesNode.Attributes.GetNamedItem("mgLinkto" + (num3 + 1)) != null)
				{
					string value12 = this.MoreGamesNode.Attributes.GetNamedItem("mgLinkto" + (num3 + 1)).Value;
					AdManager.instance.MgLinkToList.Add(value12);
				}
			}
			AdManager.instance.MgImgList.Clear();
			for (int num4 = 0; num4 < AdManager.instance.MgImgLinkList.Count; num4++)
			{
				AdManager.instance.MgImgList.Add(null);
			}
			base.StartCoroutine(this.DownloadImg(AdManager.instance.MgImgLinkList[0], 0));
		}
	}

	private void ReadMenuAdXmlData()
	{
		XmlNode xmlNode = this.MainMenuAdNode.SelectSingleNode(Application.identifier);
		if (xmlNode == null)
		{
			UnityEngine.Debug.Log("not found");
			xmlNode = this.MainMenuAdNode.SelectSingleNode("common");
		}
		else
		{
			UnityEngine.Debug.Log("---------- found");
			if (xmlNode.Attributes.GetNamedItem("onesignal") != null)
			{
				string value = xmlNode.Attributes.GetNamedItem("onesignal").Value;
				string[] array = value.Split(new char[]
				{
					'_'
				});
				//NotificationController.instance.OneSignalAppID = array[0];
				//NotificationController.instance.GoogleProjectID = array[1];
			}
		}
		AdManager.instance.MenuAdImgLink = xmlNode.Attributes.GetNamedItem("mimg").Value;
		AdManager.instance.MenuAdLinkTo = xmlNode.Attributes.GetNamedItem("linkto").Value;
		AdManager.instance.MgLink = xmlNode.Attributes.GetNamedItem("MgLink").Value;
		AdManager.instance.ExitLink = xmlNode.Attributes.GetNamedItem("exitLink").Value;
		WebViewController.instance.LoadDummyUrls();
		if (AdManager.instance.MenuAdImgLink != string.Empty)
		{
			base.StartCoroutine(this.LoadMenuAd());
		}
		//NotificationController.instance.initOneSignalLocally();
	}

	private IEnumerator LoadMenuAd()
	{
		WWW menuAdView = new WWW(AdManager.instance.MenuAdImgLink);
		yield return menuAdView;
		if (string.IsNullOrEmpty(menuAdView.error))
		{
			UnityEngine.Debug.Log("---------- OpenMenuScene from XML Reader is Menuloaded=" + AdManager.instance.IsMenuLoaded);
			Texture2D texture = menuAdView.texture;
			AdManager.instance.menuAdImg = Sprite.Create(texture, new Rect(0f, 0f, (float)texture.width, (float)texture.height), new Vector2(0f, 0f));
			MenuAdPage.instance.SetMenuAdTexture();
			if (!WelcomeGiftPage.instance.gameObject.activeInHierarchy && PlayerPrefs.GetString("IsGotWelcomeGift", "false") == "true")
			{
				AdManager.instance.StartCoroutine(AdManager.instance.OpenMenuScene(0f, true));
			}
		}
		else
		{
			MonoBehaviour.print("menuAd not loaded=" + menuAdView.error);
		}
		yield break;
	}

	private IEnumerator DownloadImg(string url, int Index)
	{
		int containIndex = this.IsFoundMGLink(Index);
		if (containIndex == -1)
		{
			this.NextMGIndex = Index + 1;
			WWW menuAdView = new WWW(url);
			yield return menuAdView;
			Texture2D texture = menuAdView.texture;
			AdManager.instance.MgImgList[Index] = Sprite.Create(texture, new Rect(0f, 0f, (float)texture.width, (float)texture.height), new Vector2(0f, 0f));
			if (this.NextMGIndex < AdManager.instance.MgLinkToList.Count)
			{
				base.StartCoroutine(this.DownloadImg(AdManager.instance.MgImgLinkList[this.NextMGIndex], this.NextMGIndex));
			}
			if (!LCMoreGames.instance.IsCreatedMGList && this.NextMGIndex >= AdManager.instance.MgImgList.Count)
			{
				LCMoreGames.instance.CreateMGList(AdManager.instance.MgImgList.Count);
			}
			yield break;
		}
		this.NextMGIndex = Index + 1;
		AdManager.instance.MgImgList[Index] = AdManager.instance.MgImgList[containIndex];
		if (this.NextMGIndex < AdManager.instance.MgLinkToList.Count)
		{
			base.StartCoroutine(this.DownloadImg(AdManager.instance.MgImgLinkList[this.NextMGIndex], this.NextMGIndex));
		}
		yield break;
	}

	private int IsFoundMGLink(int Index)
	{
		for (int i = 0; i < AdManager.instance.MgImgLinkList.Count; i++)
		{
			if (Index != i && AdManager.instance.MgImgLinkList[Index] == AdManager.instance.MgImgLinkList[i] && AdManager.instance.MgImgList[i] != null)
			{
				return i;
			}
		}
		return -1;
	}

	private XmlNode MainNode;

	private XmlNode MainMenuAdNode;

	private XmlNode MoreGamesNode;

	private int NextMGIndex;
}
