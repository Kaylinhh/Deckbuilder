using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckViewManager : MonoBehaviour
{
    public GameObject panel;
    public Transform content;
    public GameObject cardPrefab;
    public TextMeshProUGUI titleText;

    public void Show(List<CardData> cards, string title, CombatContext context, bool shuffle = false)
    {
        // clear previous cards
        foreach (Transform child in content)
            Destroy(child.gameObject);

        titleText.text = $"{title} ({cards.Count})";

        // shuffle if needed
        List<CardData> displayList = new List<CardData>(cards);
        if (shuffle)
            Shuffle(displayList);

        // Instantiate cards
        foreach (CardData card in displayList)
        {
            GameObject cardGO = Instantiate(cardPrefab, content);
            CardView cardView = cardGO.GetComponent<CardView>();
            cardView.Setup(card, context, null); // null = pas cliquable
        }

        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    private void Shuffle(List<CardData> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            CardData temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}