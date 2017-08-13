using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemeDesignerManager : MonoBehaviour {

	public int IMAGE_QUANTIY, TOP_TEXTS, BOTTOM_TEXTS;
	public Sprite[] refMemes;

	public GameObject[] refButtons;

	public Text[] refTopTexts;

	public Text[] refBottomTexts;

	string[] _sTopTexts = {
		"WINS A HOLIDAY",
		"IF AN ILLEGAL IMMIGRANT FOUGHT A CHILD MOLESTOR",
		"VISITS THE GAZA STRIP",
		"THIS IS WHERE I'D PUT FAITH IN SOCIETY",
		"HE'S GONNA NUKE THE U.S.",
		"EVER TRIED TO EAT A CLOCK?",
		"I USED TO BE ADDICTED TO SOAP",
		"NICE LOOKING GIRL WAVED AT ME EARLIER TODAY",
		"HOW I FELL ABOUT TODAY'S NEWS",
		"ONE DOES NOT SIMPLY WATCH THE EMOJI MOVIE",
		"WHICH CAME FIRST, CHICKEN OR EGG?",
		"I BROKE MY VACUUM CLEANER",
		"IF YOU CAN STOP TEXTING AND DRIVING",
		"GETS FIRED FROM HIS FIRST JOB",
		"FORGET SANTA",
		"TO BE OR NOT TO BE",
		"REMEBER WHEN MTV DIDN'T SUCK",
		"I DON'T ALWAYS MAKE A MEME",
		"EVERY TIME I SEE YOU",
		"WHEN YOU TRY TO TAKE A SELFIE WITH ONE HAND"
	};

	string[] _sBottomTexts = {
		"GUAM",
		"WOULD IT BE ALIENS VS PREDATORS?",
		"BECAUSE HE THINKS IT'S A STRIP CLUB",
		"IF I HAD ANY",
		"SEE, NOBODY CARES",
		"IT'S TIME CONSUMING",
		"BUT I'M CLEAN NOW",
		"I LET MY PUPPY PEE ON IT",
		"CHOKES ON IT AND DIES",
		"WITHOUT LOSING ALL FAITH IN HUMANITY",
		"ALIENS!",
		"FOR ONCE I OWN SOMETHING THAT DOESN'T SUCK",
		"THAT WOULD BE GREAT",
		"BUT WHEN I DO, IT'S LIT",
		"MY MIDDLE FINGER GETS A BONER",
		"BUT THAT'S NONE OF MY BUSINESS",
		"A FLY CAN'T BIRD",
		"AND PUSH IT OFF A CLIFF",
		"IN NORTH KOREA",
		"NOW I CAN ENJOY MY CUP OF COVFEFE"
	};

	// Size of pools changes to prevent duplicates
	int _iImagePoolSize, _iTopTextPoolSize, _iBottomTextPoolSize, _iScore;
	Sprite _selectedImage;
	string _sSelectedTopText, _sSelectedBottomText;

	bool _bImageSelected, _bTopTextSelected, _bBottomTextSelected, _bLevelStart;
	public Image refMemeImage;
	public Text refMemeTopText, refMemeBottomText;
	int[] _iSelectedIndexes = {0, 0, 0, 0, 0};

	Dictionary<string, bool> Combinations;
	public Image refPalomitaMeme, refPalomitaTopText, refPalomitaBottomText;
	private Image _refImageTime;
	private float _fTime;
	private GameObject _refTutorialText;
	public delegate void levelEvent();
	public static event levelEvent MemeDesignerEnd;

	void Awake() {
		_iScore = 0;
		MemeDesignerEnd += funPassReferenceToManager;
		_fTime = 60;
		_bLevelStart = false;
		_refTutorialText = GameObject.Find ("Tutorial Box");
		_refImageTime = GameObject.Find("Time Fill").GetComponent<Image>();
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(coTutorial());
		refMemeImage.GetComponent<Image>().overrideSprite = null;
		refMemeImage.GetComponent<Image>().enabled = false;
		refPalomitaMeme.enabled = false;
		refPalomitaTopText.enabled = false;
		refPalomitaBottomText.enabled = false;
		refMemeTopText.text = "";
		refMemeTopText.enabled = false;
		refMemeBottomText.text = "";
		refMemeBottomText.enabled = false;
		_iImagePoolSize = IMAGE_QUANTIY;
		_iTopTextPoolSize = TOP_TEXTS;
		_iBottomTextPoolSize = BOTTOM_TEXTS;
		funSelectImages();
		funSelectTopTexts();
		funSelectBottomTexts();
		_bImageSelected = false;
		_bTopTextSelected = false;
		_bBottomTextSelected = false;
		Combinations = new Dictionary<string, bool>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_bImageSelected && _bTopTextSelected && _bBottomTextSelected && _bLevelStart) {
			StartCoroutine(coShowMeme());
			_bImageSelected = false;
		}
		if (_bLevelStart) {
			if (_fTime <= 0) {
				MemeDesignerEnd();
			} else {
				_fTime -= Time.deltaTime * 1.5f; 
			} 
			_refImageTime.fillAmount = _fTime / 60f; 
		}
	}

	void funSelectImages() {
		int index;
		// Reset pool size to pick images
		if(_iImagePoolSize == 0) {
			_iImagePoolSize = IMAGE_QUANTIY;
		}
		
		// Select 5 indexes of images from refMemes array
		for(int i = 0; i < 5; i++) {
			// Obtain random index
			index = Random.Range(0, _iImagePoolSize);
			// Assign image to button
			refButtons[i].GetComponent<Button>().interactable = true;
			refButtons[i].GetComponent<Image>().overrideSprite = refMemes[index];
			// Move selected image to the end of the pool
			Sprite temp = refMemes[_iImagePoolSize - 1];
			refMemes[_iImagePoolSize - 1] = refMemes[index];
			_iSelectedIndexes[i] = _iImagePoolSize - 1;
			// Swap last to be in pool
			refMemes[index] = temp;
			// Decrease pool to erase duplicates
			_iImagePoolSize--;
		}
	}

	void funSelectTopTexts() {
		int index;
		// Reset pool size to pick images
		if(_iTopTextPoolSize == 0) {
			_iTopTextPoolSize = TOP_TEXTS;
		}
		
		// Select 5 indexes of top texts from top texts array
		for(int i = 0; i < 5; i++) {
			// Obtain random index
			index = Random.Range(0, _iTopTextPoolSize);
			// Assign text
			refTopTexts[i].text = _sTopTexts[index];
			refTopTexts[i].transform.parent.GetComponent<Button>().interactable = true;
			// Move selected text to the end of the pool
			string temp = _sTopTexts[_iTopTextPoolSize - 1];
			_sTopTexts[_iTopTextPoolSize - 1] = _sTopTexts[index];
			// Swap last to be in pool
			_sTopTexts[index] = temp;
			// Decrease pool to erase duplicates
			_iTopTextPoolSize--;
		}
	}

	void funSelectBottomTexts() {
		int index;
		// Reset pool size to pick images
		if(_iBottomTextPoolSize == 0) {
			_iBottomTextPoolSize = BOTTOM_TEXTS;
		}
		
		// Select 5 indexes of top texts from top texts array
		for(int i = 0; i < 5; i++) {
			// Obtain random index
			index = Random.Range(0, _iBottomTextPoolSize);
			// Assign text
			refBottomTexts[i].text = _sBottomTexts[index];
			refBottomTexts[i].transform.parent.GetComponent<Button>().interactable = true;
			// Move selected text to the end of the pool
			string temp = _sBottomTexts[_iBottomTextPoolSize - 1];
			_sBottomTexts[_iBottomTextPoolSize - 1] = _sBottomTexts[index];
			// Swap last to be in pool
			_sBottomTexts[index] = temp;
			// Decrease pool to erase duplicates
			_iBottomTextPoolSize--;
		}
	}

	public void funDisableImages(int iButtonIndex) {
		for(int i = 0; i < 5; i++) {
			if(i == iButtonIndex) {
				_selectedImage = refMemes[_iSelectedIndexes[i]];
				refPalomitaMeme.GetComponent<RectTransform>().position = new Vector3(refButtons[i].transform.position.x + 50, refButtons[i].transform.position.y, 0);
				refPalomitaMeme.enabled = true;
			}
			refButtons[i].GetComponent<Button>().interactable = false;
		}
		_bImageSelected = true;
	}

	public void funDisableTopTexts(int iTopTextIndex) {
		for(int i = 0; i < 5; i++) {
			refTopTexts[i].transform.parent.GetComponent<Button>().interactable = false;
			if(i == iTopTextIndex) {
				_sSelectedTopText = refTopTexts[i].text;
				refPalomitaTopText.GetComponent<RectTransform>().position = new Vector3(refTopTexts[i].transform.position.x + 100, refTopTexts[i].transform.position.y, 0);
				refPalomitaTopText.enabled = true;
			}
		}
		_bTopTextSelected = true;
	}

	public void funDisableBottomTexts(int iBottomTextIndex) {
		for(int i = 0; i < 5; i++) {
			refBottomTexts[i].transform.parent.GetComponent<Button>().interactable = false;
			if(i == iBottomTextIndex) {
				_sSelectedBottomText = refBottomTexts[i].text;
				refPalomitaBottomText.GetComponent<RectTransform>().position = new Vector3(refBottomTexts[i].transform.position.x + 100, refBottomTexts[i].transform.position.y, 0);
				refPalomitaBottomText.enabled = true;
			}
		}
		_bBottomTextSelected = true;
	}

	IEnumerator coShowMeme() {
		refMemeImage.GetComponent<Image>().enabled = true;
		refMemeImage.GetComponent<Image>().overrideSprite = _selectedImage;
		refMemeTopText.enabled = true;
		refMemeBottomText.enabled = true;
		refMemeTopText.text = _sSelectedTopText;
		refMemeBottomText.text = _sSelectedBottomText;
		yield return new WaitForSeconds(3);
		if(_fTime >= 0) {
			funReset();
		}
		else {
			_funDisableInteraction();
		}
	}

	void funReset() {
		string keyName = refMemeImage.GetComponent<Image>().name + refMemeTopText.text + refMemeBottomText.text;
		if(!Combinations.ContainsKey(keyName)) {
			// Add points in using game manager
			Combinations.Add(keyName, true);
			_iScore += 20;
		}
		else {
			_iScore -= 1000;
		}
		refMemeImage.GetComponent<Image>().enabled = false;
		refMemeTopText.text = "";
		refMemeBottomText.text = "";
		refMemeTopText.enabled = false;
		refMemeBottomText.enabled = false;
		_bImageSelected = false;
		_bTopTextSelected = false;
		_bBottomTextSelected = false;
		refPalomitaMeme.enabled = false;
		refPalomitaTopText.enabled = false;
		refPalomitaBottomText.enabled = false;
		funSelectImages();
		funSelectTopTexts();
		funSelectBottomTexts();
	}

	IEnumerator coTutorial() {
		while (true) {
			if(Input.GetButtonDown("Submit")){
				break; 
			}
			else {
				_refTutorialText.SetActive(true);  
			}
			yield return new WaitForSeconds (Time.deltaTime); 
		}
		_refTutorialText.SetActive(false);
		_bLevelStart = true;
	}

	void funPassReferenceToManager() {
		// Aqui pasar datos
		_bLevelStart = false;
		_funDisableInteraction();
		GameManager manager = GameManager.Instance; 
		if (manager != null) {
			manager.funFinishMinigame(_iScore);  
		} else {
			Debug.Log ("Score"); 
		}

	}

	void _funDisableInteraction() {
		for(int i = 0; i < 5; i++) {
			refButtons[i].GetComponent<Button>().interactable = false;
			refTopTexts[i].GetComponentInParent<Button>().interactable = false;
			refBottomTexts[i].GetComponentInParent<Button>().interactable = false;
		}
	}
}
