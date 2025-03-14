using System;
using UnityEngine;

public class CollisionTester2D : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (base.gameObject.name == "coll_front_wheel")
		{
		}
	}

	private void OnCollisionStay2D()
	{
		if (base.gameObject.name == "coll_front_wheel")
		{
			CollisionTester2D.front_collided = true;
			if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("Game_"))
			{
				base.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
			}
			else
			{
				base.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			}
		}
		if (base.gameObject.name == "coll_rear_wheel")
		{
			CollisionTester2D.rear_collided = true;
		}
	}

	private void Update()
	{
		CollisionTester2D.front_collided = false;
	}

	public static bool front_collided;

	public static bool rear_collided;
}
