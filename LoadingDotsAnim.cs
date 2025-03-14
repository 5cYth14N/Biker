using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingDotsAnim : MonoBehaviour
{
	private void Start()
	{
		base.StartCoroutine(this.LoadingAnim());
	}

	private IEnumerator LoadingAnim()
	{
		yield return new WaitForSeconds(0f);
		this.LoadingText.text = "Loading";
		yield return new WaitForSeconds(this.intervalTime);
		this.LoadingText.text = "Loading.";
		yield return new WaitForSeconds(this.intervalTime);
		this.LoadingText.text = "Loading..";
		yield return new WaitForSeconds(this.intervalTime);
		this.LoadingText.text = "Loading...";
		yield return new WaitForSeconds(this.intervalTime * 2f);
		base.StartCoroutine(this.LoadingAnim());
		yield break;
	}

	private void Update()
	{
		this.rotator.transform.Rotate(0f, 0f, -500f * Time.deltaTime, Space.World);
	}

	public Text LoadingText;

	public GameObject rotator;

	private float intervalTime = 0.2f;
}
