using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/Intents/BlockIntent")]
public class BlockIntent : EnemyIntent
{
    public int block;

    public override void Execute(CombatContext context)
    {
        context.enemy.GainBlock(block);
    }

    public override string GetTooltip()
    {
        return $"Gains {block} block next turn";
    }

    public override string GetValue(CombatContext context)
    {
        return block.ToString();
    }
}