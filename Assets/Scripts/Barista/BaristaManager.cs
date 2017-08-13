using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BaristaManager : MonoBehaviour {
	private float _fTime;
	private Image _refImageTime;
	private GameObject _refTutorialBox;
	private bool _bLevelStart;
	public delegate void levelEvent();
	public static levelEvent BaristaEnd;

	void Awake() {
		_bLevelStart = false;
		_fTime = 60;
		_refImageTime = GameObject.Find("Time Fill").GetComponent<Image>();
		_refTutorialBox = GameObject.Find("Tutorial Box");
		BaristaEnd += _funPassReferences;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(coTutorial());
	}
	
	// Update is called once per frame
	void Update () {
		if (_bLevelStart) {
			if (_fTime <= 0) {
				BaristaEnd();
			} 
			else {
				_fTime -= Time.deltaTime; 
			}
			_refImageTime.fillAmount = _fTime / 60f;
		}
	}

	IEnumerator coTutorial(){
		while (true) {

			if(Input.GetKeyDown(KeyCode.Space)){
				break; 
			}else{
				_refTutorialBox.SetActive (true);  
			}
			yield return new WaitForSeconds (Time.deltaTime); 
		}
		_refTutorialBox.SetActive (false); 
		_bLevelStart = true;
	}

	void _funPassReferences() {
		GameManager manager = GameObject.FindObjectOfType<GameManager> (); 
		if (manager != null) {
			//give the manager the references; 

		} else {
			//Debug.Log (_fScore); 
		}
	}
}
