using UnityEngine;
public class GameManager : Singleton<GameManager> {
    public ILevelState Level;
    
    protected GameManager() { }

    private void Start() {
        if(Level == null) {
            Level = new Level1();
        }

        Level.funDayStart();
    }
}