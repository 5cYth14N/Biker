using System;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
	private void Start()
	{
		base.Invoke("SetPlayerBike", 0.1f);
		this.playerBike = GameManager._instance.currentPlayerBike;
	}

	private void SetPlayerBike()
	{
		this.playerBike = GameManager._instance.currentPlayerBike;
		this.setPlyaerBike = true;
	}

	private void LateUpdate()
	{
		if (!this.setPlyaerBike)
		{
			return;
		}
		base.transform.position = new Vector3(this.playerBike.transform.position.x, this.playerBike.transform.position.y + this.y_offSet, this.playerBike.transform.position.z - this.z_offSet);
	}

	public GameObject playerBike;

	public float z_offSet = 5f;

	public float y_offSet = 5f;

	public bool setPlyaerBike;
}
