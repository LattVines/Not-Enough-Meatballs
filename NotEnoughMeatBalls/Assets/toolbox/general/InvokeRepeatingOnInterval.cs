using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class InvokeRepeatingOnInterval : MonoBehaviour {
	public UnityEvent invokeMethod;

	public bool is_invoking = true;
	public float delay = 1f;
	public float repeat_time = 5f;


	void Start () {
		InvokeRepeating("InvokeMethods", delay, repeat_time);
	}
	
	private void InvokeMethods()
	{
		invokeMethod.Invoke();
	}
}
