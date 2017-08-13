using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemeDesignerManager : MonoBehaviour {

	public int IMAGE_QUANTIY, TOP_TEXTS, BOTTOM_TEXTS;
	public Sprite[] Memes;

	public GameObject[] Buttons;

	public Text[] TopTexts;

	public Text[] BottomTexts;

	string[] sTopTexts = {
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

	string[] sBottomTexts = {
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
	int iImagePoolSize, iTopTextPoolSize, iBottomTextPoolSize;
	Sprite selectedImage;
	string sSelectedTopText, sSelectedBottomText;

	bool bImageSelected, bTopTextSelected, bBottomTextSelected;
	public Image MemeImage;
	public Text MemeTopText, MemeBottomText;
	int[] iSelectedIndexes = {0, 0, 0, 0, 0};

	Dictionary<string, bool> Combinations;
	public Image palomitaMeme, palomitaTopText, palomitaBottomText;

	void Awake() {
		ending += funPassReferenceToManager;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(coTutorial());
		MemeImage.GetComponent<Image>().overrideSprite = null;
		MemeImage.GetComponent<Image>().enabled = false;
		palomitaMeme.enabled = false;
		palomitaTopText.enabled = false;
		palomitaBottomText.enabled = false;
		MemeTopText.text = "";
		MemeTopText.enabled = false;
		MemeBottomText.text = "";
		MemeBottomText.enabled = false;
		iImagePoolSize = IMAGE_QUANTIY;
		iTopTextPoolSize = TOP_TEXTS;
		iBottomTextPoolSize = BOTTOM_TEXTS;
		funSelectImages();
		funSelectTopTexts();
		funSelectBottomTexts();
		bImageSelected = false;
		bTopTextSelected = false;
		bBottomTextSelected = false;
		Combinations = new Dictionary<string, bool>();
	}
	
	// Update is called once per frame
	void Update () {
		if(bImageSelected && bTopTextSelected && bBottomTextSelected) {
			StartCoroutine(coShowMeme());
			bImageSelected = false;
		}
	}

	void funSelectImages() {
		int index;
		// Reset pool size to pick images
		if(iImagePoolSize == 0) {
			iImagePoolSize = IMAGE_QUANTIY;
		}
		
		// Select 5 indexes of images from Memes array
		for(int i = 0; i < 5; i++) {
			// Obtain random index
			index = Random.Range(0, iImagePoolSize);
			// Assign image to button
			Buttons[i].GetComponent<Button>().interactable = true;
			Buttons[i].GetComponent<Image>().overrideSprite = Memes[index];
			// Move selected image to the end of the pool
			Sprite temp = Memes[iImagePoolSize - 1];
			Memes[iImagePoolSize - 1] = Memes[index];
			iSelectedIndexes[i] = iImagePoolSize - 1;
			// Swap last to be in pool
			Memes[index] = temp;
			// Decrease pool to erase duplicates
			iImagePoolSize--;
		}
	}

	void funSelectTopTexts() {
		int index;
		// Reset pool size to pick images
		if(iTopTextPoolSize == 0) {
			iTopTextPoolSize = TOP_TEXTS;
		}
		
		// Select 5 indexes of top texts from top texts array
		for(int i = 0; i < 5; i++) {
			// Obtain random index
			index = Random.Range(0, iTopTextPoolSize);
			// Assign text
			TopTexts[i].text = sTopTexts[index];
			TopTexts[i].transform.parent.GetComponent<Button>().interactable = true;
			// Move selected text to the end of the pool
			string temp = sTopTexts[iTopTextPoolSize - 1];
			sTopTexts[iTopTextPoolSize - 1] = sTopTexts[index];
			// Swap last to be in pool
			sTopTexts[index] = temp;
			// Decrease pool to erase duplicates
			iTopTextPoolSize--;
		}
	}

	void funSelectBottomTexts() {
		int index;
		// Reset pool size to pick images
		if(iBottomTextPoolSize == 0) {
			iBottomTextPoolSize = BOTTOM_TEXTS;
		}
		
		// Select 5 indexes of top texts from top texts array
		for(int i = 0; i < 5; i++) {
			// Obtain random index
			index = Random.Range(0, iBottomTextPoolSize);
			// Assign text
			BottomTexts[i].text = sBottomTexts[index];
			BottomTexts[i].transform.parent.GetComponent<Button>().interactable = true;
			// Move selected text to the end of the pool
			string temp = sBottomTexts[iBottomTextPoolSize - 1];
			sBottomTexts[iBottomTextPoolSize - 1] = sBottomTexts[index];
			// Swap last to be in pool
			sBottomTexts[index] = temp;
			// Decrease pool to erase duplicates
			iBottomTextPoolSize--;
		}
	}

	public void funDisableImages(int iButtonIndex) {
		for(int i = 0; i < 5; i++) {
			if(i == iButtonIndex) {
				selectedImage = Memes[iSelectedIndexes[i]];
				palomitaMeme.GetComponent<RectTransform>().position = new Vector3(Buttons[i].transform.position.x + 50, Buttons[i].transform.position.y, 0);
				palomitaMeme.enabled = true;
			}
			Buttons[i].GetComponent<Button>().interactable = false;
		}
		bImageSelected = true;
	}

	public void funDisableTopTexts(int iTopTextIndex) {
		for(int i = 0; i < 5; i++) {
			TopTexts[i].transform.parent.GetComponent<Button>().interactable = false;
			if(i == iTopTextIndex) {
				sSelectedTopText = TopTexts[i].text;
				palomitaTopText.GetComponent<RectTransform>().position = new Vector3(TopTexts[i].transform.position.x + 100, TopTexts[i].transform.position.y, 0);
				palomitaTopText.enabled = true;
			}
		}
		bTopTextSelected = true;
	}

	public void funDisableBottomTexts(int iBottomTextIndex) {
		for(int i = 0; i < 5; i++) {
			BottomTexts[i].transform.parent.GetComponent<Button>().interactable = false;
			if(i == iBottomTextIndex) {
				sSelectedBottomText = BottomTexts[i].text;
				palomitaBottomText.GetComponent<RectTransform>().position = new Vector3(BottomTexts[i].transform.position.x + 100, BottomTexts[i].transform.position.y, 0);
				palomitaBottomText.enabled = true;
			}
		}
		bBottomTextSelected = true;
	}

	IEnumerator coShowMeme() {
		MemeImage.GetComponent<Image>().enabled = true;
		MemeImage.GetComponent<Image>().overrideSprite = selectedImage;
		MemeTopText.enabled = true;
		MemeBottomText.enabled = true;
		MemeTopText.text = sSelectedTopText;
		MemeBottomText.text = sSelectedBottomText;
		yield return new WaitForSeconds(2);
		funReset();
	}

	void funReset() {
		string keyName = MemeImage.GetComponent<Image>().name + MemeTopText.text + MemeBottomText.text;
		if(!Combinations.ContainsKey(keyName)) {
			// Add points in using game manager
			Combinations.Add(keyName, true);
		}
		else {
			// Low score
		}
		MemeImage.GetComponent<Image>().enabled = false;
		MemeTopText.text = "";
		MemeBottomText.text = "";
		MemeTopText.enabled = false;
		MemeBottomText.enabled = false;
		bImageSelected = false;
		bTopTextSelected = false;
		bBottomTextSelected = false;
		palomitaMeme.enabled = false;
		palomitaTopText.enabled = false;
		palomitaBottomText.enabled = false;
		funSelectImages();
		funSelectTopTexts();
		funSelectBottomTexts();
	}

	IEnumerator coTutorial() {
		// Insert dialog text
		while(!Input.GetKeyDown(KeyCode.Space)) {
			yield return new WaitForSeconds(Time.fixedDeltaTime);
		}
	}

	public delegate void levelEvent();
	public static event levelEvent ending;

	void funPassReferenceToManager() {
		// Aqui pasar datos
	}
}
