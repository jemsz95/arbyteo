using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class Level3 : MonoBehaviour, ILevelState {
    private DialogManager _dialogManager;
    private Animator[] _jobAnimator;

    public void funDayStart() {
        _dialogManager = GameObject.FindObjectOfType<DialogManager>();
        _dialogManager.funStartDialog(2, _funShowJobs);
    }

    public void funLunchStart() {
    }
    
    public void funLunchEnd() {
        
    }

    public void funDayEnd() {
        Destroy(gameObject);
        // Game Over
        SceneManager.LoadScene(1);
    }

    private void _funShowJobs() {
        var jobs = GameObject.FindGameObjectsWithTag("Jobs");
        
        _jobAnimator = new Animator[jobs.Length];

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