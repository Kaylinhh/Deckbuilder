using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/Intents/AttackIntent")]
public class AttackIntent : EnemyIntent
{
    public int damage;

    public override void Execute(CombatContext context)
    {
        int modifiedDamage = context.enemy.GetModifiedDamage(damage);
        context.player.TakeDamage(modifiedDamage);
    }

    public override string GetDescription(CombatContext context)
    {
        int modifiedDamage = context.enemy.GetModifiedDamage(damage);
        return $"Attack {modifiedDamage}";
    }
}