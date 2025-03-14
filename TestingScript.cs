using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingScript : MonoBehaviour
{
	private void Awake()
	{
		TestingScript._instance = this;
		this.values.Sort();
	}

	private void Start()
	{
		this.Shuffle();
		this.MakeRandomNumber();
	}

	private void Shuffle()
	{
	}

	private void MakeRandomNumber()
	{
		for (int i = 0; i < 4; i++)
		{
			this.newRandomNummer = (float)UnityEngine.Random.Range(0, this.myNumbers.Count);
			if (this.newRandomNummer != (float)i)
			{
				MonoBehaviour.print("new random number" + this.newRandomNummer);
			}
			else
			{
				MonoBehaviour.print("retry ");
			}
		}
	}

	public static TestingScript _instance;

	public Text textt;

	private int randomNum;

	public List<string> myNumbers = new List<string>();

	public List<int> saveRandomNumbers = new List<int>();

	public List<float> values = new List<float>();

	private float newRandomNummer;

	private float lastRandomNumber;

	private float min;

	private float max;
}
