using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/Effects/DamageEffect")]
public class DamageEffect : CardEffect
{
    public int damage;
    public override void Execute(CombatContext context)
    {
        context.enemy.TakeDamage(damage);
    }
}
