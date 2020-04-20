using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    /** Maximum health */
    float MaxHealth { get; set; }
    /** Current health */
    float Health { get; }
    /** Team of object */
    Teams team { get; }
    /**
     * Reduces health of target
     * @param damage - damage to be taken
     * @param attackerTeam - team of calling projectile
     * @return true if dead; false otherwise
     */
    bool TakeDamage(float damage, Teams attackerTeam);
    /**
     * Heals to the amount specified
     * @param healAmount - amount to heal
     */
    void Heal(float healAmount);
}

/** 
 * This designates teams such that damage is taken appropriately
 */
public enum Teams 
{
    /** The player's team */
    playerTeam,
    /** The enemy's team */
    enemyTeam,
    /** environmental damage */
    giantMeteor
}
