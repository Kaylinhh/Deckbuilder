using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    public PlayerController player;
    public EnemyController enemy;
    public DeckManager deckManager;

    public static event Action<CombatContext> OnStateChanged;
    public static event Action<bool> OnCombatEnded;
    public static event Action OnNotEnoughAP;
    
    public int cardsPerTurn = 5;

    private CombatContext _context;
    private bool _isOver = false;

    private void Awake()
    {
        _context = new CombatContext
        {
            player = player,
            enemy = enemy,
            deck = deckManager
        };
    }

    private void Start()
    {
        Debug.Log($"CombatManager.Start — GameManager deck: {GameManager.Instance.deck.Count}");
        StartCombat();
    }

    private void StartCombat()
    {
        player.maxHP = GameManager.Instance.maxHP;
        player.currentHP = GameManager.Instance.currentHP;
        deckManager.drawPile = new List<CardData>(GameManager.Instance.deck);
        deckManager.Shuffle(deckManager.drawPile);
        StartPlayerTurn();
    }
    public void StartPlayerTurn()
    {
        player.ResetBlock();
        player.currentAP = player.maxAP;
        deckManager.DrawCards(cardsPerTurn);
        OnStateChanged?.Invoke(_context);
    }

    public void EndPlayerTurn()
    {
        player.TickStatuses();
        deckManager.DiscardHand();
        CheckCombatEnd();
        if (enemy.currentHP > 0)
            StartEnemyTurn();
    }

    private void StartEnemyTurn()
    {
        enemy.ResetBlock();
        ExecuteEnemyIntent();
        enemy.TickStatuses();
        if (player.currentHP > 0)
            StartPlayerTurn();
    }

    private void ExecuteEnemyIntent()
    {
        enemy.ExecuteIntent(_context);
        CheckCombatEnd();
        OnStateChanged?.Invoke(_context);
    }

    private void CheckCombatEnd()
    {
        if (player.currentHP <= 0)
        {
            _isOver = true;
            GameManager.Instance.LoseCombat();
            OnCombatEnded?.Invoke(false);
            return;
        }
        if (enemy.currentHP <= 0)
        {
            _isOver = true;
            GameManager.Instance.currentHP = player.currentHP;
            GameManager.Instance.WinCombat();
            OnCombatEnded?.Invoke(true);
        }
    }

    public void PlayCard(CardData card)
    {
        if (player.currentAP < card.cost)
        {
            OnNotEnoughAP?.Invoke();
            return;
        }

        player.currentAP -= card.cost;
        card.Play(_context);
        deckManager.DiscardCard(card);
        CheckCombatEnd();
        OnStateChanged?.Invoke(_context);
    }

}