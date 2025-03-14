using System;
using UnityEngine;

public class CharController : MonoBehaviour
{
	private void Start()
	{
		this.anim = base.GetComponent<Animator>();
		CharController.obj = this;
	}

	private void Randomanim()
	{
		int value = UnityEngine.Random.Range(0, 3);
		this.anim.SetInteger("CharPoseNo", value);
		this.currentPose = value;
		base.Invoke("Randomanim", 3f);
	}

	private void ResetAnim()
	{
		if (BikeController.instance.needCharAnimation)
		{
			this.anim.SetInteger("CharPoseNo", -1);
			this.currentPose = -1;
		}
	}

	public void LeanBackWard()
	{
		if (BikeController.instance.needCharAnimation)
		{
			this.anim.SetInteger("CharPoseNo", 2);
			this.currentPose = 2;
		}
	}

	public void LeanForward()
	{
		if (BikeController.instance.needCharAnimation)
		{
			this.anim.SetInteger("CharPoseNo", 1);
			this.currentPose = 1;
		}
	}

	public void LeanNormal()
	{
		if (BikeController.instance.needCharAnimation)
		{
			this.anim.SetInteger("CharPoseNo", 0);
			this.currentPose = 0;
		}
	}

	public void RidePose()
	{
		if (BikeController.instance.needCharAnimation)
		{
			this.anim.SetInteger("CharPoseNo", 3);
			this.currentPose = 3;
		}
	}

	private Animator anim;

	public static CharController obj;

	public int currentPose;
}
