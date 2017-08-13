using UnityEngine;
public class Level2 : MonoBehaviour, ILevelState {
    private DialogManager _dialogManager;

    public void funDayStart() {
        _dialogManager = GameObject.FindObjectOfType<DialogManager>();
        _dialogManager.funStartDialog(1, null);
    }

    public void funLunchStart() {
    }
    
    public void funLunchEnd() {
        
    }

    public void funDayEnd() {
        GameManager.Instance.funLoadLevelState(new GameObject().AddComponent<Level3>());
        Destroy(gameObject);
    }
}