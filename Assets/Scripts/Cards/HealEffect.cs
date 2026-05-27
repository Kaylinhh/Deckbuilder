using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/Effects/HealEffect")]
public class HealEffect : CardEffect
{
    public int healAmount;

    public override void Execute(CombatContext context)
    {
        context.player.currentHP = Mathf.Min(
            context.player.currentHP + healAmount,
            context.player.maxHP
        );
    }

    public override string GetDescription(CombatContext context)
    {
        return $"Heal {healAmount} HP.";
    }
}