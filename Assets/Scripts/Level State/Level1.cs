using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Level1 : MonoBehaviour, ILevelState {
    private DialogManager _dialogManager;
    private Animator[] _jobAnimator;

    public void funDayStart() {
        _dialogManager = GameObject.FindObjectOfType<DialogManager>();
        _dialogManager.funStartDialog(0, _funShowJobs);
    }

    public void funLunchStart() {
    }
    
    public void funLunchEnd() {
        
    }

    public void funDayEnd() {
        GameManager.Instance.funLoadLevelState(new GameObject().AddComponent<Level2>());
        Destroy(gameObject);
    }

    private void _funShowJobs() {
        var jobs = GameObject.FindGameObjectsWithTag("Jobs");
        
        _jobAnimator = new Animator[jobs.Length];
        
        foreach(var btnObj in GameObject.FindGameObjectsWithTag("JobButtons")) {
            btnObj.GetComponent<Button>().interactable = true;
        }

        for(int i = 0; i < jobs.Length; i++) {
            _jobAnimator[i] = jobs[i].GetComponent<Animator>();
        }

        StartCoroutine(coShowJobs());
    }

    private IEnumerator coShowJobs() {
        foreach(var anim in _jobAnimator) {
            anim.SetTrigger("Appear");
            yield return new WaitForSeconds(1f);
        }
    }
}