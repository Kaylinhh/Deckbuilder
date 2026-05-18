using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/Intents/AttackIntent")]
public class AttackIntent : EnemyIntent
{
    public int damage;

    public override void Execute(CombatContext context)
    {
        context.player.TakeDamage(damage);
    }
}