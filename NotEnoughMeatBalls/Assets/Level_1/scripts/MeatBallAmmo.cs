using UnityEngine;
using System.Collections;

public class MeatBallAmmo : MonoBehaviour {

	public int ammo_count = 1;
	public GameObject particleSys;

	void Start(){
		Invoke("PlayAndDestroy", 10f);
	}

	void OnTriggerEnter(Collider other) {
		
		if(other.tag == "Player" || other.tag == "GameUnit"){
			other.gameObject.GetComponent<GameUnit>().AddAmmo(ammo_count);
			Destroy(this.gameObject);
		}

	}


	void PlayAndDestroy(){
		Instantiate(particleSys, this.transform.position, Quaternion.identity);
		Destroy(this.gameObject);//ten seconds to pick it up
	}

}
