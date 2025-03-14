using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
	private void OnEnable()
	{
		if (this.PriceTxt == null && this.ShowPrice && base.gameObject.transform.childCount > 0)
		{
			this.PriceTxt = base.GetComponentInChildren<Text>();
		}
		if (this.inAppCategory == BuyItem.InAppCategory.NonConsumable)
		{
			if (InAppController.instance.m_StoreController != null)
			{
				if (InAppController.instance.IsRestorableProduct(this.Index))
				{
					UnityEngine.Debug.Log("------------ It is purchased Already");
					base.gameObject.GetComponent<Button>().interactable = false;
				}
				else
				{
					if (this.Index == InAppController.instance.UnlockAllLevelsIndex)
					{
						this.DiscountIndex = InAppController.instance.LevelsDiscountIndex;
					}
					else if (this.Index == InAppController.instance.UnlockAllUpgradesIndex)
					{
						this.DiscountIndex = InAppController.instance.UpgradesDiscountIndex;
					}
					if (this.DiscountIndex != -1 && InAppController.instance.IsRestorableProduct(this.DiscountIndex))
					{
						base.gameObject.GetComponent<Button>().interactable = false;
					}
				}
			}
			if (PlayerPrefs.HasKey("nonConsumableProducts_" + this.Index) && this.ShowPrice)
			{
				this.PriceTxt.text = PlayerPrefs.GetString("nonConsumableProducts_" + this.Index, "BUY");
			}
		}
		else
		{
			base.gameObject.GetComponent<Button>().interactable = true;
			if (PlayerPrefs.HasKey("consumableProducts_" + this.Index) && this.ShowPrice)
			{
				this.PriceTxt.text = PlayerPrefs.GetString("consumableProducts_" + this.Index, "BUY");
			}
		}
	}

	private void OnDisable()
	{
	}

	private void Start()
	{
		base.gameObject.GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.BuyClicked(this.Index);
		});
	}

	public void BuyClicked(int InAppIndex)
	{
		UnityEngine.Debug.Log("----- BuyClicked");
		if (this.inAppCategory == BuyItem.InAppCategory.NonConsumable)
		{
			InAppController.InAppSuccessCallBack += this.SuccessCallBack;
			InAppController.InAppSFailCallBack += this.FailCallBack;
		}
		if (this.inAppCategory == BuyItem.InAppCategory.NonConsumable)
		{
			AdManager.instance.BuyItem(InAppIndex, true, base.gameObject);
		}
		else
		{
			AdManager.instance.BuyItem(InAppIndex, false, base.gameObject);
		}
	}

	public void SuccessCallBack()
	{
		UnityEngine.Debug.Log("-------------- InAppSuccessCallBack");
		InAppController.InAppSuccessCallBack -= this.SuccessCallBack;
		InAppController.InAppSFailCallBack -= this.FailCallBack;
		if (this.inAppCategory == BuyItem.InAppCategory.NonConsumable)
		{
			base.gameObject.GetComponent<Button>().interactable = false;
		}
		else
		{
			base.gameObject.GetComponent<Button>().interactable = true;
		}
	}

	public void FailCallBack()
	{
		UnityEngine.Debug.Log("-------------- InAppFailedCallBack");
		base.gameObject.GetComponent<Button>().interactable = true;
	}

	public int Index;

	public Text PriceTxt;

	public bool ShowPrice = true;

	private int DiscountIndex = -1;

	public BuyItem.InAppCategory inAppCategory;

	public enum InAppCategory
	{
		Consumable,
		NonConsumable
	}
}
