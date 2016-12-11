using UnityEngine;
using System.Collections;

public class BadGuy : GameUnit {


	NavMeshAgent agent;
	public GameObject projectile, death_explosion;//SET IN EDITOR

	AudioSource sfx_src;
	public AudioClip sfx_walking, sfx_shoot, sfx_death, sfx_taunt, sfx_no_ammo;

	public Transform shootSpot;//SET IN EDITOR
	public float projectile_speed = 50f;
	public float refire_delay = 1f / 3f; //3 shots a second or so
	bool isReady2Fire = true;


	bool is_Alive = true;
	bool is_moving_to_goal = false;
	float goal_update_delay = 1f;
	GameObject nav_goal = null;


	/*
	 I had a method to do this automatically without setting in
	 editor, but I kept getting a stalememref exception and an out of bounds error.
	 couln't figure it out and had no time to waste on it. :(
	 I will leave the FindEnemies method below. Maybe you can figure it out.
	*/
	public GameObject[] enemy_targets; //SETTING IN EDITOR TO AVOID PROBLEMS

	//special bools for handing AI
	bool needs_ammo = false;
	bool wants_to_shoot = false;
	bool needs_health = false;
	//---------------------------

	Transform player_trans;
	void Awake(){
		sfx_src = GetComponent<AudioSource>();
		agent = GetComponent<NavMeshAgent>();

		player_trans = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Start(){
		//enemy_targets = FindEnemies(); //whish this had worked. But it didn't
		Invoke("StartUpAI", 2f);//to prevent enemies firing right at scene load
	}

	void StartUpAI(){
		StartCoroutine(GoalUpdater());
	}



	// Update is called once per frame
	void Update () {
		
		if(isReady2Fire && wants_to_shoot ){
			isReady2Fire = false;
			AimAndShoot();
		}


	}



	void ReArmGun(){
		isReady2Fire = true;
	}

	IEnumerator GoalUpdater()
	{
		while(is_Alive){
			if(!is_moving_to_goal){
			//Debug.Log("looking for new goal");
			SetNavGoal();
			

			}
			else{
				//if still moving toward a goal, keep resetting it. because some objects may move
				agent.SetDestination(nav_goal.transform.position);
			}



			yield return new WaitForSeconds(goal_update_delay);
		}
		
	}

	public void SetNavGoal(){
		//Debug.Log("attempting to set goal");

		//decide what is needed
		//meatballs = 1, attack =3, health= 2
		int goal_pick = 0;


		if(meatballs < 5)
		{
			goal_pick = 1;
		}
		else goal_pick = 3;

		if(Random.value > 0.8f && meatballs > 2){
			goal_pick = 3;
		}
		/* this part doesn't work too well
		if(GoAfterPlayerWithVengeance()){
			Debug.Log("AFTER PLAYER!!!");
			goal_pick = 3;
		}
		*/
	



		//go to it


		//TEST 
		//goal_pick = 3;
		//TEST
	



		if(goal_pick == 1){
			GameObject[] ammos = FindAmmo();

			 
			 needs_ammo = true;
			if(ammos.Length > 0){
				bool is_going_for_can = false;
				//first try and go for cans

				//10% to want the can
				if(Random.value > 0.9f){
					for(int i=0; i <ammos.Length; i++){
						
						if(ammos[i].name.Contains("can")){
							nav_goal = ammos[i];
							agent.SetDestination(nav_goal.transform.position);
							is_going_for_can = true;
							wants_to_shoot = false;
						}
					}
				}

				//
				if(!is_going_for_can){
					nav_goal = FindClosestGameObject(ammos);
					agent.SetDestination(nav_goal.transform.position);
					wants_to_shoot = false;
				}

			}
		}


		else if(goal_pick == 3){
			nav_goal = FindClosestGameObject(enemy_targets);
			//agent.Stop();
			wants_to_shoot = true;
		}


	}


	public void ResetLookingForGoal(){
		//Debug.Log("reset looking for goal to false");
		is_moving_to_goal = false;
	}




	GameObject[] FindAmmo(){
		GameObject[] ammos = GameObject.FindGameObjectsWithTag("Ammo");
		return ammos;
	}



	GameObject[] FindEnemies(){

		//this has some weird problem. I don't know what it is.
		GameUnit[] enemies = FindObjectsOfType<GameUnit>();
		GameObject[] enemies_excluding_self = new GameObject[enemies.Length - 1];
		GameUnit gur;

		for(int i=0; i<enemies.Length; i++){
			
			gur = enemies[i].GetComponent<GameUnit>();
			if(gur.name == this.name)
			{
				Debug.Log("skip self");
			}
			else{
				
				enemies_excluding_self[i] = gur.gameObject;//it happens right here. But when I used debugger everything looks correct
			}

		}

		return enemies_excluding_self;
	}

	GameObject[] FindSpecialLocations(){
		return null;
	}



	public GameObject FindClosestGameObject(GameObject[] gameobjs)
	{
		GameObject closest = null;;
		float shortest_distance = 999999999f;
		foreach(GameObject obj in gameobjs)
		{
			float dist = Vector3.Distance(this.transform.position, obj.transform.position);
			if(dist < shortest_distance)
			{
				shortest_distance = dist;
				closest = obj;
			}
		}

		return closest;
	}



	public void AimAndShoot()
	{
		//this.transform.Rotate(Vector3.RotateTowards(this.transform.rotation, nav_goal.transform.position


		if(meatballs > 0){
			meatballs--;
			GameObject obj = Instantiate(projectile, shootSpot.position, Quaternion.identity ) as GameObject;

			obj.GetComponent<Rigidbody>().velocity = projectile_speed * GetAimingVector(shootSpot.position, nav_goal.transform.position);
			Physics.IgnoreCollision(obj.GetComponent<Collider>(), GetComponent<Collider>());
			obj.GetComponent<MeatBall>().whoFireThisMeatBall = this;

			sfx_src.PlayOneShot(sfx_shoot);
			Invoke("ReArmGun", refire_delay);
		}
		else {
			sfx_src.PlayOneShot(sfx_no_ammo);
			Invoke("ReArmGun", refire_delay);
		}
	}



	Vector3 GetAimingVector(Vector3 pnt, Vector3 tgt)
	{
		Vector3 aim_vec = tgt - pnt;
		//Vector3 bad_aim = new Vector3(Random.Range(0, 0.2f), Random.Range(0, 0.2f), Random.Range(0, 0.2f)) ;
		//aim_vec = aim_vec + bad_aim;
		//Debug.DrawRay(this.shootSpot.position, aim_vec, Color.green, 5f);
		return aim_vec.normalized;
	}

	public bool GoAfterPlayerWithVengeance()
	{
		nav_goal = GameObject.FindGameObjectWithTag("Player");
		return Random.value > 0.95f;
	}




	public override void Death ()
	{
		base.Death();
		sfx_src.PlayOneShot(sfx_death);

		meatballs = 10;
		health = 100;
		Instantiate(death_explosion, this.transform.position, Quaternion.identity);
		GameObject[] respawn_pnts = GameObject.FindGameObjectsWithTag("Respawn");
		this.transform.position = respawn_pnts[Random.Range(0, respawn_pnts.Length)].transform.position;
			
	}

}
