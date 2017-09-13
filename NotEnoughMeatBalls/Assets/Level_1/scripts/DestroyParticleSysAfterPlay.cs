using UnityEngine;
using System.Collections;


[RequireComponent (typeof (ParticleSystem))]
public class DestroyParticleSysAfterPlay : MonoBehaviour {
	
	void Start () {
		Destroy( this.gameObject, GetComponent<ParticleSystem>().duration );
	}
	

}
