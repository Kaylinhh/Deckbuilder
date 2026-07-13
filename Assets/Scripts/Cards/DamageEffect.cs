using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/Effects/DamageEffect")]
public class DamageEffect : CardEffect
{
    public int damage;
    public int hits;

    public override void Execute(CombatContext context)
    {
        int modifiedDamage = context.player.GetModifiedDamage(damage);
        for (int i = 0; i < hits; i++)
        {
            context.enemy.TakeDamage(modifiedDamage);
        }
    }

    public override string GetDescription(CombatContext context)
    {
        int modifiedDamage = context.player.GetModifiedDamage(damage);
        string color = modifiedDamage > damage ? "green" : "black";
        return $"Deal <color={color}>{modifiedDamage}</color> damage.";
    }
}
