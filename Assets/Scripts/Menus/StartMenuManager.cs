using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour {

	
	public void funPlay() {
		//SceneManager.LoadScene(2);
	}

	public void funCredits() {
		SceneManager.LoadScene(1);
	}

	public void funExit() {
		Application.Quit();
	}
}
