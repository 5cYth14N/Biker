using System;
using UnityEngine;

public class StuntController : MonoBehaviour
{
	private void Awake()
	{
		StuntController.instance = this;
	}

	private void Start()
	{
		this.anim = base.GetComponent<Animator>();
		this.SittingIdle();
		this.bikeController = base.GetComponent<BikeController>();
	}

	public void PerformRandomStunt()
	{
		if (this.bikeController.controltype == BikeController.controlType.AI)
		{
			this.isdoingStunt = false;
			int num = UnityEngine.Random.Range(1, 9);
			this.anim.SetTrigger("Stunt" + num);
		}
		else
		{
			this.isdoingStunt = true;
			int num2 = UnityEngine.Random.Range(1, 9);
			this.anim.SetTrigger("Stunt" + num2);
			UnityEngine.Debug.Log("performing stunt__" + this.isdoingStunt);
			this.SlowMotion();
		}
	}

	public void SittingIdle()
	{
		this.anim.SetTrigger("SittingIdle");
	}

	public void LeanForward()
	{
		this.anim.SetTrigger("LeanForward");
	}

	public void LeanBackwardStunt()
	{
		this.anim.SetTrigger("LeanBackward");
		base.Invoke("NormalizeCharAnim", 1f);
		base.Invoke("NormalizeCharAnim_Ai", 1f);
	}

	public void HalfIdle()
	{
		this.anim.SetTrigger("HalfIdle");
	}

	private void NormalizeCharAnim()
	{
		GameManager._instance.currentPlayerBike.GetComponent<BikeController>().idleOneTime = false;
		GameManager._instance.currentPlayerBike.GetComponent<BikeController>().stuntonetime = false;
	}

	private void NormalizeCharAnim_Ai()
	{
		base.GetComponentInParent<BikeController>().idleOneTime = false;
		base.GetComponentInParent<BikeController>().stuntonetime = false;
	}

	public void StuntEnd()
	{
		UnityEngine.Debug.Log("stunt Endddd");
		this.isdoingStunt = false;
		this.SittingIdle();
	}

	private void SlowMotion()
	{
		Time.timeScale = 0.4f;
		base.Invoke("NormalSpeed", 1f);
	}

	private void NormalSpeed()
	{
		Time.timeScale = 1f;
	}

	public static StuntController instance;

	public bool isdoingStunt;

	private Animator anim;

	private BikeController bikeController;
}
