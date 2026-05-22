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
}