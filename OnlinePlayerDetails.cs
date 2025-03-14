using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OnlinePlayerDetails : MonoBehaviour
{
	private void Awake()
	{
	}

	private void Start()
	{
		this.LoadAllInitially();
	}

	private void LoadAllInitially()
	{
		this.profilePic.gameObject.SetActive(false);
		this.loadingImg.SetActive(true);
		base.StartCoroutine(this.LoadLoadingImg((float)UnityEngine.Random.Range(3, 15)));
		this.connectingTxt.gameObject.SetActive(true);
		this.rotate = true;
		base.StartCoroutine(this.ConnectingEffect(0f));
	}

	private IEnumerator LoadLoadingImg(float delay)
	{
		yield return new WaitForSeconds(delay);
		this.loadingImg.SetActive(false);
		this.rotate = false;
		this.profilePic.gameObject.SetActive(true);
		this.GenerateRandomProfilePics();
		this.GenerateRandomProfileNames();
		yield break;
	}

	private void GenerateRandomProfilePics()
	{
		this.profilePicArray = Resources.LoadAll<Sprite>("aiicons");
		this.profilePicList = this.profilePicArray.ToList<Sprite>();
		Sprite sprite = this.profilePicList[UnityEngine.Random.Range(0, this.profilePicList.Count)];
		this.profilePic.sprite = sprite;
		this.isConnected = true;
	}

	private void GenerateRandomProfileNames()
	{
		if (!(TextAsset)Resources.Load(this.resourcesNameTxtFile, typeof(TextAsset)))
		{
			UnityEngine.Debug.Log("Names not found! Please add a .txt file named 'ainames' with a list of names to /Resources folder.");
			return;
		}
		int num = 0;
		this.opponentNames = (TextAsset)Resources.Load(this.resourcesNameTxtFile, typeof(TextAsset));
		this.nameReader = new StringReader(this.opponentNames.text);
		for (string item = this.nameReader.ReadLine(); item != null; item = this.nameReader.ReadLine())
		{
			num++;
			if (this.opponentNamesList.Count < num)
			{
				this.opponentNamesList.Add(item);
			}
		}
		int num2 = UnityEngine.Random.Range(0, this.opponentNamesList.Count);
		this.nameTxt.text = this.opponentNamesList[num2].ToString();
		UnityEngine.Debug.Log("random num: " + num2);
		this.name = this.nameTxt.text.ToString();
	}

	private void Update()
	{
		if (!this.rotate)
		{
			return;
		}
		this.loadingImg.transform.Rotate(0f, 0f, -500f * Time.deltaTime);
	}

	private IEnumerator ConnectingEffect(float delay)
	{
		while (!this.isConnected)
		{
			yield return new WaitForSeconds(this.delayToDots);
			this.connectingTxt.text = "CONNECTING\n.";
			yield return new WaitForSeconds(this.delayToDots);
			this.connectingTxt.text = "CONNECTING\n..";
			yield return new WaitForSeconds(this.delayToDots);
			this.connectingTxt.text = "CONNECTING\n...";
		}
		yield return this.isConnected;
		this.connectingTxt.text = "CONNECTED.";
		this.connectingTxt.color = new Color32(0, 118, 11, 225);
		UnityEngine.Debug.Log("______________");
		yield break;
	}

	public new string name;

	public Image profilePic;

	public GameObject loadingImg;

	public Text connectingTxt;

	public Text nameTxt;

	public bool rotate;

	public List<Sprite> profilePicList = new List<Sprite>();

	public Sprite[] profilePicArray;

	public List<string> opponentNamesList = new List<string>();

	public TextAsset opponentNames;

	public StringReader nameReader;

	[Header("Resources File Txt Names")]
	public string resourcesNameTxtFile;

	public bool isConnected;

	private float delayToDots = 0.3f;
}
