using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour {

	public Text meatballstext, healthtext,  scoretext;
	public GameObject NotEnoughMeatBallsPanel, WinScreen, LoseScreen, HudScreen;//set in editor
	static GameController __instance__ = null;


	//Using mainly for sequencing between screens and updating UI stuff
	bool game_is_running = true;

	void Awake(){

		if(__instance__ == null){
			__instance__ = this;
		}
		else{
			Destroy(this.gameObject);
		}
	}



	public static GameController GetInstance(){
		return __instance__;
	}





	public void ACTIVATEMEATBALLS(){

		NotEnoughMeatBallsPanel.SetActive(true);
		Invoke("DeactivateMeatBalls", 5f);
	}



	public void DeactivateMeatBalls(){
		NotEnoughMeatBallsPanel.SetActive(false);
	}


	public void GoToWinScreen(){
		HudScreen.SetActive(false);
		WinScreen.SetActive(true);
		WinScreen.GetComponent<YouWinScreen>().final_score_text.text = GetFinalScoreString();
	}

	public void GoToLoseScreen(){
		HudScreen.SetActive(false);
		LoseScreen.SetActive(true);
		LoseScreen.GetComponent<YouWinScreen>().final_score_text.text = GetFinalScoreString();
	}



	public string GetFinalScoreString(){

		GameUnit[] game_units= FindObjectsOfType<GameUnit>();

		string score_string = string.Empty;

		for(int i=0; i < game_units.Length; i++)
		{
			if(i != 0) score_string = score_string  + "\n";
			string score2append = "" + game_units[i].name + "\t" + game_units[i].score;
			Debug.Log(score2append);
			score_string = score_string + score2append;
			if(game_units[i].tag != "Player")
				Destroy(game_units[i].gameObject);
			else{
				game_units[i].GetComponent<RigidbodyFirstPersonController>().enabled = false;
				game_units[i].GetComponent<PlayerInput>().enabled = false;
			}
		}

		return score_string;

	}




}
