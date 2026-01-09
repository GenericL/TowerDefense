using System.Collections.Generic;
using UnityEngine;

public abstract class Playable : Character
{
    public abstract bool Basic(List<Enemy> targets, int principalTarget);
    public abstract bool Ability(List<Enemy> targets, int principalTarget);
    public abstract bool Definitive(List<Enemy> targets, int principalTarget);
}
