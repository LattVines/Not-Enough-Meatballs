using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("LoadRealScene", 3f);
	}

	void LoadRealScene(){
		SceneManager.LoadScene("main");
	}

}
