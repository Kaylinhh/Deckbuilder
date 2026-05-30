using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/StatusEffects/WeakEffect")]
public class WeakEffect : StatusEffect
{
    public float damageMultiplier = 0.75f;

    public override int ModifyOutgoingDamage(int damage)
    {
        return Mathf.RoundToInt(damage * damageMultiplier);
    }

    public override void OnTurnEnd(Entity entity)
    {
        duration--;
    }

    public override string GetTooltip()
    {
        return "Weak: deals 25% less damage.";
    }
}