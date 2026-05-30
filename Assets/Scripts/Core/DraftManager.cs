using System.Collections.Generic;
using UnityEngine;

public class DraftManager : MonoBehaviour
{
    public Transform cardsContainer;
    public GameObject cardPrefab;
    public GameObject cardRewardPanel;

    [Header("Card Pool")]
    public List<CardData> cardPool;

    public void ShowDraft(CombatContext context)
    {
        cardRewardPanel.SetActive(true);

        // Empty the container
        foreach (Transform child in cardsContainer)
            Destroy(child.gameObject);

        // Offer 3 random cards
        List<CardData> offered = GetRandomCards(3);
        foreach (CardData card in offered)
        {
            GameObject cardGO = Instantiate(cardPrefab, cardsContainer);
            CardView cardView = cardGO.GetComponent<CardView>();
            cardView.Setup(card, context, OnCardChosen);
        }
    }

    private List<CardData> GetRandomCards(int count)
    {
        List<CardData> pool = new List<CardData>(cardPool);
        List<CardData> result = new List<CardData>();

        for (int i = 0; i < count && pool.Count > 0; i++)
        {
            int index = Random.Range(0, pool.Count);
            result.Add(pool[index]);
            pool.RemoveAt(index);
        }
        return result;
    }

    public void OnCardChosen(CardData card)
    {
        GameManager.Instance.deck.Add(card);
        cardRewardPanel.SetActive(false);
    }

    public void OnSkip()
    {
        cardRewardPanel.SetActive(false);
    }
}