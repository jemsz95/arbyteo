using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class MemeDesignerManager : MonoBehaviour {

	public Sprite[] memes;

	public GameObject[] buttons;

	int iPoolSize;
	public int IMAGE_QUANTIY;

	// Use this for initialization
	void Start () {
		iPoolSize = IMAGE_QUANTIY;
		funSelectImages();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void funSelectImages() {
		int index;
		// Reset pool size to pick images
		if(iPoolSize == 0) {
			iPoolSize = IMAGE_QUANTIY;
		}
		
		// Select 5 indexes of images from memes array
		for(int i = 0; i < 5; i++) {
			// Obtain random index
			index = Random.Range(0, iPoolSize);
			// Assign image to button
			buttons[i].GetComponent<Image>().overrideSprite = memes[index];
			// Move selected image to the end of the pool
			Sprite temp = memes[iPoolSize - 1];
			memes[iPoolSize - 1] = memes[index];
			// Swap last to be in pool
			memes[index] = temp;
			// Decrease pool to erase duplicates
			iPoolSize--;
		}
	}

	public void funDisable(int iButtonIndex) {
		for(int i = 0; i < 5; i++) {
			buttons[i].GetComponent<Button>().interactable = false;
			buttons[i].GetComponent<Button>().enabled = false;
			if(i == iButtonIndex) {
				// Show selected
			}
		}
	}

}
