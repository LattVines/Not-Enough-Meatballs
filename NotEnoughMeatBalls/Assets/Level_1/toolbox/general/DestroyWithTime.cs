using UnityEngine;
using System.Collections;

public class DestroyWithTime : MonoBehaviour {

	public float delay_time = 3.3f;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, delay_time);

	}
	

}
