using System;
using System.Collections;
using UnityEngine;

public class BikeCollisionTest : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (this.isCollided)
		{
			return;
		}
		if (base.transform.root.GetComponent<BikeController>().controltype == BikeController.controlType.PLAYER)
		{
			BikeController component = base.transform.root.GetComponent<BikeController>();
			if (!component.reachedFinish)
			{
				component.DestroyBike();
				AudioClipManager.Instance.Play(5);
			}
			this.isCollided = true;
		}
		else if (base.transform.root.GetComponent<BikeController>().controltype == BikeController.controlType.AI)
		{
			if (this.isCollided)
			{
				return;
			}
			BikeController component2 = base.transform.root.GetComponent<BikeController>();
			BikeCollisionTest.aiCrashedPos = component2.transform.position;
			this.isCollided = true;
			base.StartCoroutine(this.RegenerateAi(0f, component2.gameObject));
		}
	}

	private void DestroyAiBike(GameObject col)
	{
		UnityEngine.Object.Destroy(col.gameObject, 10f);
		UnityEngine.Debug.Log("destroy__");
	}

	private IEnumerator RegenerateAi(float delay, GameObject col)
	{
		yield return new WaitForSeconds(0f);
		Vector3 currentPosUp = new Vector3(col.transform.position.x, col.transform.position.y + 2f, col.transform.position.z);
		col.transform.position = currentPosUp;
		col.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		this.isCollided = false;
		yield break;
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if (this.isCollided)
		{
			return;
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
	}

	public static Vector3 aiCrashedPos;

	public bool isCollided;
}
