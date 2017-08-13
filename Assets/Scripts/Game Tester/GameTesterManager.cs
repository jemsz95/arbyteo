using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameTesterManager : Singleton<GameTesterManager> {

	private float _fLife, _fTime; 
	private Text _refTextTime, _refTextLife; 
	public delegate void levelEvent (); 
	public static event levelEvent GameTesterEnd; 

	void Awake(){
		_fLife = 100f; 
		_fTime = 60f; 
		GameTesterEnd += _funPassReferences; 
		_refTextTime = GameObject.Find ("Health Button"); 
		_refTextTime = GameObject.Find ("Send Button"); 
	}

	public void funAddTime(float famount){
		_fTime = Mathf.Clamp (_fTime += famount, 0, 100); 
	}

	public void funAddLife(float famount){
		_fLife = Mathf.Clamp (_fLife += famount, 0, 100); 
	}

	void Update(){
		if (_fLife <= 0 || _fTime <= 0) {
			GameTesterEnd (); 
		} else {
			_fLife -= Time.deltaTime; 
			_fTime -= Time.deltaTime; 
		}
		_refTextLife = _fLife; 
		_refTextTime = _fTime; 
	}

	void _funPassReferences(){
		GameManager manager = GameObject.FindObjectOfType<GameManager> (); 
		if(manager!=null){
			//give the manager the references; 
		}
	}




}
