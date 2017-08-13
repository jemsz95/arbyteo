using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameTesterManager : Singleton<GameTesterManager> {

	private float _fLife, _fTime, _fScore; 
	private bool _bLevelStart; 
	private Image _refImgTime, _refImgLife; 
	private GameObject _refTutorialText; 
	public delegate void levelEvent (); 
	public static event levelEvent GameTesterEnd; 


	void Awake(){
		_fLife = 100f; 
		_fTime = 60f; 
		_bLevelStart = false; 
		GameTesterEnd += _funPassReferences; 
		_refImgLife= GameObject.Find ("Life Fill").GetComponent<Image>(); 
		_refImgTime = GameObject.Find ("Time Fill").GetComponent<Image> ();
		_refTutorialText = GameObject.Find ("Tutorial Box");  
	}

	void Start(){
		StartCoroutine (Tutorial ()); 
	}

	public void funAddScore(float famount){
		if (_fLife > 1) {
			_fScore += famount; 
		}
	}

	public void funAddLife(float famount){
		if (_fLife > 1 && _bLevelStart) {
			_fLife = Mathf.Clamp (_fLife += famount, 0, 100); 
		}
	}

	void Update(){
		if (_bLevelStart) {
			if (_fLife <= 0 || _fTime <= 0) {
				GameTesterEnd (); 
			} else {
				_fLife -= Time.deltaTime * 8; 
				_fTime -= Time.deltaTime; 
			}
			_refImgLife.fillAmount = _fLife / 100f; 
			_refImgTime.fillAmount = _fTime / 60f; 
		}
	}

	void _funPassReferences(){
		GameManager manager = GameManager.Instance; 
		if (manager != null) {
			manager.funFinishMinigame((int) _fScore);
		} else {
			Debug.Log (_fScore); 
		}
	}

	IEnumerator Tutorial(){
		while (true) {

			if(Input.GetButtonDown("Submit")){
				break; 
			}else{
				_refTutorialText.SetActive (true);  
			}
			yield return new WaitForSeconds (Time.deltaTime); 
		}
		_refTutorialText.SetActive (false); 
		_bLevelStart = true; 
	}




}
