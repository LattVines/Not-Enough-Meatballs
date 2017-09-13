using UnityEngine;
using System.Collections;

public class MeatBall : MonoBehaviour {


	public GameUnit whoFireThisMeatBall;
	Collider collider;
	Rigidbody rigbod;
	public int meatBallDamage = 10;
	bool isArmed = true;
	public float reArmDelay = 5f;
	public GameObject ammo_prefab;//SET IN EDITOR

	void Awake()
	{

		collider = GetComponent<Collider>();
		rigbod = GetComponent<Rigidbody>();
	}

	void Start(){
		//Invoke("ReArm", 0.3f);
		Invoke("TurnIntoAmmo", 2.4f);
	}

	public void HandleGameUnitCollision(GameUnit thingToDamage)
	{
		thingToDamage.DealDamage(meatBallDamage, whoFireThisMeatBall);
	}




	void OnCollisionEnter(Collision collision) {
		if(isArmed && (collision.gameObject.tag == "Player" ||collision.gameObject.tag == "GameUnit" ))
		{
			isArmed = false;
			HandleGameUnitCollision(collision.gameObject.GetComponent<GameUnit>());
			Invoke("ReArm", reArmDelay);

		}
	}

	void ReArm(){
		isArmed = true;
	}

	void TurnIntoAmmo(){
		Instantiate(ammo_prefab, this.transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}






}
