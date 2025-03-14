using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeController : MonoBehaviour
{
	private void Start()
	{
		this.enableControlls = false;
		this.dirtParticleEffect.SetActive(false);
		this.needCharAnimation = false;
		this.stuntController = base.GetComponent<StuntController>();
		if (this.controltype == BikeController.controlType.PLAYER)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UICamera(Controls)")) as GameObject;
			this.UICamera = gameObject.GetComponent<Camera>();
		}
		BikeController.instance = this;
		this.body = base.GetComponent<Rigidbody2D>();
		this.body.centerOfMass = new Vector2(this.COM.localPosition.x, this.COM.localPosition.y);
		this.wheelradius = this.frontwheel.transform.GetComponent<CircleCollider2D>().radius;
		this.holded = 0;
		this.IsMobile = true;
		JointSuspension2D suspension = base.GetComponents<WheelJoint2D>()[0].suspension;
		JointSuspension2D suspension2 = base.GetComponents<WheelJoint2D>()[1].suspension;
		suspension.angle = 90f;
		suspension2.angle = 90f;
		base.GetComponents<WheelJoint2D>()[0].suspension = suspension;
		base.GetComponents<WheelJoint2D>()[1].suspension = suspension2;
		if (this.controltype == BikeController.controlType.PLAYER)
		{
			this.bikeParts.Add(this.frontwheel.gameObject);
			this.bikeParts.Add(this.backwheel.gameObject);
			this.bikeParts.Add(this.bikeBody.gameObject);
		}
	}

	public void DestroyBike()
	{
		this.fireEffect = GameManager._instance.fireEffect;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.fireEffect, this.body.transform.position, Quaternion.identity);
		gameObject.SetActive(true);
		this.bikeDestroyPos = this.bikeBody.transform.position;
		this.blastEff = true;
		this.instantiatedBikeBody = UnityEngine.Object.Instantiate<GameObject>(this.bikeBody, this.body.transform.position, Quaternion.identity);
		this.instantiatedBikeBody.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
		this.wheel1 = UnityEngine.Object.Instantiate<GameObject>(this.frontwheel.gameObject, this.body.transform.position, Quaternion.identity);
		this.wheel1.transform.rotation = this.frontwheel.transform.rotation;
		this.wheel2 = UnityEngine.Object.Instantiate<GameObject>(this.backwheel.gameObject, this.body.transform.position, Quaternion.identity);
		this.wheel2.transform.rotation = this.backwheel.transform.rotation;
		this.wheel1rb2D = this.wheel1.GetComponent<Rigidbody2D>();
		this.wheel2rb2D = this.wheel2.GetComponent<Rigidbody2D>();
		this.wheel1rb2D.mass = 1f;
		this.wheel2rb2D.mass = 1f;
		this.smoothfolow = (UnityEngine.Object.FindObjectOfType(typeof(SmoothFollow)) as SmoothFollow);
		this.smoothfolow.playerBike = this.instantiatedBikeBody;
		this.instantiatedBikeBody.AddComponent<Rigidbody2D>();
		this.instantiatedBikeBody.AddComponent<BoxCollider2D>();
		this.instantiatedBikeBody.GetComponent<BoxCollider2D>().size = new Vector2(0.1f, 0.1f);
		gameObject.transform.SetParent(this.instantiatedBikeBody.transform);
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.bikeBody.SetActive(false);
		this.frontwheel.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
		this.backwheel.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
		this.bikeManChar.SetActive(false);
		this.dummyChar.SetActive(true);
		this.dummyChar.transform.parent = null;
		this.dirtParticleEffect.SetActive(false);
		base.Invoke("RemoveBlastEff", 0.5f);
	}

	private void RemoveBlastEff()
	{
		this.blastEff = false;
		base.Invoke("EnableResettingImg", 2f);
	}

	private void EnableResettingImg()
	{
		GameManager._instance.resettingImg.SetActive(true);
		base.Invoke("ResetBike", 2f);
	}

	private void ResetBike()
	{
		this.bikeBody.SetActive(true);
		this.frontwheel.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
		this.backwheel.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
		this.bikeManChar.SetActive(true);
		this.dummyChar.SetActive(false);
		this.dirtParticleEffect.SetActive(true);
		base.transform.position = this.bikeDestroyPos;
		base.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		this.smoothfolow.playerBike = base.gameObject;
		UnityEngine.Object.Destroy(this.wheel1, 0.01f);
		UnityEngine.Object.Destroy(this.wheel2, 0.01f);
		UnityEngine.Object.Destroy(this.instantiatedBikeBody, 0.01f);
		GameManager._instance.resettingImg.SetActive(false);
		this.isDestroy = false;
	}

	private void Update()
	{
		if (this.blastEff)
		{
			this.wheel1rb2D.AddForce(new Vector2(1f, -2f) * 10f);
			this.wheel2rb2D.AddForce(new Vector2(1f, 1f) * 10f);
			this.instantiatedBikeBody.GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, 0.1f) * 10f);
			this.instantiatedBikeBody.transform.Rotate(base.transform.forward * 350f * Time.deltaTime);
		}
	}

	private IEnumerator UnFreezAi(float delay)
	{
		yield return new WaitForSeconds(delay);
		this.aiStruckedCount = 0f;
		this.isAiStrucked = false;
		yield break;
	}

	private IEnumerator FreezPlayer(float delay)
	{
		if (!this.isFreez)
		{
			yield return new WaitForSeconds(delay);
			base.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			yield return new WaitForSeconds(0.1f);
			base.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			this.body.constraints = RigidbodyConstraints2D.FreezePositionX;
			this.isFreez = true;
		}
		yield break;
	}

	private void IdleOneTime()
	{
		this.stuntController.SittingIdle();
		this.idleOneTime = true;
	}

	private void StuntOneTime()
	{
		this.stuntController.LeanBackwardStunt();
		this.stuntonetime = true;
	}

	private void DestroyOnce()
	{
		this.DestroyBike();
		this.isDestroy = true;
	}

	private void FixedUpdate()
	{
		if (!RaceManager._instance.isRaceStarted)
		{
			this._horizontal = 0f;
			this._vertical = 0f;
			return;
		}
		if (!this.enableControlls)
		{
			base.StartCoroutine(this.FreezPlayer(0.6f));
			return;
		}
		Vector2 origin = this.frontwheel.transform.position - base.transform.up * (this.wheelradius + 0.01f);
		Vector2 origin2 = this.backwheel.transform.position - base.transform.up * (this.wheelradius + 0.01f);
		UnityEngine.Debug.DrawRay(this.backwheel.transform.position - base.transform.up * (this.wheelradius + 0.01f), Vector3.down);
		this.rayHit_front = Physics2D.Raycast(origin, -base.transform.up, this.wheelradius + 0.01f);
		this.rayHit_back = Physics2D.Raycast(origin2, -base.transform.up, this.wheelradius + 0.01f);
		this.BikeDirection = this.frontwheel.position - this.backwheel.position;
		this.BikeDirection.Normalize();
		this.angvel = -this._horizontal * 120f;
		if (!this.stuntController.isdoingStunt)
		{
			if (this.body.transform.localEulerAngles.z > 20f && this.body.transform.localEulerAngles.z < 315f)
			{
				if (!this.stuntonetime)
				{
					this.StuntOneTime();
				}
			}
			else if (!this.idleOneTime)
			{
				this.IdleOneTime();
			}
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
		{
			this.SetBoosterForce();
		}
		if (this.controltype == BikeController.controlType.AI && !this.aiCrashed)
		{
			if (this.isAiStrucked)
			{
				this.aiStruckedCount += Time.fixedDeltaTime;
				if (this.aiStruckedCount >= 3f)
				{
					this._vertical -= 0.1f;
					if (this._vertical > -1f)
					{
						this._vertical = -1f;
					}
					base.StartCoroutine(this.UnFreezAi(0.3f));
				}
			}
			if (this.difficulty == BikeController.Difficulty.easy)
			{
				this._vertical += 0.1f;
				if (this._vertical > 1f)
				{
					this._vertical = 1f;
				}
			}
			else if (this.difficulty == BikeController.Difficulty.medium)
			{
				this._vertical += 0.125f;
				if (this._vertical > 1.25f)
				{
					this._vertical = 1.25f;
				}
			}
			else if (this.difficulty == BikeController.Difficulty.hard)
			{
				this._vertical += 0.25f;
				if (this._vertical > 1.5f)
				{
					this._vertical = 1.5f;
				}
			}
			else if (this.difficulty == BikeController.Difficulty.extrahard)
			{
				this._vertical += 0.35f;
				if (this._vertical > 1.7f)
				{
					this._vertical = 1.7f;
				}
			}
			Quaternion rotation = base.transform.rotation;
			if (this.rayHit_back.collider != null)
			{
				if (this.angvel >= 0f)
				{
					this.ApplyTorqueBike(this.speed);
					if (this.body.velocity.x <= 1f)
					{
						this.isAiStrucked = true;
					}
					else
					{
						this.isAiStrucked = false;
					}
				}
			}
			else
			{
				RaycastHit2D raycastHit2D = Physics2D.Raycast(this.rayCastPivote.transform.position, this.rayCastPivote.TransformDirection(Vector3.right), 40f);
				RaycastHit2D raycastHit2D2 = Physics2D.Raycast(this.rayCastPivote.transform.position, this.rayCastPivote.TransformDirection(Vector3.down), 40f);
				RaycastHit2D raycastHit2D3 = Physics2D.Raycast(this.rayCastPivote.transform.position, Quaternion.AngleAxis(-120f, this.rayCastPivote.transform.forward) * base.transform.up, 40f);
				if (raycastHit2D != null)
				{
					UnityEngine.Debug.DrawRay(this.rayCastPivote.transform.position, this.rayCastPivote.TransformDirection(Vector3.right) * 20f, Color.white);
				}
				if (raycastHit2D2 != null)
				{
					UnityEngine.Debug.DrawRay(this.rayCastPivote.transform.position, this.rayCastPivote.TransformDirection(Vector3.down) * 20f, Color.white);
				}
				if (raycastHit2D3 != null)
				{
					UnityEngine.Debug.DrawRay(this.rayCastPivote.transform.position, Quaternion.AngleAxis(-120f, this.rayCastPivote.transform.forward) * base.transform.up * 20f, Color.white);
				}
				if (raycastHit2D && raycastHit2D2 && raycastHit2D3 && raycastHit2D.collider && raycastHit2D2.collider && raycastHit2D3.collider)
				{
					if (this.difficulty == BikeController.Difficulty.easy)
					{
						this.body.AddTorque(500f);
					}
					else if (this.difficulty == BikeController.Difficulty.medium)
					{
						this.body.AddTorque(600f);
					}
					else if (this.difficulty == BikeController.Difficulty.hard)
					{
						this.body.AddTorque(700f);
					}
					else if (this.difficulty == BikeController.Difficulty.extrahard)
					{
						this.body.AddTorque(750f);
					}
				}
				else if (raycastHit2D3 && raycastHit2D2 && raycastHit2D3.collider && raycastHit2D2.collider)
				{
					if (this.difficulty == BikeController.Difficulty.easy)
					{
						this.body.AddTorque(200f);
					}
					else if (this.difficulty == BikeController.Difficulty.medium)
					{
						this.body.AddTorque(300f);
					}
					else if (this.difficulty == BikeController.Difficulty.hard)
					{
						this.body.AddTorque(400f);
					}
					else if (this.difficulty == BikeController.Difficulty.extrahard)
					{
						this.body.AddTorque(450f);
					}
				}
				else if (raycastHit2D2 && raycastHit2D2.collider)
				{
					if (this.difficulty == BikeController.Difficulty.easy)
					{
						this.body.AddTorque(-200f);
					}
					else if (this.difficulty == BikeController.Difficulty.medium)
					{
						this.body.AddTorque(-300f);
					}
					else if (this.difficulty == BikeController.Difficulty.hard)
					{
						this.body.AddTorque(-400f);
					}
					else if (this.difficulty == BikeController.Difficulty.extrahard)
					{
						this.body.AddTorque(-450f);
					}
				}
				else
				{
					base.transform.rotation = Quaternion.Slerp(rotation, Quaternion.Euler(0f, 0f, 0f), 1f * Time.deltaTime);
				}
				if (Physics2D.Raycast(this.rayCastPivote.transform.position, this.rayCastPivote.TransformDirection(Vector3.down), 10f))
				{
					UnityEngine.Debug.DrawRay(this.rayCastPivote.transform.position, this.rayCastPivote.TransformDirection(Vector3.down) * 10f, Color.blue);
					this.aiStunts = false;
				}
				else if (this.controltype == BikeController.controlType.AI && !this.aiStunts)
				{
					this.stuntController.PerformRandomStunt();
					this.aiStunts = true;
				}
			}
		}
		else if (this.controltype == BikeController.controlType.PLAYER)
		{
			if (this.holded == 1)
			{
				return;
			}
			
			#if UNITY_EDITOR
			if (InputHelper.GetTouches().Count > 0)
			{
				foreach (Touch touch in InputHelper.GetTouches())
				{
					Ray ray = this.UICamera.ScreenPointToRay(touch.position);
					if (Physics.Raycast(ray, out this.hit))
					{
						if (this.hit.transform.gameObject.name == "ForwardInput")
						{
							this._vertical += 0.1f;
							if (this._vertical > 1f)
							{
								this._vertical = 1f;
							}
						}
						if (this.hit.transform.gameObject.name == "BackwardInput")
						{
							this._vertical -= 0.1f;
							if (this._vertical < -1f)
							{
								this._vertical = -1f;
							}
						}
						if (this.hit.transform.gameObject.name == "LeftLean")
						{
							this._horizontal -= 0.1f;
							if (this._horizontal < -1f)
							{
								this._horizontal = -1f;
							}
						}
						if (this.hit.transform.gameObject.name == "RightLean")
						{
							this._horizontal += 0.1f;
							if (this._horizontal > 1f)
							{
								this._horizontal = 1f;
							}
						}
					}
				}
			}
			
			
			#else
if (UnityEngine.Input.touchCount > 0)
			{
				foreach (Touch touch in Input.touches)
				{
					Ray ray = this.UICamera.ScreenPointToRay(touch.position);
					if (Physics.Raycast(ray, out this.hit))
					{
						if (this.hit.transform.gameObject.name == "ForwardInput")
						{
							this._vertical += 0.1f;
							if (this._vertical > 1f)
							{
								this._vertical = 1f;
							}
						}
						if (this.hit.transform.gameObject.name == "BackwardInput")
						{
							this._vertical -= 0.1f;
							if (this._vertical < -1f)
							{
								this._vertical = -1f;
							}
						}
						if (this.hit.transform.gameObject.name == "LeftLean")
						{
							this._horizontal -= 0.1f;
							if (this._horizontal < -1f)
							{
								this._horizontal = -1f;
							}
						}
						if (this.hit.transform.gameObject.name == "RightLean")
						{
							this._horizontal += 0.1f;
							if (this._horizontal > 1f)
							{
								this._horizontal = 1f;
							}
						}
					}
				}
			}

#endif
			else
			{
				this._vertical = 0f;
				this._horizontal = 0f;
				this.onPress = false;
			}
			if (this.rayHit_back.collider != null)
			{
				if (this.angvel >= 0f)
				{
					this.ApplyTorqueBike(this.speed);
					this.dirtParticleEffect.SetActive(true);
					if (this.stuntController.isdoingStunt)
					{
						UnityEngine.Debug.LogError("show level fail because doing stunts on ground ");
						if (!this.isDestroy)
						{
							this.DestroyOnce();
						}
						Time.timeScale = 1f;
						return;
					}
				}
				GameManager._instance.stuntBtn.gameObject.SetActive(false);
				this.stuntBtnCount = 0;
			}
			else
			{
				this.dirtParticleEffect.SetActive(false);
				this.stuntBtnCount++;
				if (this.stuntBtnCount >= 15)
				{
					this.stuntBtnCount = 0;
					GameManager._instance.stuntBtn.gameObject.SetActive(true);
				}
			}
			if (this.angvel != 0f)
			{
				if (this.angvel > 0f)
				{
					this.body.angularVelocity = this.angvel;
				}
				else
				{
					if (this.rayHit_front.collider == null)
					{
						this.body.angularVelocity = this.angvel;
					}
					if (this.rayHit_back.collider != null)
					{
						this.ApplyTorqueBike(1500);
						if ((double)this.body.velocity.x <= 0.2)
						{
							this.body.angularVelocity = this.angvel;
						}
						this.frontwheel.AddTorque(-500f * this._vertical);
					}
					else if (this.rayHit_back.collider == null && (double)this.body.velocity.x <= 0.2)
					{
						this.body.angularVelocity = this.angvel;
					}
				}
			}
			if (!this.rayHit_back.collider || this._vertical == 0f)
			{
			}
			if (this.rayHit_back.collider == null)
			{
				this.backwheel.AddTorque(-100f * this._vertical);
			}
			this.FSuspension.transform.position = this.frontwheel.transform.position;
			Vector3 vector = this.backwheel.transform.position - this.BWheelRod.transform.position;
			Vector3 eulerAngles = this.BWheelRod.transform.eulerAngles;
			float num = Mathf.Atan2(vector.y, vector.x);
			eulerAngles.z = num * 57.29578f;
			this.BWheelRod.transform.eulerAngles = eulerAngles;
			if (this.Speed_txt != null)
			{
				this.Speed_txt.text = "Speed : " + this.body.velocity.x;
			}
			if (this.body.velocity.x >= 18f)
			{
				this.body.velocity = new Vector2(18f, this.body.velocity.y);
			}
		}
	}

	private void PerFormAiStunts()
	{
		this.stuntController.PerformRandomStunt();
	}

	private void removeForce()
	{
		this.forceAdding = 0f;
	}

	public void SetBoosterForce()
	{
		if (!this.Isbooster)
		{
			base.StartCoroutine(this.AddBForce());
			UnityEngine.Debug.Log("Booster___");
		}
	}

	public void ApplyTorqueBike(int force)
	{
		if (this.body.velocity.x <= 18f && this.body.velocity.x >= -15f)
		{
			this.body.AddForce(this._vertical * this.BikeDirection * (float)force);
		}
	}

	private IEnumerator AddBForce()
	{
		for (int i = 0; i < 200; i++)
		{
			if (this.rayHit_back.collider != null)
			{
				this.body.AddForce(this.BikeDirection * this.BoostSpeed);
				this.Isbooster = true;
			}
			yield return new WaitForSeconds(0.01f);
		}
		this.Isbooster = false;
		yield break;
	}

	public IEnumerator Addangularforce()
	{
		Time.timeScale = 0f;
		for (int i = 0; i < 100; i++)
		{
			if (this.rayHit_back.collider != null)
			{
				this.body.AddForce(base.transform.up * 12000f);
				this.body.AddForce(this.BikeDirection * 2000f);
			}
			yield return new WaitForSeconds(0.01f);
		}
		yield break;
	}

	public void AddExtraForce(int totalforce, float delay)
	{
		this.speed = totalforce;
		base.Invoke("ResetForce", delay);
	}

	private void ResetForce()
	{
		this.speed = 1500;
	}

	public static BikeController instance;

	public bool enableControlls;

	public BikeController.controlType controltype;

	private int speed = 1500;

	private Rigidbody2D body;

	public Rigidbody2D frontwheel;

	public Rigidbody2D backwheel;

	public WheelJoint2D backwheeljoint;

	public WheelJoint2D frontwheeljoint;

	public GameObject FSuspension;

	public GameObject BWheelRod;

	public Transform COM;

	public Camera UICamera;

	public float _horizontal;

	private float _vertical;

	private Vector2 BikeDirection;

	private RaycastHit hit;

	private float angvel;

	public GameObject bikeManChar;

	private float wheelradius;

	public int holded;

	public bool IsMobile;

	private RaycastHit2D rayHit_front;

	private RaycastHit2D rayHit_back;

	public GameObject dummyChar;

	public GameObject dirtParticleEffect;

	public GameObject bikeBody;

	[HideInInspector]
	public bool needCharAnimation;

	public Text Speed_txt;

	public float BoostSpeed = 1500f;

	private StuntController stuntController;

	[Header("-----------------FOR AI------------------")]
	public Transform rayCastPivote;

	public bool aiCrashed;

	public bool reachedFinish;

	public BikeController.Difficulty difficulty;

	public List<GameObject> bikeParts = new List<GameObject>();

	private Vector3 bikeDestroyPos;

	private Rigidbody2D wheel1rb2D;

	private Rigidbody2D wheel2rb2D;

	private GameObject instantiatedBikeBody;

	private SmoothFollow smoothfolow;

	private GameObject wheel1;

	private GameObject wheel2;

	private bool blastEff;

	private GameObject fireEffect;

	private bool onPress;

	private Vector3 objPos = Vector3.zero;

	private bool isAiStrucked;

	private float aiStruckedCount;

	[HideInInspector]
	public bool idleOneTime;

	[HideInInspector]
	public bool stuntonetime;

	[HideInInspector]
	public bool aiStunts;

	private bool isFreez;

	private int stuntBtnCount;

	private bool showStuntBtn;

	private bool isDestroy;

	private bool runOnce = true;

	private float forceAdding = 200f;

	private bool Isbooster;

	public enum controlType
	{
		PLAYER,
		AI
	}

	public enum Difficulty
	{
		easy,
		medium,
		hard,
		extrahard
	}
}
