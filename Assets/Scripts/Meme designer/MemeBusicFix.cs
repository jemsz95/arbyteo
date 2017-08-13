using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemeBusicFix : MonoBehaviour {

	private AudioSource _refComponent; 

	void Awake(){
		_refComponent = GetComponent<AudioSource> (); 
	}

	void Update(){
		if (_refComponent.time >= 4.35f) {
			_refComponent.time = 0; 
			_refComponent.Play (); 
		}

	}
}
