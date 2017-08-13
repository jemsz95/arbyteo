using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TItleScreenMusicFix : MonoBehaviour {

	private AudioSource _refComponent; 

	void Awake(){
		_refComponent = GetComponent<AudioSource> (); 
	}

	void Update(){
		if (_refComponent.time >= 12f) {
			_refComponent.time = 0; 
			_refComponent.Play (); 
		}

	}
}
