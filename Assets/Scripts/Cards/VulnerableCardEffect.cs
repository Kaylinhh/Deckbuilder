using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/Effects/VulnerableCardEffect")]
public class VulnerableCardEffect : CardEffect
{
    public StatusEffect vulnerableStatus;
    public int duration;

    public override void Execute(CombatContext context)
    {
        StatusEffect instance = Instantiate(vulnerableStatus);
        instance.duration = duration;
        context.enemy.ApplyStatus(instance);
    }

    public override string GetDescription(CombatContext context)
    {
        return $"Apply Vulnerable for {duration} turns.";
    }
}