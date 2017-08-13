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
	public Text[] refIngredientsText, refOrderIngredientsText;
	private int[] _iIngredientsQty = {0, 0, 0, 0, 0};
	private int[] _iOrderIngredientQty = {0, 0, 0, 0, 0};
	private int _iScore;

	void Awake() {
		_bLevelStart = false;
		_fTime = 60;
		_iScore = 0;
		_refImageTime = GameObject.Find("Time Fill").GetComponent<Image>();
		_refTutorialBox = GameObject.Find("Tutorial Box");
		BaristaEnd += _funPassReferences;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(coTutorial());
		_funGenerateRandomOrder();
	}
	
	// Update is called once per frame
	void Update () {
		// Validate level start
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

	IEnumerator coTutorial() {
		// Wait for user to finish reading tutorial, use space bar
		while (true) {
			if(Input.GetKeyDown(KeyCode.Space)) {
				break; 
			} 
			else {
				_refTutorialBox.SetActive(true);  
			}
			yield return new WaitForSeconds (Time.deltaTime); 
		}
		_refTutorialBox.SetActive (false); 
		_bLevelStart = true;
	}

	void _funPassReferences() {
		// Aqui pasar datos
		_bLevelStart = false;
		_funDisableInteraction();
		GameObject manager = GameObject.Find("Game Manager"); 
		if (manager != null) {
			//Manager.Instance.something pasarle es score.  
		} else {
			Debug.Log ("Score");
		}
	}

	public void IncreaseQty(int index) {
		// Increse qty of ingredient, prevent more than 5 for simplicity
		if(_iIngredientsQty[index] >= 5) {
			return;
		}
		_iIngredientsQty[index]++;
		refIngredientsText[index].text = "" + _iIngredientsQty[index];
	}

	public void DecreaseQty(int index) {
		// Decrease qty of ingredient, preven negatives
		if(_iIngredientsQty[index] == 0) {
			return;
		}
		_iIngredientsQty[index]--;
		refIngredientsText[index].text = "" + _iIngredientsQty[index];
	}

	void _funGenerateRandomOrder() {
		// Generate order with random values for every ingredient
		for(int i = 0; i < 5; i++) {
			_iOrderIngredientQty[i] = Random.Range(0, 6);
			refOrderIngredientsText[i].text = "" + _iOrderIngredientQty[i];
		}
	}

	public void funServe() {
		// Validate order was served correctly
		bool bFlag = false;
		for(int i = 0; i < 5; i++) {
			if(_iIngredientsQty[i] != _iOrderIngredientQty[i]) {
				bFlag = true;
			}
			// Reset values of ingredients
			_iIngredientsQty[i] = 0;
			refIngredientsText[i].text = "" + _iIngredientsQty[i];
		}
		if(bFlag) {
			_funRandomReward();
		}
		else {
			_iScore += 30;
		}
		_funGenerateRandomOrder();
	}

	public void _funRandomReward() {
		int iLuck = Random.Range(0, 2);
		if(iLuck == 0) {
			_iScore -= 50;
		}
		else {
			_iScore += 50;
		}
	}

	void _funDisableInteraction() {
		for(int i = 0; i < 5; i++) {
			refIngredientsText[i].gameObject.transform.parent.GetChild(0).GetComponent<Button>().interactable = false;
			refIngredientsText[i].gameObject.transform.parent.GetChild(1).GetComponent<Button>().interactable = false;
		}
	}
}
