using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsHolder : MonoBehaviour
{
	private void Awake()
	{
		SlotsHolder._instance = this;
		for (int i = 0; i < this.slotIcons.Count; i++)
		{
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public static SlotsHolder _instance;

	public List<Image> slotsHolder = new List<Image>();

	public List<Image> slotIcons = new List<Image>();
}
