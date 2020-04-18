using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigWhale {
    public struct Whale
    {
        public float health = Blood;
        public float blood = 1000; // velocity meters per second
        public float armor = 0;
        public float fireRate = 3.0; // projectils per second
        public
        public float projectileDmg = 5.0; // damage of projectile
    }
    public struct HealthChangeEvent
    {

    }
    public class WhaleStats : MonoBehaviour
    {
        public int Blood = 1000;
        public int Armor = 0;
        public int Health = Blood * Armor;
        public float TurretDmg = 10.0;
        public float HealingRate = 0.02 * BioGeneratorCount; // percent of regen per second

        // Start is called before the first frame update
        void Start()
        {
            H
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        void HealthUpdate()
        {
            
        }

        public void TakeDamage(float amount)
        {
            HealthChangeEvent healthChange;
            healthChange.amount = 
        }

    }
}