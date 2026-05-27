using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/Intents/AttackIntent")]
public class AttackIntent : EnemyIntent
{
    public int damage;
    public int hits;

    public override void Execute(CombatContext context)
    {
        for (int i = 0; i < hits; i++)
        {
            int modifiedDamage = context.enemy.GetModifiedDamage(damage);
            context.player.TakeDamage(modifiedDamage);
        }
    }

    public override string GetDescription(CombatContext context)
    {
        int modifiedDamage = context.enemy.GetModifiedDamage(damage);
        if (hits > 1)
            return $"Attack {hits}x{modifiedDamage}";
        return $"Attack {modifiedDamage}";
    }
}