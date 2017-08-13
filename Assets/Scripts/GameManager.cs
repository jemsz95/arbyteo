using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {
    public ILevelState RefLevel;
    public float FCurrentMoney;
    public float FNewMoney;
    public float FJobCost;

    [SerializeField]
    private int[] _JobScene;
    [SerializeField]
    private int[] _JobCost;
    [SerializeField]
    private bool[] _JobDone;

    public void funSelectOddJob(int job) {
        if(!_JobDone[job]) {
            RefLevel.funLunchStart();
            FJobCost = _JobCost[job];
            _JobDone[job] = true;
            SceneManager.LoadScene(_JobScene[job]);
        }
    }

    public void funFinishMinigame(int score) {
        SceneManager.LoadScene(1); // TODO: Check game scene number
        RefLevel.funLunchEnd();
        
        FNewMoney = score;
        SceneManager.LoadScene(1); //TODO: Check result scene number
    }

    public void funFinishResults() {
        RefLevel.funDayEnd();
    }

    public void funLoadLevelState(ILevelState lvl) {
        GameObject.DontDestroyOnLoad((RefLevel as MonoBehaviour).gameObject);
        SceneManager.LoadScene(1); // TODO: Check game scene number
        RefLevel = lvl;
        RefLevel.funDayStart();
    }
    
    protected GameManager() { }

    private void Start() {
        if(RefLevel == null) {
            RefLevel = new GameObject().AddComponent<Level1>();
            DontDestroyOnLoad((RefLevel as MonoBehaviour).gameObject);
        }

        RefLevel.funDayStart();
    }
}