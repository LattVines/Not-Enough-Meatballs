using UnityEngine;
using System.Collections;

public class SFXController : MonoBehaviour {

	private static SFXController __instance__;
	AudioSource audio_Src;

	void Awake(){
		audio_Src = GetComponent<AudioSource>();
	}

	public static void PlayClip(params AudioClip[] clips)
	{
		if(__instance__ == null) 
		{
			__instance__ = FindObjectOfType<SFXController>();
		}

		if(SFXController.__instance__) __instance__.PlaySFX(clips);
	}

	public void PlaySFX(AudioClip clip)
	{
		if(audio_Src == null){
			audio_Src.GetComponent<AudioSource>();
		}
		audio_Src.PlayOneShot(clip);
	}



	public void  PlaySFX(params AudioClip[] clips)
	{
		if(audio_Src == null){
			audio_Src.GetComponent<AudioSource>();
		}
		int rand_index = Random.Range(0, clips.Length);
		audio_Src.PlayOneShot(clips[rand_index]);

	}



}


