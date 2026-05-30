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
    public TextMeshProUGUI playerAPText;
    public TextMeshProUGUI playerHPText;
    public GameObject playerBlockIcon;
    public TextMeshProUGUI playerBlockValueText;

    [Header("Enemy")]
    public Slider enemyHPBar;
    public Transform enemyIntentContainer;
    public TextMeshProUGUI enemyHPText;
    public GameObject enemyBlockIcon;
    public TextMeshProUGUI enemyBlockValueText;


    [Header("Status Icons")]
    public GameObject statusIconPrefab;
    public Transform playerStatusContainer;
    public Transform enemyStatusContainer;

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
    public DraftManager draftManager;
    public GameObject defeatPanel;

    [Header("Feedback")]
    public TextMeshProUGUI notEnoughAPText;

    private CombatContext _context;

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

    public void UpdatePlayerUI(CombatContext context)
    {
        PlayerController player = context.player;

        // HP Bar
        playerHPBar.maxValue = player.maxHP;
        playerHPBar.value = player.currentHP;
        playerHPBar.fillRect.GetComponent<Image>().color = 
            player.currentBlock > 0 ? Color.blue : Color.red;

        // HP Text
        playerHPText.text = $"{player.currentHP}/{player.maxHP}";

        // AP Text
        playerAPText.text = $"AP: {player.currentAP}/{player.maxAP}";
        playerAPText.color = player.currentAP == 0 ? Color.red : Color.white;

        // Block
        playerBlockIcon.SetActive(player.currentBlock > 0);
        playerBlockValueText.text = player.currentBlock.ToString();

        // Status effects
        foreach (Transform child in playerStatusContainer)
            Destroy(child.gameObject);
        foreach (StatusEffect status in player.activeStatuses)
            SpawnStatusIcon(playerStatusContainer, status.icon, status.duration.ToString(), status.GetTooltip());
    }

    public void UpdateEnemyUI(CombatContext context)
    {
        EnemyController enemy = context.enemy;

        // HP Bar
        enemyHPBar.maxValue = enemy.maxHP;
        enemyHPBar.value = enemy.currentHP;
        enemyHPBar.fillRect.GetComponent<Image>().color = 
            enemy.currentBlock > 0 ? Color.blue : Color.red;

        // HP Text
        enemyHPText.text = $"{enemy.currentHP}/{enemy.maxHP}";

        // Intent
        foreach (Transform child in enemyIntentContainer)
            Destroy(child.gameObject);

        foreach (EnemyIntent intent in enemy.CurrentSlot.intents)
            SpawnStatusIcon(enemyIntentContainer, intent.icon, intent.GetValue(context), intent.GetTooltip());

        // Block
        enemyBlockIcon.SetActive(enemy.currentBlock > 0);
        enemyBlockValueText.text = enemy.currentBlock.ToString();

        // Status effects
        foreach (Transform child in enemyStatusContainer)
            Destroy(child.gameObject);
        foreach (StatusEffect status in enemy.activeStatuses)
            SpawnStatusIcon(enemyStatusContainer, status.icon, status.duration.ToString(), status.GetTooltip());
    }

    public void UpdateDeckUI(DeckManager deck)
    {
        drawPileText.text = $"{deck.drawPile.Count}";
        discardPileText.text = $"{deck.discardPile.Count}";
    }

    private void Refresh(CombatContext context)
    {
        _context = context;
        UpdatePlayerUI(context);
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

            if (victory)
        draftManager.ShowDraft(_context);
    else
        defeatPanel.SetActive(true);
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

    private void SpawnStatusIcon(Transform container, Sprite icon, string value, string tooltip = "")
    {
        GameObject go = Instantiate(statusIconPrefab, container);
        go.GetComponent<Image>().sprite = icon;
        go.GetComponentInChildren<TextMeshProUGUI>().text = value;

        if (tooltip != "")
    {
        TooltipTrigger trigger = go.AddComponent<TooltipTrigger>();
        trigger.content = tooltip;
    }
    }
}