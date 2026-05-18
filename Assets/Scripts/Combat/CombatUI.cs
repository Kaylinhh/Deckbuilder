using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        enemyIntentText.text = enemy.currentIntent.description;
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
    }
}