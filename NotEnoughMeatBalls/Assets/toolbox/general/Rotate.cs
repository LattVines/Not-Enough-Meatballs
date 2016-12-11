using UnityEngine;
using System.Collections;



public class Rotate : MonoBehaviour {


	public Vector3 _rotationAxis = new Vector3 (0,1,0);
	public float _rotationSpeed = 90;
	
	private Transform _transform;
	
	void Start () {
	
		// cache the transform
		_transform = transform;
	}
	
	
	void Update ()
	{
		_transform.Rotate (_rotationAxis * _rotationSpeed * Time.deltaTime);
	}
}

