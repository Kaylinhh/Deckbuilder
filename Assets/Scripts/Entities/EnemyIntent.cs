using UnityEngine;

public abstract class EnemyIntent : ScriptableObject
{
    public string description;
    public Sprite icon;

    public abstract void Execute(CombatContext context);

    public virtual string GetTooltip()
    {
        return description;
    }

    public virtual string GetValue(CombatContext context)
    {
        return "";
    }
}