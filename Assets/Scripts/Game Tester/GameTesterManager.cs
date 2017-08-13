using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTesterManager : Singleton<GameTesterManager> {

	private float _fLife, _fTime; 
	[HideInInspector]
	public float fLife, fTime; 
	public delegate void levelEvent (); 
	public static event levelEvent GameTesterEnd; 

	void Awake(){
		_fLife = 100f; 
		_fTime = 60f; 
		GameTesterEnd += _funPassReferences; 
	}

	public void funAddTime(float famount){
		_fTime = Mathf.Clamp (_fTime += famount, 0, 100); 
	}

	public void funAddLife(float famount){
		_fLife = Mathf.Clamp (_fLife += famount, 0, 100); 
	}

	void Update(){
		fTime = _fTime - (_fTime % 1f);  
		Debug.Log (fTime); 
		if (_fLife <= 0 || _fTime <= 0) {
			GameTesterEnd (); 
		} else {
			_fLife--; 
			_fTime -= Time.deltaTime; 
		}
	}

	void _funPassReferences(){
		GameManager manager = GameObject.FindObjectOfType<GameManager> (); 
		if(manager!=null){
			//give the manager the references; 
		}
	}




}
