using UnityEngine;
using System.Collections;

public class GameUnit : MonoBehaviour {

	public int health = 100;
	public int meatballs = 10;
	public int score = 0;
	public AudioClip[] sfx_damage, sfx_health;
	public AudioClip sfx_moreAmmo;
	public GameObject ammodrop;

	public void DealDamage(int dmg, GameUnit fromWho){

		health -= dmg;
		PlayRandomSFX(sfx_damage);
		if(health <= 0){
			fromWho.score++;

			GameObject.FindGameObjectWithTag("Player").GetComponent<GameUnit>().UpdateCanvasTextIfPlayer();

			if(fromWho.score >= 10){
				//somebody won. Who was it?
				if(fromWho.tag == "Player")
					GameController.GetInstance().GoToWinScreen();
				else
					GameController.GetInstance().GoToLoseScreen();
			}
			Death();
		}
		UpdateCanvasTextIfPlayer();
	}

	public void AddAmmo(int delta_amount)
	{
		//Debug.Log("add Ammo called");
		meatballs +=  delta_amount;
		PlaySingleSFX(sfx_moreAmmo);
		UpdateCanvasTextIfPlayer();
	}




	public void GiveHealth(int aid)
	{
		health += aid;
		PlayRandomSFX(sfx_health);
		UpdateCanvasTextIfPlayer();
	}

	void PlayRandomSFX(AudioClip[] sfx_array)
	{
		AudioSource audiosrc = GetComponent<AudioSource>();
		if(audiosrc != null)
		{ 
			int pick = Random.Range(0, sfx_array.Length);

			audiosrc.PlayOneShot(sfx_array[pick]);
		}

	}

	void PlaySingleSFX(AudioClip sfx_single)
	{
		AudioSource audiosrc = GetComponent<AudioSource>();
		if(audiosrc != null)
		{ 
			audiosrc.PlayOneShot(sfx_single);
		}

	}



	public void UpdateCanvasTextIfPlayer(){
		if(!(this.tag == "Player")) return;

		GameController game_ctrl = GameController.GetInstance();
		game_ctrl.meatballstext.text = meatballs.ToString();
		game_ctrl.healthtext.text = health.ToString();
		game_ctrl.scoretext.text = score.ToString();

	}


	public virtual void  Death()
	{
		//Debug.Log("parent death drops ammo");
		if(meatballs > 0){
			GameObject ammo_obj = Instantiate(ammodrop, this.transform.position, Quaternion.identity) as GameObject;
			ammo_obj.GetComponent<MeatBallCan>().ammo_count = meatballs;
		}
	}
}
