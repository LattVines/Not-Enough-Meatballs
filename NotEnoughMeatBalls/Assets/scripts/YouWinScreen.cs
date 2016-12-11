using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class YouWinScreen : MonoBehaviour {

	public Text final_score_text;
	public Text restarterText;
	bool isCounting = true;

	int seconds_remaining = 15;

	void Start(){
		StartCoroutine(restarter());
	}

	IEnumerator restarter(){
		while(isCounting){
			yield return new WaitForSeconds(1f);
			if(seconds_remaining == 0){
				isCounting = false;
				SceneManager.LoadScene(0);

			}
			seconds_remaining--;
			restarterText.text = "game restarts in " + seconds_remaining.ToString();

		}
	}

}
