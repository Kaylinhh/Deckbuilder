using UnityEngine;

public abstract class EnemyIntent : ScriptableObject
{
    public string description;
    public Sprite icon;

    public abstract void Execute(CombatContext context);

    public virtual string GetDescription(CombatContext context)
    {
        return description;
    }
}