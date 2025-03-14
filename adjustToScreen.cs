using System;
using UnityEngine;

public class adjustToScreen : MonoBehaviour
{
	private void Awake()
	{
	}

	private void Start()
	{
		base.transform.position = new Vector3(UIResolutionSet.newPosX(base.transform.position.x), base.transform.position.y, base.transform.position.z);
		if (this.ShouldScale)
		{
			Vector3 localScale = new Vector3(UIResolutionSet.newScaleValue(base.transform.localScale.x), base.transform.localScale.y, base.transform.localScale.z);
			base.transform.localScale = localScale;
		}
		else if (this.scalexy)
		{
			Vector3 localScale2 = new Vector3(UIResolutionSet.newScaleValue(base.transform.localScale.x), UIResolutionSet.newScaleValue(base.transform.localScale.y), base.transform.localScale.z);
			base.transform.localScale = localScale2;
		}
	}

	private void Update()
	{
	}

	public bool ShouldScale;

	public bool scalexy;
}
