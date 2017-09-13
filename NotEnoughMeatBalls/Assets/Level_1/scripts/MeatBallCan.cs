using UnityEngine;
using System.Collections;

public class MeatBallCan : MonoBehaviour {
	
	public int ammo_count = 4;

	void OnTriggerEnter(Collider other) {

		if(other.tag == "Player" || other.tag == "GameUnit"){
			other.gameObject.GetComponent<GameUnit>().AddAmmo(ammo_count);
			Destroy(this.gameObject);
		}

	}


}
