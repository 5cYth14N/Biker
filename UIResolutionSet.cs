using System;
using System.Collections.Generic;
using UnityEngine;

public class UIResolutionSet : MonoBehaviour
{
	private void Awake()
	{
		this.defaultAspectRatio = (float)Math.Round((double)(this.DefaultRes.x / this.DefaultRes.y), 3);
		if (!this.UICamera)
		{
			this.UICamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		}
		if (!this.UICamera)
		{
			UnityEngine.Debug.Log("No Camera found with the tag MainCamera");
			return;
		}
		if (!this.UICamera.orthographic)
		{
			UnityEngine.Debug.Log("It works for only OrthoGraphic Cameras");
			return;
		}
		this.currentAspectRatio = (float)Math.Round((double)this.UICamera.aspect, 3);
		this.screenHeight = this.UICamera.orthographicSize * 2f;
		this.screenWidth = this.screenHeight * this.currentAspectRatio;
		UIResolutionSet.isDefaultResolution = Mathf.Approximately(this.defaultAspectRatio, this.currentAspectRatio);
		UIResolutionSet.Multiplier = this.currentAspectRatio / this.defaultAspectRatio;
		if (!UIResolutionSet.isDefaultResolution)
		{
			for (int i = 0; i < this.UIObjects.Count; i++)
			{
				this.checkResolution(this.UIObjects[i]);
			}
		}
	}

	private void checkResolution(ObjectCustom objectCustom)
	{
		TransformProperty transformPropery = objectCustom.transformPropery;
		if (transformPropery != TransformProperty.POSITION)
		{
			if (transformPropery != TransformProperty.ROTATION)
			{
				if (transformPropery == TransformProperty.SCALE)
				{
					this.checkScale(objectCustom);
				}
			}
			else
			{
				this.checkRotation(objectCustom);
			}
		}
		else
		{
			this.checkPosition(objectCustom);
		}
	}

	private void checkPosition(ObjectCustom objectCustom)
	{
		float multiply = objectCustom.Multiply;
		Vector3 position = objectCustom.MainObject.transform.position;
		switch (objectCustom.direction)
		{
		case Direction.X:
		{
			position.x *= UIResolutionSet.Multiplier;
			float num = position.x * multiply * (1f / UIResolutionSet.Multiplier);
			position.x -= num;
			break;
		}
		case Direction.Y:
		{
			position.y *= UIResolutionSet.Multiplier;
			float num = position.y * multiply * (1f / UIResolutionSet.Multiplier);
			position.y -= num;
			break;
		}
		case Direction.Z:
		{
			position.z *= UIResolutionSet.Multiplier;
			float num = position.z * multiply * (1f / UIResolutionSet.Multiplier);
			position.z -= num;
			break;
		}
		case Direction.XY:
			this.checkPosition(objectCustom, Direction.X);
			this.checkPosition(objectCustom, Direction.Y);
			return;
		case Direction.YZ:
			this.checkPosition(objectCustom, Direction.Y);
			this.checkPosition(objectCustom, Direction.Z);
			return;
		case Direction.XZ:
			this.checkPosition(objectCustom, Direction.X);
			this.checkPosition(objectCustom, Direction.Z);
			return;
		case Direction.XYZ:
			this.checkPosition(objectCustom, Direction.X);
			this.checkPosition(objectCustom, Direction.Y);
			this.checkPosition(objectCustom, Direction.Z);
			return;
		}
		objectCustom.MainObject.transform.position = position;
	}

	private void checkPosition(ObjectCustom objectCustom, Direction direction)
	{
		float multiply = objectCustom.Multiply;
		Vector3 position = objectCustom.MainObject.transform.position;
		if (direction != Direction.X)
		{
			if (direction != Direction.Y)
			{
				if (direction == Direction.Z)
				{
					position.z *= UIResolutionSet.Multiplier;
					float num = position.z * multiply * (1f / UIResolutionSet.Multiplier);
					position.z -= num;
				}
			}
			else
			{
				position.y *= UIResolutionSet.Multiplier;
				float num = position.y * multiply * (1f / UIResolutionSet.Multiplier);
				position.y -= num;
			}
		}
		else
		{
			position.x *= UIResolutionSet.Multiplier;
			float num = position.x * multiply * (1f / UIResolutionSet.Multiplier);
			position.x -= num;
		}
		objectCustom.MainObject.transform.position = position;
	}

	public static Vector3 checkPosition(Vector3 position)
	{
		return position;
	}

	private void checkRotation(ObjectCustom objectCustom)
	{
		float multiply = objectCustom.Multiply;
		Vector3 eulerAngles = objectCustom.MainObject.transform.localRotation.eulerAngles;
		switch (objectCustom.direction)
		{
		case Direction.X:
		{
			eulerAngles.x *= UIResolutionSet.Multiplier;
			float num = eulerAngles.x * multiply * (1f / UIResolutionSet.Multiplier);
			eulerAngles.x -= num;
			break;
		}
		case Direction.Y:
		{
			eulerAngles.y *= UIResolutionSet.Multiplier;
			float num = eulerAngles.y * multiply * (1f / UIResolutionSet.Multiplier);
			eulerAngles.y -= num;
			break;
		}
		case Direction.Z:
		{
			eulerAngles.z *= UIResolutionSet.Multiplier;
			float num = eulerAngles.z * multiply * (1f / UIResolutionSet.Multiplier);
			eulerAngles.z -= num;
			break;
		}
		case Direction.XY:
			this.checkRotation(objectCustom, Direction.X);
			this.checkRotation(objectCustom, Direction.Y);
			return;
		case Direction.YZ:
			this.checkRotation(objectCustom, Direction.Y);
			this.checkRotation(objectCustom, Direction.Z);
			return;
		case Direction.XZ:
			this.checkRotation(objectCustom, Direction.X);
			this.checkRotation(objectCustom, Direction.Z);
			return;
		case Direction.XYZ:
			this.checkRotation(objectCustom, Direction.X);
			this.checkRotation(objectCustom, Direction.Y);
			this.checkRotation(objectCustom, Direction.Z);
			return;
		}
		objectCustom.MainObject.transform.localRotation = Quaternion.Euler(eulerAngles);
	}

