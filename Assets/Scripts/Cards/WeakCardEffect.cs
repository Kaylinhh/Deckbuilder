using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/Effects/WeakEffect")]
public class WeakCardEffect : CardEffect
{
    public StatusEffect weakStatus;
    public int duration;

    public override void Execute(CombatContext context)
    {
        StatusEffect instance = Instantiate(weakStatus);
        instance.duration = duration;
        context.enemy.ApplyStatus(instance);
    }

    public override string GetDescription(CombatContext context)
    {
        return $"Apply Weak for {duration} turns. Enemy deals 25% less damage.";
    }
}