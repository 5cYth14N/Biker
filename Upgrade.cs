using System;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
	private void Start()
	{
		Upgrade._instance = this;
		this.previousBtn.SetActive(false);
		UnityEngine.Debug.Log("upgrade Start::");
		this.vehiclePurchasedStr = PlayerPrefs.GetString(GameEnum.vehiclePurchased);
		this.camInitPos = this.mainCam.transform.position;
		this.camInitRotation = this.mainCam.transform.localEulerAngles;
	}

	private void OnEnable()
	{
		this.UpdateCoinText();
		if (PageHandler._instance)
		{
			PageHandler._instance.pageType = PageHandler.pageTypeEnum.upgrade;
		}
		this.ChangeTruckDetails();
		this.ShareBtn.SetActive(false);
		this.loading.SetActive(false);
		this.CheckAllInAppButtons();
		this.loadingBar.fillAmount = 0f;
		if (AdManager.instance)
		{
			AdManager.instance.RunActions(AdManager.PageType.Upgrade, 1, 0);
		}
	}

	public void CheckAllInAppButtons()
	{
		if (PlayerPrefs.GetString(GameEnum.vehiclePurchased) == "1111111111")
		{
			this.unlockAllTrucksBtn.SetActive(false);
			this.ShareBtn.SetActive(false);
			this.buyBtn.SetActive(false);
		}
	}

	private void BikeTweenGo()
	{
		iTween.MoveTo(this.bikesParent.gameObject, iTween.Hash(new object[]
		{
			"x",
			-5,
			"time",
			0.2,
			"easetype",
			iTween.EaseType.linear
		}));
		base.Invoke("BikeTweenComeBackNxt", 0.5f);
		this.nextBtn.GetComponent<Button>().interactable = false;
		this.previousBtn.GetComponent<Button>().interactable = false;
	}

	private void BikeTweenComeBackNxt()
	{
		iTween.MoveTo(this.bikesParent.gameObject.gameObject, iTween.Hash(new object[]
		{
			"x",
			0,
			"time",
			0.2,
			"easetype",
			iTween.EaseType.linear
		}));
		this.nextBtn.GetComponent<Button>().interactable = true;
		this.previousBtn.GetComponent<Button>().interactable = true;
	}

	public void NextClick()
	{
		AudioClipManager.Instance.Play(1);
		this.FillBikeDetails(0.05f);
		this.BikeTweenGo();
		this.previousBtn.SetActive(true);
		base.Invoke("ChangeNextCar", 0.5f);
	}

	private void ChangeNextCar()
	{
		if (this.carIndex < this.myCars.Length)
		{
			this.carIndex++;
			this.previousBtn.SetActive(true);
			for (int i = 0; i < this.myCars.Length; i++)
			{
				this.myCars[this.carIndex].SetActive(true);
			}
			this.myCars[this.carIndex - 1].SetActive(false);
			if (this.carIndex >= this.myCars.Length - 1)
			{
				this.nextBtn.SetActive(false);
			}
			this.CheckBuyBtn();
			this.ChangeTruckDetails();
			if (PlayerPrefs.GetString(GameEnum.vehiclePurchased) != "1111111111")
			{
				this.CheckShareBtn();
			}
		}
	}

	public void PreviousClick()
	{
		AudioClipManager.Instance.Play(1);
		this.DecreaseBikeDetails(0.05f);
		this.BikeTweenGo();
		this.nextBtn.SetActive(true);
		base.Invoke("ChangePreviousCar", 0.5f);
	}

	private void ChangePreviousCar()
	{
		if (this.carIndex > 0)
		{
			this.carIndex--;
			this.nextBtn.SetActive(true);
			for (int i = 0; i < this.myCars.Length; i++)
			{
				this.myCars[this.carIndex].SetActive(true);
			}
			this.myCars[this.carIndex + 1].SetActive(false);
			if (this.carIndex <= 0)
			{
				this.previousBtn.SetActive(false);
			}
			this.CheckBuyBtn();
			this.ChangeTruckDetails();
			if (PlayerPrefs.GetString(GameEnum.vehiclePurchased) != "1111111111")
			{
				this.CheckShareBtn();
			}
		}
	}

	public void CheckBuyBtn()
	{
		this.vehicleCost = this.vehicleCostArray[this.carIndex];
		this.priceText.text = this.vehicleCost.ToString();
		this.CarUnlockCheck(this.carIndex);
	}

	private void CheckShareBtn()
	{
	}

	private void FillBikeDetails(float value)
	{
		this.bikePerformance.fillAmount += value;
		this.bikeSpeed.fillAmount += value;
		this.bikeAcceleration.fillAmount += value;
	}

	private void DecreaseBikeDetails(float value)
	{
		this.bikePerformance.fillAmount -= value;
		this.bikeSpeed.fillAmount -= value;
		this.bikeAcceleration.fillAmount -= value;
	}

	public void BuyClick(int _index)
	{
		char[] array = this.vehiclePurchasedStr.ToCharArray();
		if (this.totalCoins >= this.vehicleCost)
		{
			array[_index] = '1';
			string value = new string(array);
			PlayerPrefs.SetString(GameEnum.vehiclePurchased, value);
			this.vehiclePurchasedStr = PlayerPrefs.GetString(GameEnum.vehiclePurchased);
			UnityEngine.Debug.Log("you can buy");
			this.totalCoins -= this.vehicleCost;
			UnityEngine.Debug.Log("totalcoins after purchase::" + this.totalCoins);
			PlayerPrefs.SetInt(GameEnum.totalCoins, this.totalCoins);
			this.UpdateCoinText();
			this.CarUnlockCheck(_index);
		}
		else
		{
			UnityEngine.Debug.Log("cant buy..open store");
			AdManager.instance.BuyItem(1, true, null);
		}
		UnityEngine.Debug.Log("vehicleString::" + PlayerPrefs.GetString(GameEnum.vehiclePurchased));
	}

	public bool CarUnlockCheck(int _index)
	{
		if (PlayerPrefs.GetString(GameEnum.vehiclePurchased) == "1111111111")
		{
			return false;
		}
		if (this.vehiclePurchasedStr.Substring(_index, 1) == "1")
		{
			this.selectBtn.SetActive(true);
			this.buyBtn.SetActive(false);
			UnityEngine.Debug.LogError("CheckBuyBtn_:" + _index);
			return true;
		}
		this.selectBtn.SetActive(false);
		this.buyBtn.SetActive(true);
		return false;
	}

	private void ChangeTruckDetails()
	{
		int num = 0;
		if (num >= this.vehiclePower.Length)
		{
			return;
		}
		this.truckPowerTxt.text = this.vehiclePower[this.carIndex];
		for (int i = 0; i < this.vehicleSpeed.Length; i++)
		{
			this.truckTorqueTxt.text = this.vehicleSpeed[this.carIndex];
		}
		for (int j = 0; j < this.vehicleWeight.Length; j++)
		{
			this.truckWeightTxt.text = this.vehicleWeight[this.carIndex];
		}
		for (int k = 0; k < this.vehicleGrip.Length; k++)
		{
			this.truckChassisTxt.text = this.vehicleGrip[this.carIndex];
		}
		for (int l = 0; l < this.vehicleName.Length; l++)
		{
			this.vehicleNameTxt.text = this.vehicleName[this.carIndex];
		}
	}

	public void UpdateCoinText()
	{
		this.totalCoinsText.text = PlayerPrefs.GetInt(GameEnum.totalCoins).ToString();
		this.totalCoins = PlayerPrefs.GetInt(GameEnum.totalCoins);
		UnityEngine.Debug.Log("totalcoins::" + this.totalCoins);
	}

	public void ShareClick()
	{
		UnityEngine.Debug.Log("Share");
		PlayerPrefs.SetString(GameEnum.isShareDone, "true");
		this.CheckShareBtn();
	}

	public void SelectClick()
	{
		AudioClipManager.Instance.Play(1);
		PageHandler._instance.Open_LevelSelection();
	}

	public static Upgrade _instance;

	public GameObject[] myCars;

	public int carIndex;

	public GameObject nextBtn;

	public GameObject previousBtn;

	public GameObject selectBtn;

	public GameObject buyBtn;

	public Text priceText;

	public int vehicleCost;

	public int[] vehicleCostArray;

	public Text totalCoinsText;

	public int totalCoins;

	private string vehiclePurchasedStr;

	public GameObject ShareBtn;

	public GameObject loading;

	public GameObject unlockAllTrucksBtn;

	public Image loadingBar;

	public Text truckPowerTxt;

	public Text truckTorqueTxt;

	public Text truckWeightTxt;

	public Text truckChassisTxt;

	public Text vehicleNameTxt;

	public string[] vehicleSpeed = new string[0];

	public string[] vehicleGrip = new string[0];

	public string[] vehiclePower = new string[]
	{
		"410 HP",
		"460 HP",
		"510 HP",
		"310 HP",
		"450 HP",
		"510 HP",
		"480 HP",
		"540 HP",
		"680 HP",
		"476 HP"
	};

	public string[] vehicleWeight = new string[0];

	private string[] vehicleName = new string[]
	{
		"HONDA CRF450R ",
		"HUSQVARNA FC450",
		"450_SX-F",
		"KTM 450SX-F",
		"SUZUKI RM-Z450",
		"TM RACING MX450",
		"YAMAHA YZ450F",
		"HUSQVARNA FC350",
		"KTM 350SX-F",
		"TM RACING MX300",
		"HONDA CRF250R",
		"HUSQVARNA FC250",
		"HUSQVARNA TC250",
		"KAWASAKI KX250F",
		"KTM 250SX-F",
		"Rondai",
		"Huget",
		"Cherystler",
		"Amber",
		"Taxi Cab",
		"Quand Yover",
		"Cherystler",
		"Blue Star",
		"Quand Yover",
		"Denaulto",
		"Tinky",
		"Flipper",
		"Rordai",
		"Police Car",
		"Luxury lane"
	};

	public Image bikePerformance;

	public Image bikeSpeed;

	public Image bikeAcceleration;

	public Camera mainCam;

	private Vector3 camInitPos;

	private Vector3 camInitRotation;

	public Text currentCarDisplayTxt;

	public GameObject bikesParent;
}
