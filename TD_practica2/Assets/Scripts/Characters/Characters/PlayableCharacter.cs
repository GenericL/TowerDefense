using UnityEngine;

[CreateAssetMenu(fileName = "Scripts/ScriptableObjects/PlayableCharacter", menuName = "Character Data")]
public class PlayableCharacter : TestCharacter
{
    public float cost;

    public override void normalAttack()
    {
        //do damage
    }
}
