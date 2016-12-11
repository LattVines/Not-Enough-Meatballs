using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class InvokeOnDestroy : MonoBehaviour {
	public UnityEvent invokeMethod;


	void OnDestroy(){
		invokeMethod.Invoke();
	}
}
