using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Multiplayerpage : MonoBehaviour
{
	private void Awake()
	{
		Multiplayerpage._instance = this;
	}

	private void OnEnable()
	{
		this.playerImage.sprite = AvatarSelection.instance.myImage.sprite;
		this.playerName.text = PlayerPrefs.GetString("playername");
		if (ModeSelection._instance.noOfRacers == ModeSelection.NoOfRacers.two)
		{
			this.popup.transform.localPosition = new Vector3(370f, this.popup.transform.localPosition.y, this.popup.transform.localPosition.z);
			this.playerImage.gameObject.SetActive(true);
			for (int i = 0; i < this.aiIcons.Count; i++)
			{
				this.aiIcons[i].gameObject.SetActive(false);
				this.aiIcons[0].SetActive(true);
			}
		}
		else if (ModeSelection._instance.noOfRacers == ModeSelection.NoOfRacers.four)
		{
			UnityEngine.Debug.Log("fourrrrrrrrr");
			this.popup.transform.localPosition = new Vector3(200f, this.popup.transform.localPosition.y, this.popup.transform.localPosition.z);
			this.playerImage.gameObject.SetActive(true);
			for (int j = 0; j < this.aiIcons.Count; j++)
			{
				this.aiIcons[j].gameObject.SetActive(false);
				for (int k = 0; k < 3; k++)
				{
					this.aiIcons[k].SetActive(true);
				}
			}
		}
		else if (ModeSelection._instance.noOfRacers == ModeSelection.NoOfRacers.six)
		{
			this.popup.transform.localPosition = new Vector3(0f, this.popup.transform.localPosition.y, this.popup.transform.localPosition.z);
			this.playerImage.gameObject.SetActive(true);
			for (int l = 0; l < this.aiIcons.Count; l++)
			{
				this.aiIcons[l].gameObject.SetActive(false);
				for (int m = 0; m < 5; m++)
				{
					this.aiIcons[m].SetActive(true);
				}
			}
		}
		base.StartCoroutine(this.ConnectingEffect(1f));
		this.rotate = true;
		base.Invoke("StopLoadingImg", 2f);
	}

	private void Start()
	{
		base.InvokeRepeating("CheckPlayersConnectivity", 0f, 1f);
	}

	private void StopLoadingImg()
	{
		this.isConnected = true;
		this.playerLoadingImg.gameObject.SetActive(false);
		this.onlinePlayers = (UnityEngine.Object.FindObjectsOfType(typeof(OnlinePlayerDetails)) as OnlinePlayerDetails[]);
		this.ais = this.onlinePlayers.ToList<OnlinePlayerDetails>();
	}

	private IEnumerator ConnectingEffect(float delay)
	{
		while (!this.isConnected)
		{
			yield return new WaitForSeconds(this.delayToDots);
			this.playerConnectingTxt.text = "CONNECTING\n.";
			yield return new WaitForSeconds(this.delayToDots);
			this.playerConnectingTxt.text = "CONNECTING\n..";
			yield return new WaitForSeconds(this.delayToDots);
			this.playerConnectingTxt.text = "CONNECTING\n...";
		}
		yield return this.isConnected;
		this.playerConnectingTxt.text = "CONNECTED.";
		AudioClipManager.Instance.Play(2);
		this.playerConnectingTxt.color = new Color32(0, 118, 11, 225);
		yield break;
	}

	private void Update()
	{
		if (this.rotate)
		{
			this.playerLoadingImg.transform.Rotate(0f, 0f, -500f * Time.deltaTime);
		}
	}

	private void CheckPlayersConnectivity()
	{
		if (this.ais.Count == 0)
		{
			return;
		}
		if (ModeSelection._instance.noOfRacers == ModeSelection.NoOfRacers.two)
		{
			if (this.ais[0].isConnected)
			{
				this.canStartGame = true;
				LevelSelection._instance.LoadIngame();
				return;
			}
		}
		else if (ModeSelection._instance.noOfRacers == ModeSelection.NoOfRacers.four)
		{
			if (this.ais[0].isConnected && this.ais[1].isConnected && this.ais[2].isConnected)
			{
				this.canStartGame = true;
				LevelSelection._instance.LoadIngame();
				return;
			}
		}
		else if (ModeSelection._instance.noOfRacers == ModeSelection.NoOfRacers.six && this.ais[0].isConnected && this.ais[1].isConnected && this.ais[2].isConnected && this.ais[3].isConnected && this.ais[4].isConnected)
		{
			this.canStartGame = true;
			LevelSelection._instance.LoadIngame();
			return;
		}
	}

	public static Multiplayerpage _instance;

	public Image playerImage;

	public Text playerName;

	public Text playerConnectingTxt;

	public GameObject playerLoadingImg;

	public GameObject popup;

	public List<GameObject> aiIcons = new List<GameObject>();

	public List<Text> aiNames = new List<Text>();

	private bool isConnected;

	public List<OnlinePlayerDetails> ais = new List<OnlinePlayerDetails>();

	private int connectedPlayers;

	private float delayToDots = 0.3f;

	private bool canStartGame;

	private bool rotate;

	private OnlinePlayerDetails[] onlinePlayers;
}
