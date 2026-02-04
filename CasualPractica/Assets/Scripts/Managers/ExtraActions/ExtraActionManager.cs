using UnityEngine;

public class ExtraActionManager
{
    public ExtraActionManager()
    {
        passiveManager = new(this);
        additionalTurnManager = new();
    }

    private PassiveManager passiveManager;
    private AdditionalTurnManager additionalTurnManager;

    public PassiveManager PassiveManager {  get { return passiveManager; } }
    public AdditionalTurnManager AdditionalTurnManager { get {return additionalTurnManager; } }
}
