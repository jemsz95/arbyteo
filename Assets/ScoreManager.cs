using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text RefOldScore;
	public Text RefNewScore;
	public Text RefTotal;

	// Use this for initialization

	void Awake() {
		RefOldScore = GameObject.Find("Text (1)").GetComponent<Text>();
		RefNewScore = GameObject.Find("Text (2)").GetComponent<Text>();
		RefTotal = GameObject.Find("Text (3)").GetComponent<Text>();
	}

	void Update () {
		var gm = GameManager.Instance;

		if(gm != null) {
			RefOldScore.text = "$" + (gm.FCurrentMoney).ToString();
			RefNewScore.text = "+ $" + gm.FNewMoney;

			gm.FCurrentMoney += gm.FNewMoney;

			RefTotal.text = "$" + gm.FCurrentMoney;
		}
	}

	public void cont() {
		GameManager.Instance.funFinishResults();
	}
}
