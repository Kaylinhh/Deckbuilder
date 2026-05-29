using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action OnMapShown;
    public static event Action OnCombatShown;


    [Header("Run State")]
    public int currentHP;
    public int maxHP = 75;
    public List<CardData> deck = new List<CardData>();
    public int currentCombatIndex = 0;
    public bool isRunActive = false;

    [Header("Starting Deck")]
    public List<CardData> startingDeck;

    [Header("Panels")]
    public GameObject mapPanel;
    public GameObject combatPanel;
    public GameObject combatEndPanel;
    public GameObject blockerOverlay;

    [Header("Enemies")]
    public GameObject currentEnemyPrefab;

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
        Debug.Log($"WinCombat — new index: {currentCombatIndex}");
    }

    public void LoseCombat()
    {
        isRunActive = false;
    }

    public void ShowMap()
    {
        combatEndPanel.SetActive(false);
        blockerOverlay.SetActive(false);
        mapPanel.SetActive(true);
        combatPanel.SetActive(false);
        OnMapShown?.Invoke();
    }
    public void ShowCombat()
    {
        mapPanel.SetActive(false);
        combatPanel.SetActive(true);
        OnCombatShown?.Invoke();
    }
}