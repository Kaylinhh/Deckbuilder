using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CombatUI : MonoBehaviour
{
    [Header("Player")]
    public Slider playerHPBar;
    public TextMeshProUGUI playerBlockText;
    public TextMeshProUGUI playerPAText;

    [Header("Enemy")]
    public Slider enemyHPBar;
    public TextMeshProUGUI enemyBlockText;
    public TextMeshProUGUI enemyIntentText;

    [Header("Deck")]
    public TextMeshProUGUI drawPileText;
    public TextMeshProUGUI discardPileText;

    [Header("Hand")]
    public Transform handPanel;
    public GameObject cardPrefab;
    public CombatManager combatManager;

    private void OnEnable()
    {
        CombatManager.OnStateChanged += Refresh; 
    }

    private void OnDisable()
    {
        CombatManager.OnStateChanged -= Refresh;  
    }

    public void UpdatePlayerUI(PlayerController player)
    {
        playerHPBar.maxValue = player.maxHP;
        playerHPBar.value = player.currentHP;
        playerBlockText.text = $"Block: {player.currentBlock}";
        playerPAText.text = $"PA: {player.currentPA}/{player.maxPA}";
    }

    public void UpdateEnemyUI(EnemyController enemy)
    {
        enemyHPBar.maxValue = enemy.maxHP;
        enemyHPBar.value = enemy.currentHP;
        enemyBlockText.text = $"Block: {enemy.currentBlock}";
        enemyIntentText.text = $"Next round: {enemy.currentIntent.description}";
    }

    public void UpdateDeckUI(DeckManager deck)
    {
        drawPileText.text = $"{deck.drawPile.Count}";
        discardPileText.text = $"{deck.discardPile.Count}";
    }

    private void Refresh(CombatContext context)
    {
        UpdatePlayerUI(context.player);
        UpdateEnemyUI(context.enemy);
        UpdateDeckUI(context.deck);
        RefreshHand(context.deck.hand);
    }

    private void RefreshHand(List<CardData> hand)
{
    // Destroy existing cards
    foreach (Transform child in handPanel)
        Destroy(child.gameObject);

    // Create a card for each CardData in the hand
    foreach (CardData cardData in hand)
    {
        GameObject cardGO = Instantiate(cardPrefab, handPanel);
        CardView cardView = cardGO.GetComponent<CardView>();
        cardView.Setup(cardData, (card) => combatManager.PlayCard(card));
    }
}
}