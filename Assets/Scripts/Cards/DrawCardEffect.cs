using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/Effects/DrawCardEffect")]
public class DrawCardEffect : CardEffect
{
    public int cardsToDraw;

    public override void Execute(CombatContext context)
    {
        context.deck.DrawCards(cardsToDraw);
    }

    public override string GetDescription(CombatContext context)
    {
        return $"Draw {cardsToDraw} card(s).";
    }
}