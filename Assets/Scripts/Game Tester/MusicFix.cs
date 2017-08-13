using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFix : MonoBehaviour {
	private AudioSource _refComponent; 

	void Awake(){
		_refComponent = GetComponent<AudioSource> (); 
	}

	void Update(){
		if (_refComponent.time >= 12.5f) {
			_refComponent.time = 0; 
			_refComponent.Play (); 
		}

	}
}
