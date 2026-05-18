using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    public PlayerController player;
    public EnemyController enemy;
    public DeckManager deckManager;

    public static event Action<CombatContext> OnStateChanged;
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

    private void Update()
    {
    if (!_isOver && Keyboard.current.spaceKey.wasPressedThisFrame)
        EndPlayerTurn();

    if (!_isOver && Keyboard.current.enterKey.wasPressedThisFrame)
    {
        if (deckManager.hand.Count > 0)
            PlayCard(deckManager.hand[0]);
        Debug.Log($"Hand: {deckManager.hand.Count}, PA: {player.currentPA}, Enemy HP: {enemy.currentHP}");
    }
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
        player.currentPA = player.maxPA;
        deckManager.DrawCards(cardsPerTurn);
        Debug.Log($"Player turn — PA: {player.currentPA}, Hand: {deckManager.hand.Count} cards");
        OnStateChanged?.Invoke(_context);
    }

    public void EndPlayerTurn()
    {
        deckManager.DiscardHand();
        CheckCombatEnd();
        if (enemy.currentHP > 0)
            StartEnemyTurn();
    }

    private void StartEnemyTurn()
    {
        enemy.ResetBlock();
        ExecuteEnemyIntent();
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
            return;
        }
        if (enemy.currentHP <= 0)
        {
            Debug.Log("Victory!");
            _isOver = true;
        }
    }

    private void PlayCard(CardData card)
    {
        if (player.currentPA < card.cost)
        {
            Debug.Log("Not enough PA!");
            return;
        }

        player.currentPA -= card.cost;
        card.Play(_context);
        deckManager.DiscardCard(card);
        CheckCombatEnd();
        OnStateChanged?.Invoke(_context);
    }

}