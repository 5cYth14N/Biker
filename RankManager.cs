using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
	private void Awake()
	{
		RankManager.instance = this;
	}

	private void Start()
	{
		this.players = new Dictionary<string, Player>();
	}

	public void InsertNamesInTextField()
	{
		for (int i = 0; i < RaceManager._instance.allRacers.Count; i++)
		{
			this.txtRanks[i].text = i + 1 + " . " + RaceManager._instance.allRacers[i].GetComponent<BikerDetails>().name;
		}
	}

	public void SetRank(Player player)
	{
		this.players[player.name] = player;
		IOrderedEnumerable<KeyValuePair<string, Player>> orderedEnumerable = from x in this.players
		orderby x.Value.targetDistance
		orderby x.Value.distance
		select x;
		int num = 0;
		foreach (KeyValuePair<string, Player> keyValuePair in orderedEnumerable)
		{
			this.txtRanks[num].text = num + 1 + " . " + keyValuePair.Value.name;
			num++;
		}
	}

	public static RankManager instance;

	public Text[] txtRanks;

	private Dictionary<string, Player> players;

	private Dictionary<string, Player> sortedPlayers;
}
