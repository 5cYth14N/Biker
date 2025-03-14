using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchRegion : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IEventSystemHandler
{
	private void Awake()
	{
		TouchRegion.Instance = this;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		this.ControlsPressed = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		this.ControlsPressed = false;
		this.ControlsReleased = true;
	}

	private void Update()
	{
		#if UNITY_EDITOR
		if (InputHelper.GetTouches().Count <= 1)
		{
			this.ControlsReleased = false;
		}
		
		#else
if (UnityEngine.Input.touchCount <= 1)
		{
			this.ControlsReleased = false;
		}
#endif
		
	}

	public static TouchRegion Instance;

	public bool ControlsPressed;

	public bool ControlsReleased;

	public bool Enter;
}
