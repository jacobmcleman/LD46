using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    float MaxHealth { get; set; }
    float Health { get; }
    void TakeDamage(float damage);
    void Heal(float healAmount);
}
