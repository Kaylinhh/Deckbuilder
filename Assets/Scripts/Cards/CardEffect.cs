using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    public abstract void Execute(CombatContext context);

    public virtual string GetDescription(CombatContext context)
    {
        return "";
    }
}
