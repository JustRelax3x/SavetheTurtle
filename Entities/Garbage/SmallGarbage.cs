using UnityEngine;
internal class SmallGarbage : Garbage
{
    public override int GetDamage()
    {
        return GameConstants.SmallGarbageDamage;
    }
}