	private void checkRotation(ObjectCustom objectCustom, Direction direction)
	{
		float multiply = objectCustom.Multiply;
		Vector3 eulerAngles = objectCustom.MainObject.transform.localRotation.eulerAngles;
		if (direction != Direction.X)
		{
			if (direction != Direction.Y)
			{
				if (direction == Direction.Z)
				{
					eulerAngles.z *= UIResolutionSet.Multiplier;
					float num = eulerAngles.z * multiply * (1f / UIResolutionSet.Multiplier);
					eulerAngles.z -= num;
				}
			}
			else
			{
				eulerAngles.y *= UIResolutionSet.Multiplier;
				float num = eulerAngles.y * multiply * (1f / UIResolutionSet.Multiplier);
				eulerAngles.y -= num;
			}
		}
		else
		{
			eulerAngles.x *= UIResolutionSet.Multiplier;
			float num = eulerAngles.x * multiply * (1f / UIResolutionSet.Multiplier);
			eulerAngles.x -= num;
		}
		objectCustom.MainObject.transform.localRotation = Quaternion.Euler(eulerAngles);
	}

	private void checkScale(ObjectCustom objectCustom)
	{
		float multiply = objectCustom.Multiply;
		Vector3 localScale = objectCustom.MainObject.transform.localScale;
		switch (objectCustom.direction)
		{
		case Direction.X:
		{
			localScale.x *= UIResolutionSet.Multiplier;
			float num = localScale.x * multiply * (1f / UIResolutionSet.Multiplier);
			localScale.x -= num;
			break;
		}
		case Direction.Y:
		{
			localScale.y *= UIResolutionSet.Multiplier;
			float num = localScale.y * multiply * (1f / UIResolutionSet.Multiplier);
			localScale.y -= num;
			break;
		}
		case Direction.Z:
		{
			localScale.z *= UIResolutionSet.Multiplier;
			float num = localScale.z * multiply * (1f / UIResolutionSet.Multiplier);
			localScale.z -= num;
			break;
		}
		case Direction.XY:
			this.checkScale(objectCustom, Direction.X);
			this.checkScale(objectCustom, Direction.Y);
			return;
		case Direction.YZ:
			this.checkScale(objectCustom, Direction.Y);
			this.checkScale(objectCustom, Direction.Z);
			return;
		case Direction.XZ:
			this.checkScale(objectCustom, Direction.X);
			this.checkScale(objectCustom, Direction.Z);
			return;
		case Direction.XYZ:
			this.checkScale(objectCustom, Direction.X);
			this.checkScale(objectCustom, Direction.Y);
			this.checkScale(objectCustom, Direction.Z);
			return;
		}
		objectCustom.MainObject.transform.localScale = localScale;
	}

	private void checkScale(ObjectCustom objectCustom, Direction direction)
	{
		float multiply = objectCustom.Multiply;
		Vector3 localScale = objectCustom.MainObject.transform.localScale;
		if (direction != Direction.X)
		{
			if (direction != Direction.Y)
			{
				if (direction == Direction.Z)
				{
					localScale.z *= UIResolutionSet.Multiplier;
					float num = localScale.z * multiply * (1f / UIResolutionSet.Multiplier);
					localScale.z -= num;
				}
			}
			else
			{
				localScale.y *= UIResolutionSet.Multiplier;
				float num = localScale.y * multiply * (1f / UIResolutionSet.Multiplier);
				localScale.y -= num;
			}
		}
		else
		{
			localScale.x *= UIResolutionSet.Multiplier;
			float num = localScale.x * multiply * (1f / UIResolutionSet.Multiplier);
			localScale.x -= num;
		}
		objectCustom.MainObject.transform.localScale = localScale;
	}

	public static Vector3 checkScale(Vector3 scale)
	{
		scale.x *= UIResolutionSet.Multiplier;
		scale.y *= UIResolutionSet.Multiplier;
		return scale;
	}

	public static float newScaleValue(float scaleValue)
	{
		scaleValue *= UIResolutionSet.Multiplier;
		return scaleValue;
	}

	public static float newPosX(float posX)
	{
		posX *= UIResolutionSet.Multiplier;
		return posX;
	}

	public Camera UICamera;

	public Vector2 DefaultRes;

	public List<ObjectCustom> UIObjects;

	private float screenHeight;

	private float screenWidth;

	private float currentAspectRatio;

	private float defaultAspectRatio;

	private static float Multiplier;

	private static bool isDefaultResolution;
}
