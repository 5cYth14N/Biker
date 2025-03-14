using System;
using UnityEngine;

[Serializable]
public class ObjectCustom
{
	public GameObject MainObject;

	public TransformProperty transformPropery;

	public Direction direction;

	[Range(-10f, 10f)]
	public float Multiply;
}
