using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClickSound : MonoBehaviour {

	private AudioSource me; 

	void Awake(){
		me = this.gameObject.GetComponent<AudioSource> (); 
	}

	 public void clicked (){
		me.Play (); 
	}


}
