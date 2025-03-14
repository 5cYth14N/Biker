using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionConverter : MonoBehaviour
{
	private void Start()
	{
		this.GetAllObjects();
		this.ConvertCollisionTo2D();
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void GetAllObjects()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject;
			MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
			if (component != null)
			{
				MeshCollider component2 = gameObject.GetComponent<MeshCollider>();
				if (component2 == null)
				{
					gameObject.AddComponent<MeshCollider>();
				}
				component2 = gameObject.GetComponent<MeshCollider>();
				this.MColliders.Add(component2);
				if (gameObject.transform.childCount == 1)
				{
					Collider2D component3 = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
					if (component3 == null)
					{
						UnityEngine.Debug.LogError("MISSING 2D COLLIDER -" + gameObject.name);
					}
					else
					{
						this.PCollisers.Add(component3);
					}
				}
			}
		}
	}

	public void ConvertCollisionTo3D()
	{
		if (this._in == "3D")
		{
			return;
		}
		Rigidbody2D component = base.GetComponent<Rigidbody2D>();
		if (component != null)
		{
			UnityEngine.Object.Destroy(component);
		}
		for (int i = 0; i < this.PCollisers.Count; i++)
		{
			this.PCollisers[i].gameObject.SetActive(false);
		}
		for (int j = 0; j < this.MColliders.Count; j++)
		{
			this.MColliders[j].enabled = true;
		}
		this._in = "3D";
	}

	public void ConvertCollisionTo2D()
	{
		if (this._in == "2D")
		{
			return;
		}
		for (int i = 0; i < this.PCollisers.Count; i++)
		{
			this.PCollisers[i].gameObject.SetActive(true);
		}
		for (int j = 0; j < this.MColliders.Count; j++)
		{
			this.MColliders[j].enabled = false;
		}
		this._in = "2D";
	}

	private void Update()
	{
	}

	private string _in = "2D";

	private List<Collider2D> PCollisers = new List<Collider2D>();

	private List<MeshCollider> MColliders = new List<MeshCollider>();
}
