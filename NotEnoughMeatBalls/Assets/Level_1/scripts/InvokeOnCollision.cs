using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class InvokeOnCollision : MonoBehaviour {
	public UnityEvent invokeMethod;

	public bool isArmed = true;
	public float reArmDelay = 5f;

	void OnCollisionEnter(Collision collision) {
		if(isArmed)
		{
			isArmed = false;
			InvokeMethods();
			Invoke("ReArm", reArmDelay);
		}
	}

	private void InvokeMethods()
	{
		invokeMethod.Invoke();
	}


	void ReArm(){
		isArmed = true;
	}
}
