using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/StatusEffects/StrengthEffect")]
public class StrengthEffect : StatusEffect
{
    public int bonusDamage;

    public override int ModifyOutgoingDamage(int damage)
    {
        return damage + bonusDamage; 
    }

    public override void OnTurnEnd(Entity entity)
    {
        duration--;
    }

    public override string GetTooltip()
    {
        return $"Strength: deals +{bonusDamage} damage per attack.";
    }
}