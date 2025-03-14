using System;
using UnityEngine;

public class MoveFromScript : MonoBehaviour
{
	private void OnEnable()
	{
		base.transform.localPosition = new Vector2(-1000f, base.transform.localPosition.y);
		if (this.moveAxis == MoveFromScript.moveFromAxis.X)
		{
			iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
			{
				"x",
				this.moveTo,
				"islocal",
				true,
				"delay",
				this.moveDelay,
				"time",
				this.moveTime,
				"easetype",
				iTween.EaseType.easeInBack
			}));
		}
	}

	public MoveFromScript.moveFromAxis moveAxis;

	public float moveTo;

	public float moveDelay = 0.1f;

	public float moveTime = 0.5f;

	public bool canMove;

	public enum moveFromAxis
	{
		X,
		Y,
		Z
	}
}
