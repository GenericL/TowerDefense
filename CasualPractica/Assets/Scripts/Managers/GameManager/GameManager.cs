using UnityEngine;

public class GameManager
{
    private static GameManager instance = new GameManager();
    private GameState currentState;
    private GameManager()
    {}
    public static GameManager Instance {
        get { return instance; }
    }
    public GameState CurrentState
    {
        set {
            if (currentState != value) {
                currentState = value;

                switch (value) {
                    case GameState.MAIN_MENU:
                        // Play Intro here
                        break;
                default: 
                    // Do this when none of the cases above fit
                    break;
                }
            }
        }
        get { return currentState; }
    }

    private TeamManager teamManager = new TeamManager();
    public TeamManager TeamManager { get { return teamManager; } }

    /*public void Pause(bool paused)
    {
        if (paused)
        {
            // pause the game/physic
            Time.time = 0.0f;
        }
        else
        {
            // resume
            Time.time = 1.0f
        }
    }*/
}
