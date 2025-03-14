using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class InAppController : MonoBehaviour, IStoreListener
{
	public static event InAppController.InAppSuccessCheck InAppSuccessCallBack;

	public static event InAppController.InAppFailCheck InAppSFailCallBack;

	public static InAppController instance
	{
		get
		{
			if (InAppController._Instance == null)
			{
				InAppController._Instance = UnityEngine.Object.FindObjectOfType<InAppController>();
			}
			return InAppController._Instance;
		}
	}

	private void Awake()
	{
	}

	private void Start()
	{
		if (this.m_StoreController == null)
		{
			this.InitializePurchasing();
		}
	}

	public void InitializePurchasing()
	{
		if (this.IsInitialized())
		{
			return;
		}
		ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(), new IPurchasingModule[0]);
		for (int i = 0; i < this.ConsumableProducts.Length; i++)
		{
			configurationBuilder.AddProduct(this.ConsumableProducts[i], ProductType.Consumable);
		}
		for (int j = 0; j < this.NonConsumableProducts.Length; j++)
		{
			configurationBuilder.AddProduct(this.NonConsumableProducts[j], ProductType.NonConsumable);
		}
		UnityPurchasing.Initialize(this, configurationBuilder);
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		this.m_StoreController = controller;
		InAppController.m_StoreExtensionProvider = extensions;
		UnityEngine.Debug.Log("--- OnInitialized");
		UnityEngine.Debug.Log("products=" + this.m_StoreController.products);
		this.RestorePurchases();
		for (int i = 0; i < this.ConsumableProducts.Length; i++)
		{
			Product product = this.m_StoreController.products.WithID(this.ConsumableProducts[i]);
			PlayerPrefs.SetString("consumableProducts_" + i, product.metadata.localizedPriceString);
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"price of product=",
				product.metadata.ToString(),
				"===",
				product.metadata.localizedPrice,
				"::::",
				product.metadata.localizedPriceString
			}));
		}
		for (int j = 0; j < this.NonConsumableProducts.Length; j++)
		{
			Product product2 = this.m_StoreController.products.WithID(this.NonConsumableProducts[j]);
			PlayerPrefs.SetString("nonConsumableProducts_" + j, product2.metadata.localizedPriceString);
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"price of product=",
				product2.ToString(),
				"===",
				product2.metadata.localizedPrice,
				"::::",
				product2.metadata.localizedPriceString
			}));
		}
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		UnityEngine.Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}

	public void RestorePurchases()
	{
		InAppController.m_StoreExtensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(delegate(bool result)
		{
			if (result)
			{
				UnityEngine.Debug.Log("--- OnInitialized RestoreProducts");
				for (int i = 0; i < this.NonConsumableProducts.Length; i++)
				{
					Product product = this.m_StoreController.products.WithID(this.NonConsumableProducts[i]);
					if (product != null && product.hasReceipt)
					{
						UnityEngine.Debug.Log("Restored id is=" + this.NonConsumableProducts[i]);
						if (!PlayerPrefs.HasKey("IsRestored"))
						{
							AdManager.instance.ShowToast("Restored successfully");
							PlayerPrefs.SetString("IsRestored", "true");
						}
						CallbacksController.instance.InAppCallBacks(product.definition.id);
						this.checkUnlockAllPurchase(product.definition.id);
					}
				}
			}
		});
	}

	private bool IsInitialized()
	{
		return this.m_StoreController != null && InAppController.m_StoreExtensionProvider != null;
	}

	public void BuySubscription()
	{
		this.BuyProductID(InAppController.kProductIDSubscription);
	}

	public void BuyProductID(string InAppID)
	{
		if (this.IsInitialized())
		{
			Product product = this.m_StoreController.products.WithID(InAppID);
			if (product != null && product.availableToPurchase)
			{
				UnityEngine.Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				this.m_StoreController.InitiatePurchase(InAppID);
			}
			else
			{
				UnityEngine.Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		else
		{
			UnityEngine.Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}

	public void BuyProductID(int InAppIndex, bool IsNonConsumable, GameObject BuyBtn = null)
	{
		UnityEngine.Debug.Log("--------- BuyProductID");
		if (!IsNonConsumable)
		{
			this.productId = this.ConsumableProducts[InAppIndex];
		}
		else
		{
			this.productId = this.NonConsumableProducts[InAppIndex];
		}
		this.ClickedBuyBtn = BuyBtn;
		if (this.IsInitialized())
		{
			Product product = this.m_StoreController.products.WithID(this.productId);
			if (product != null && product.availableToPurchase)
			{
				UnityEngine.Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				this.m_StoreController.InitiatePurchase(product);
			}
			else
			{
				UnityEngine.Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		else
		{
			UnityEngine.Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		UnityEngine.Debug.Log("------------------ process purchase");
		if (InAppController.InAppSuccessCallBack != null)
		{
			InAppController.InAppSuccessCallBack();
		}
		CallbacksController.instance.InAppCallBacks(args.purchasedProduct.definition.id);
		this.checkUnlockAllPurchase(args.purchasedProduct.definition.id);
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		InAppController.InAppSFailCallBack();
		UnityEngine.Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}

	public bool IsRestorableProduct(int Index)
	{
		Product product = this.m_StoreController.products.WithID(this.NonConsumableProducts[Index]);
		return product != null && product.hasReceipt;
	}

	public void checkUnlockAllPurchase(string productId)
	{
		if (productId == this.NonConsumableProducts[this.UnlockAllUpgradesIndex] || productId == this.NonConsumableProducts[this.UpgradesDiscountIndex])
		{
			AdManager.UpgradeUnlocked = 1;
		}
		if (productId == this.NonConsumableProducts[this.UnlockAllLevelsIndex] || productId == this.NonConsumableProducts[this.LevelsDiscountIndex])
		{
			AdManager.LevelsUnlocked = 1;
		}
	}

	public IStoreController m_StoreController;

	public static IExtensionProvider m_StoreExtensionProvider;

	public static string kProductIDSubscription = "subscription";

	private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

	private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

	public string[] ConsumableProducts;

	public string[] NonConsumableProducts;

	public int UnlockAllLevelsIndex;

	public int LevelsDiscountIndex;

	public int UnlockAllUpgradesIndex;

	public int UpgradesDiscountIndex;

	private GameObject ClickedBuyBtn;

	private static InAppController _Instance;

	private string productId;

	public delegate void InAppSuccessCheck();

	public delegate void InAppFailCheck();
}
