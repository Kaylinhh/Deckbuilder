using UnityEngine;

public class Entity : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    public int currentBlock;

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
}