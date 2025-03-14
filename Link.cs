using System;
using UnityEngine;

public class Link : MonoBehaviour
{
	public void OnDestroy()
	{
		if (this.removeMeFromList != null)
		{
			this.removeMeFromList(base.gameObject);
		}
	}

	public Action<GameObject> removeMeFromList;
}
