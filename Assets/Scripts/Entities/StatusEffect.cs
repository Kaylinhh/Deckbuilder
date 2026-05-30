using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    public string statusName;
    public Sprite icon;
    public int duration;

    public abstract void OnTurnEnd(Entity entity);

    public virtual int ModifyOutgoingDamage(int damage)
    {
        return damage;
    }

    public virtual string GetTooltip()
    {
        return statusName;;
    }
}