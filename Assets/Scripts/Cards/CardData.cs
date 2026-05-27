using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    public Sprite artwork;
    public int cost;
    public CardEffect[] effects;
    [TextArea]
    public string description;

    public void Play(CombatContext context)
    {
        foreach (var effect in effects)
        {
            effect.Execute(context);
        }
    }

    public string GetFullDescription(CombatContext context)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (CardEffect effect in effects)
            sb.AppendLine(effect.GetDescription(context));
        return sb.ToString().TrimEnd();
    }
}