using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/Effects/BlockEffect")]
public class BlockEffect : CardEffect
{
    public int block;
    public override void Execute(CombatContext context)
    {
        context.player.GainBlock(block);
    }
}
