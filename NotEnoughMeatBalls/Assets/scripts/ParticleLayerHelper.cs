using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ParticleSystem))]
public class ParticleLayerHelper : MonoBehaviour {

	public int sort_layer = 0;
	void Awake(){
		Renderer rend =  GetComponent<ParticleSystem>().GetComponent<Renderer>();
		rend.sortingLayerName = "Default";
		rend.sortingOrder = sort_layer;
	}
}
