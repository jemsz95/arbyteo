using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenuManager : MonoBehaviour {

	// Use this for initialization
	public void funExit() {
		Application.Quit();
	}

	public void funStart() {
		SceneManager.LoadScene(0);
	}
}
