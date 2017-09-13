using UnityEngine;
using System.Collections;

public class DestroyAfterAnimation : MonoBehaviour {

	public AnimationClip clip_for_the_time;//Drag from folders in editor
	//only way to determine an animation clip (that I have found) is doing it this way.

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, clip_for_the_time.length);
	}
	

}
