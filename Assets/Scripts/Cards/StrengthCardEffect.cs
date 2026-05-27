using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/Effects/StrengthEffect")]
public class StrengthCardEffect : CardEffect
{
    public StatusEffect strengthStatus;
    public int duration;

    public override void Execute(CombatContext context)
    {
        StatusEffect instance = Instantiate(strengthStatus);
        instance.duration = duration;
        context.player.ApplyStatus(instance);
    }

    public override string GetDescription(CombatContext context)
    {
        return $"Gain {duration} turn(s) of +{((StrengthEffect)strengthStatus).bonusDamage} Strength.";
    }
}