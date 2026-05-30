using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI descriptionText;
    public Button button;

    private CardData _cardData;

    public void Setup(CardData cardData, CombatContext context,System.Action<CardData> onPlay)
    {
        _cardData = cardData;
        cardNameText.text = cardData.cardName;
        costText.text = cardData.cost.ToString();
        descriptionText.text = cardData.GetFullDescription(context);        
        button.onClick.RemoveAllListeners();
        if (onPlay == null)
            button.interactable = false;
        else
            button.onClick.AddListener(() => onPlay(_cardData));
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}