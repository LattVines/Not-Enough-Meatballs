using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class InvokeOnAwake : MonoBehaviour {

	public UnityEvent invokeMethod;

	void Awake(){
		invokeMethod.Invoke();
	}

}
