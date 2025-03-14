using System;
using UnityEngine;
using UnityEngine.UI;

public class TestingScript2 : MonoBehaviour
{
	private void Start()
	{
		this.textt = TestingScript._instance.textt;
	}

	private void Update()
	{
	}

	public Text textt;
}
