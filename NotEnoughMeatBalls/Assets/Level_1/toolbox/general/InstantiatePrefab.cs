using UnityEngine;
using System.Collections;

public class InstantiatePrefab : MonoBehaviour {

	public GameObject prefab;//SET IN EDITOR

	bool spawn_at_prefab_Transform = false;

	public void InstantiateInstance()
	{
		if(spawn_at_prefab_Transform) Instantiate(prefab );
		else Instantiate(prefab, this.transform.position, Quaternion.identity );
	}

}
