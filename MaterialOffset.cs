using System;
using UnityEngine;

public class MaterialOffset : MonoBehaviour
{
	private void Start()
	{
		this.rampRenderer = base.GetComponent<Renderer>();
	}

	private void Update()
	{
		float x = Time.time * this.scrollSpeed;
		this.rampRenderer.material.SetTextureOffset("_MainTex", new Vector2(x, 0f));
	}

	public float scrollSpeed = 0.5f;

	public Renderer rampRenderer;
}
