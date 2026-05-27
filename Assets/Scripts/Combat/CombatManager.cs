using System;
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

    private void Start()
    {
        _context = new CombatContext
        {
            player = player,
            enemy = enemy,
            deck = deckManager
        };

        StartCombat();
    }

    private void StartCombat()
    {
        deckManager.Shuffle(deckManager.drawPile);
        Debug.Log("Combat started!");
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        player.ResetBlock();
        player.currentAP = player.maxAP;
        deckManager.DrawCards(cardsPerTurn);
        Debug.Log($"Player turn — AP: {player.currentAP}, Hand: {deckManager.hand.Count} cards");
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
        Debug.Log($"Enemy uses: {enemy.currentIntent.description} — Player HP: {player.currentHP}");
        enemy.ExecuteIntent(_context);
        Debug.Log($"Player HP after: {player.currentHP}");
        CheckCombatEnd();
        OnStateChanged?.Invoke(_context);
    }

    private void CheckCombatEnd()
    {
        if (player.currentHP <= 0)
        {
            Debug.Log("Defeat!");
            _isOver = true;
            OnCombatEnded?.Invoke(false);
            return;
        }
        if (enemy.currentHP <= 0)
        {
            Debug.Log("Victory!");
            _isOver = true;
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