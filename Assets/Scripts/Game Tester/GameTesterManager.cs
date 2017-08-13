using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameTesterManager : Singleton<GameTesterManager> {

	private float _fLife, _fTime, _fScore; 
	private Image _refImgTime, _refImgLife; 
	public delegate void levelEvent (); 
	public static event levelEvent GameTesterEnd; 

	void Awake(){
		_fLife = 100f; 
		_fTime = 60f; 
		GameTesterEnd += _funPassReferences; 
		_refImgLife= GameObject.Find ("Life Fill").GetComponent<Image>(); 
		_refImgTime = GameObject.Find ("Time Fill").GetComponent<Image> ();
	}

	public void funAddScore(float famount){
		if (_fLife > 1) {
			_fScore += famount; 
		}
	}

	public void funAddLife(float famount){
		if (_fLife > 1) {
			_fLife = Mathf.Clamp (_fLife += famount, 0, 100); 
		}
	}

	void Update(){
		if (_fLife <= 0 || _fTime <= 0) {
			GameTesterEnd (); 
		} else {
			_fLife -= Time.deltaTime * 8; 
			_fTime -= Time.deltaTime; 
		}
		_refImgLife.fillAmount = _fLife / 100f; 
		_refImgTime.fillAmount = _fTime / 60f;  
	}

	void _funPassReferences(){
		GameManager manager = GameObject.FindObjectOfType<GameManager> (); 
		if(manager!=null){
			//give the manager the references; 

		}
		Debug.Log(_fScore); 
	}




}
