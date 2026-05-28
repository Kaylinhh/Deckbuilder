using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Run State")]
    public int currentHP;
    public int maxHP = 75;
    public List<CardData> deck = new List<CardData>();
    public int currentCombatIndex = 0;
    public bool isRunActive = false;

    [Header("Starting Deck")]
    public List<CardData> startingDeck;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
            
        StartRun();
    }

    public void StartRun()
    {
        currentHP = maxHP;
        currentCombatIndex = 0;
        isRunActive = true;
        deck = new List<CardData>(startingDeck);
    }

    public void WinCombat()
    {
        currentCombatIndex++;
    }

    public void LoseCombat()
    {
        isRunActive = false;
    }
}