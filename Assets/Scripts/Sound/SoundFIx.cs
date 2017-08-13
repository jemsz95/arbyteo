using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFIx : MonoBehaviour {

	private AudioSource _refComponent; 

	void Awake(){
		_refComponent = GetComponent<AudioSource> (); 
	}

	void Update(){
		if (_refComponent.time >= 13.45f) {
			_refComponent.time = 0; 
			_refComponent.Play (); 
		}
	
	}

}
