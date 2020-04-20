using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFireable
{
    /**
     * Triggers fire and intilializes projectiles (if applicable)
     * @param wielder - the wielder that instagated the weapon fire
     * @param intendedDirection - unit vector indicating the intended direction
     *  of the projectile
     * @return whether the fire was successful
     */
    bool Fire(IWieldable firer);

    /**
     * Returns whether the object can fire. Reasons that a weapon cannot fire
     * may be cooldown, overheat, or out-of-ammo
     */
    bool CanFire();

    /** Type of weapon. See below */
    FireableType type { get; }

    /** Name to display to user */
    string name { get; }
    /** Team weapon belongs to */
    Teams team { get; set; }
    /** Starts cooldown */
}
public enum FireableType
{
    MissileLauncher,
    Gun,
    HitScan
}