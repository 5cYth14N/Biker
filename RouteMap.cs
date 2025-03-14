using System;
using System.Collections.Generic;
using UnityEngine;

public class RouteMap : MonoBehaviour
{
	private void Awake()
	{
	}

	private void Start()
	{
		base.Invoke("init", 0.2f);
	}

	private void init()
	{
		for (int i = 0; i < RaceManager._instance.currentAiBikes.Count; i++)
		{
			this.Bar_AI_car_1[i].SetActive(true);
		}
		if (this.Bar_Player_Car == null && GameObject.Find("Player_Car_Map"))
		{
			this.Bar_Player_Car = GameObject.Find("Player_Car_Map");
		}
		if (this.GameFinish == null && GameObject.Find("FinishCollider"))
		{
			this.GameFinish = GameObject.Find("FinishCollider");
			for (int j = 0; j < RaceManager._instance.currentAiBikes.Count; j++)
			{
				this.Aicar_1_TotalDis = this.CheckDistance(RaceManager._instance.currentAiBikes[j].transform, this.GameFinish.transform);
			}
			this.Player_TotalDis = this.CheckDistance(GameManager._instance.currentPlayerBike.transform, this.GameFinish.transform);
		}
		else
		{
			for (int k = 0; k < RaceManager._instance.allRacers.Count; k++)
			{
				this.Aicar_1_TotalDis = this.CheckDistance(RaceManager._instance.currentAiBikes[k].transform, this.GameFinish.transform);
			}
			this.Player_TotalDis = this.CheckDistance(GameManager._instance.currentPlayerBike.transform, this.GameFinish.transform);
		}
		this.AddCheckPointsToList();
	}

	private void AddCheckPointsToList()
	{
	}

	private float CheckDistance(Transform startposi, Transform Finishposi)
	{
		return Vector3.Distance(startposi.position, Finishposi.position);
	}

	private void Update()
	{
		if (this.GameFinish == null || RaceManager._instance.allRacers == null || GameManager._instance.currentPlayerBike == null || this.Aicar_1_TotalDis <= 0f || this.Player_TotalDis <= 0f || !RaceManager._instance.isRaceStarted)
		{
			return;
		}
		Vector3 position = this.Bar_Finish.transform.position;
		position.x = this.Bar_Finish.transform.position.x - this.Bar_Start.transform.position.x;
		for (int i = 0; i < RaceManager._instance.currentAiBikes.Count; i++)
		{
			float num = this.CheckDistance(RaceManager._instance.currentAiBikes[i].transform, this.GameFinish.transform) / this.Aicar_1_TotalDis;
			Vector3 vector = this.Bar_Start.transform.position + (1f - num) * position;
			this.Bar_AI_car_1[i].transform.position = new Vector3(vector.x, this.Bar_AI_car_1[i].transform.position.y, this.Bar_AI_car_1[i].transform.position.z);
		}
		float num2 = this.CheckDistance(GameManager._instance.currentPlayerBike.transform, this.GameFinish.transform) / this.Player_TotalDis;
		Vector3 vector2 = this.Bar_Start.transform.position + (1f - num2) * position;
		this.Bar_Player_Car.transform.position = new Vector3(vector2.x, this.Bar_Player_Car.transform.position.y, this.Bar_Player_Car.transform.position.z);
	}

	public GameObject Bar_Player_Car;

	public GameObject GameFinish;

	public GameObject Bar_Finish;

	public GameObject Bar_Start;

	private float Aicar_1_TotalDis;

	private float Player_TotalDis;

	public List<GameObject> Bar_AI_car_1 = new List<GameObject>();

	public Transform distanceCheckHolder;

	public List<Transform> distanceCheckPoints = new List<Transform>();

	private List<float> distances = new List<float>();
}
