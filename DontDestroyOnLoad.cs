using System;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}
}
