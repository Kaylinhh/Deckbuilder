using UnityEngine;

public class EnemyController : Entity
{
    public IntentSlot[] phase1Pattern;
    public IntentSlot[] phase2Pattern;

    private bool _inPhase2 = false;

    private IntentSlot[] CurrentPattern
    {
        get
        {
            bool shouldBePhase2 = phase2Pattern != null && phase2Pattern.Length > 0 && currentHP < maxHP * 0.5f;
            if (shouldBePhase2 && !_inPhase2)
            {
                _inPhase2 = true;
                _currentIntentIndex = 0;
                Debug.Log($"CurrentPattern called — inPhase2: {_inPhase2}, index: {_currentIntentIndex}, phase1 length: {phase1Pattern.Length}");
                Debug.Log("Phase 2 triggered!");

            }
            return _inPhase2 ? phase2Pattern : phase1Pattern;
        }
    }
        
    private int _currentIntentIndex = 0;

    public IntentSlot CurrentSlot => CurrentPattern[_currentIntentIndex];

    public void ExecuteIntent(CombatContext context)
    {
        foreach (EnemyIntent intent in CurrentPattern[_currentIntentIndex].intents)
        {
            intent.Execute(context);
        }
        _currentIntentIndex = (_currentIntentIndex + 1) % CurrentPattern.Length;
    }

    public string GetIntentDescription(CombatContext context)
    {
        string result = "";
        foreach (EnemyIntent intent in CurrentSlot.intents)
        {
            if (result != "")
                result += " / ";
            result += intent.GetDescription(context);
        }
        return result;
    }
}