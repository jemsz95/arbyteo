using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTesterManager : Singleton<GameTesterManager> {

	private float _fLife, _fTime; 
	public delegate void levelEvent (); 
	public static event levelEvent GameTesterEnd; 

	void Awake(){
		_fLife = 100f; 
		_fTime = 60f; 
		GameTesterEnd += _funPassReferences; 
	}

	public void funAddTime(float famount){
		_fTime += famount; 
	}

	public void funAddLife(float famount){
		_fLife += famount; 
	}

	void Update(){
		if (_fLife <= 0 || _fTime <= 0) {
			GameTesterEnd (); 
		}
	}

	void _funPassReferences(){
		GameManager manager = GameObject.FindObjectOfType<GameManager> (); 
		if(manager!=null){
			//give the manager the references; 
		}
	}




}
