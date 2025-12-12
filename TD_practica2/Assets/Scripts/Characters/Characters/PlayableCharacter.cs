using UnityEngine;

[CreateAssetMenu(fileName = "Scripts/ScriptableObjects/PlayableCharacter", menuName = "Character Data")]
public class PlayableCharacter : TestCharacter
{
    public int cost;

    public override void normalAttack()
    {
        //do damage
    }
    
    public int getCost()
    {
        return cost;
    }


}
