using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Playables;

public class TeamManager
{
    private GameObject[] activeTeam;

    public TeamManager() { 
        activeTeam = new GameObject[4];
    }

    public void QuickSetupTeam(GameObject[] playables)
    {
        activeTeam = playables;
    }

    public void SetupPosition(GameObject playable, int position)
    {
        if (position < activeTeam.Length && position >= 0)
        {
            activeTeam[position] = playable;
        }
    }
    public void EliminateFromPosition(int position)
    {
        if (position < activeTeam.Length && position >= 0)
        {
            activeTeam[position] = null;
        }
    }
    public void ClearTeam()
    {
        activeTeam.ForEach(playable => playable = null);
    }
}
