using UnityEngine;

[CreateAssetMenu(menuName = "DeckbuilderSO/StatusEffects/VulnerableEffect")]
public class VulnerableEffect : StatusEffect
{
    public float damageMultiplier = 1.5f;

    public override int ModifyIncomingDamage(int damage)
    {
        return Mathf.RoundToInt(damage * damageMultiplier);
    }

    public override void OnTurnEnd(Entity entity)
    {
        duration--;
    }

    public override string GetTooltip()
    {
        return "Vulnerable: takes 50% more damage.";
    }
}