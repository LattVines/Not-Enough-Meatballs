using UnityEngine;
using System.Collections;

public class PlayerInput : GameUnit {

	public GameObject projectile;//SET IN EDITOR
	public Transform shootSpot;//SET IN EDITOR
	public AudioClip sfx_shoot, sfx_empty,sfx_death;//SET IN EDITOR
	AudioSource sfx_src;
	public float projectile_speed = 50f;
	public float refire_delay = 1f / 3f; //3 shots a second or so
	bool isReady2Fire = true;





	void Awake(){
		sfx_src = GetComponent<AudioSource>();
	}




	// Update is called once per frame
	void Update () {
		if(isReady2Fire && Input.GetMouseButton(0)){
			isReady2Fire = false;
			ShootMeatBall();
		}
	}




	void ShootMeatBall()
	{
		if(meatballs > 0){
			meatballs--;
			GameController.GetInstance().meatballstext.text = meatballs.ToString();
			GameObject obj = Instantiate(projectile, shootSpot.position, Quaternion.identity ) as GameObject;
		
			obj.GetComponent<Rigidbody>().velocity = projectile_speed * Camera.main.transform.forward;
			Physics.IgnoreCollision(obj.GetComponent<Collider>(), GetComponent<Collider>());

			obj.GetComponent<MeatBall>().whoFireThisMeatBall = this;

			sfx_src.PlayOneShot(sfx_shoot);
			Invoke("ReArmGun", refire_delay);
		}
		else {
			sfx_src.PlayOneShot(sfx_empty);
			GameController.GetInstance().ACTIVATEMEATBALLS();
			Invoke("ReArmGun", refire_delay);
		}
	}

	void ReArmGun(){
		isReady2Fire = true;
	}

	public override void Death ()
	{
		base.Death();
	
		meatballs = 5;
		health = 100;
		sfx_src.PlayOneShot(sfx_death);
		GameObject[] respawn_pnts = GameObject.FindGameObjectsWithTag("Respawn");
		this.transform.position = respawn_pnts[Random.Range(0, respawn_pnts.Length)].transform.position;

	}

}
