using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class CombatUI : MonoBehaviour
{
    [Header("Player")]
    public Slider playerHPBar;
    public TextMeshProUGUI playerBlockText;
    public TextMeshProUGUI playerAPText;
    public TextMeshProUGUI playerHPText;

    [Header("Enemy")]
    public Slider enemyHPBar;
    public TextMeshProUGUI enemyBlockText;
    public TextMeshProUGUI enemyIntentText;
    public TextMeshProUGUI enemyHPText;


    [Header("Deck")]
    public TextMeshProUGUI drawPileText;
    public TextMeshProUGUI discardPileText;

    [Header("Hand")]
    public Transform handPanel;
    public GameObject cardPrefab;
    public CombatManager combatManager;

    [Header("End Combat")]
    public GameObject blockerOverlay;
    public GameObject combatEndPanel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI buttonText;

    [Header("Feedback")]
    public TextMeshProUGUI notEnoughAPText;

    private void OnEnable()
    {
        CombatManager.OnStateChanged += Refresh; 
        CombatManager.OnCombatEnded += HandleCombatEnd;
        CombatManager.OnNotEnoughAP += HandleNotEnoughAP;
    }

    private void OnDisable()
    {
        CombatManager.OnStateChanged -= Refresh;  
        CombatManager.OnCombatEnded -= HandleCombatEnd;
        CombatManager.OnNotEnoughAP -= HandleNotEnoughAP;
    }

    public void UpdatePlayerUI(PlayerController player)
    {
        playerHPBar.maxValue = player.maxHP;
        playerHPBar.value = player.currentHP;
        playerBlockText.text = $"Block: {player.currentBlock}";
        playerAPText.text = $"AP: {player.currentAP}/{player.maxAP}";
        playerAPText.color = player.currentAP == 0 ? Color.red : Color.white;
        playerHPText.text = $"HP: {player.currentHP}/{player.maxHP}";
    }

    public void UpdateEnemyUI(CombatContext context)
    {
        EnemyController enemy = context.enemy;

        enemyHPBar.maxValue = enemy.maxHP;
        enemyHPBar.value = enemy.currentHP;
        enemyBlockText.text = $"Block: {enemy.currentBlock}";
        enemyIntentText.text = enemy.GetIntentDescription(context);
        enemyHPText.text = $"HP: {enemy.currentHP}/{enemy.maxHP}";
    }

    public void UpdateDeckUI(DeckManager deck)
    {
        drawPileText.text = $"{deck.drawPile.Count}";
        discardPileText.text = $"{deck.discardPile.Count}";
    }

    private void Refresh(CombatContext context)
    {
        UpdatePlayerUI(context.player);
        UpdateEnemyUI(context);
        UpdateDeckUI(context.deck);
        RefreshHand(context);
    }

    private void RefreshHand(CombatContext context)
    {
        List<CardData> hand = context.deck.hand;
        // Destroy existing cards
        foreach (Transform child in handPanel)
            Destroy(child.gameObject);

        // Create a card for each CardData in the hand
        foreach (CardData cardData in hand)
        {
            GameObject cardGO = Instantiate(cardPrefab, handPanel);
            CardView cardView = cardGO.GetComponent<CardView>();
            cardView.Setup(cardData, context, (card) => combatManager.PlayCard(card));        
            }
    }

    private void HandleCombatEnd(bool victory)
    {
        blockerOverlay.SetActive(true);
        combatEndPanel.SetActive(true);
        resultText.text = victory ? "Victory!" : "Defeat!";
        buttonText.text = victory ? "Next" : "New Game";
    }

    public IEnumerator ShowNotEnoughAP()
    {
        notEnoughAPText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        notEnoughAPText.gameObject.SetActive(false);
    }

    private void HandleNotEnoughAP()
    {
        StartCoroutine(ShowNotEnoughAP());
    }
}