using UnityEngine;

public class EnemyController : Entity
{
    public EnemyIntent[] pattern;
    private int _currentIntentIndex = 0;

    public EnemyIntent currentIntent => pattern[_currentIntentIndex];

    public void ExecuteIntent(CombatContext context)
    {
        currentIntent.Execute(context);
        _currentIntentIndex = (_currentIntentIndex + 1) % pattern.Length;
    }
}