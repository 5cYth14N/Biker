using System;
using UnityEngine;

public class AiCollisionTest : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		UnityEngine.Debug.Log(string.Concat(new string[]
		{
			" hit with ",
			col.transform.tag,
			" ",
			col.transform.name,
			" myName",
			base.transform.name,
			" ",
			base.transform.tag
		}));
		UnityEngine.Debug.Log("parent__" + base.transform.root);
		if (base.transform.root.GetComponent<BikeController>().controltype == BikeController.controlType.PLAYER)
		{
			BikeController.instance.DestroyBike();
		}
		else
		{
			UnityEngine.Debug.Log("parent__" + base.transform.root);
		}
	}
}
