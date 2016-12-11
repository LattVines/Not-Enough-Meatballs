using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class InvokeWithTime : MonoBehaviour {
	public UnityEvent invokeMethod;

	public float delay_time = 5f;



	void Start () {
		Invoke("InvokeMethods", delay_time);
	}

	private void InvokeMethods()
	{
		invokeMethod.Invoke();
	}

}
