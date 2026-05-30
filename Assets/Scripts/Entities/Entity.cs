using UnityEngine;
using System.Collections.Generic;


public class Entity : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    public int currentBlock;

    public List<StatusEffect> activeStatuses = new List<StatusEffect>();

    public void TakeDamage(int amount)
    {
        int remaining = amount - currentBlock;
        currentBlock = Mathf.Max(0, currentBlock - amount);
        if (remaining > 0)
        {
            currentHP = Mathf.Max(0, currentHP - remaining);
        }
    }

    public void GainBlock(int amount)
    {
        currentBlock += amount;
    }

    public void ResetBlock()
    {
        currentBlock = 0;
    }

    public int GetModifiedDamage(int baseDamage)
    {
        int result = baseDamage;
        foreach (StatusEffect status in activeStatuses)
            result = status.ModifyOutgoingDamage(result);
        return result;
    }

    public void ApplyStatus(StatusEffect status)
    {
        StatusEffect existing = activeStatuses.Find(s => s.GetType() == status.GetType());
        if (existing != null)
            existing.duration += status.duration;
        else
            activeStatuses.Add(status);
    }

    public void TickStatuses()
    {
        for (int i = activeStatuses.Count - 1; i >= 0; i--)
        {
            activeStatuses[i].OnTurnEnd(this);
            if (activeStatuses[i].duration <= 0)
                activeStatuses.RemoveAt(i);
        }
    }
}