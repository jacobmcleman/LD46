using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigWhale {
    public struct Whale
    {
        public float health;
        public float blood; // velocity meters per second
        public float armor;
        public float fireRate; // projectils per second
        public float projectileDmg; // damage of projectile
    }
    public struct HealthChangeEvent
    {

    }
    public class WhaleStats : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

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

        }

    }
}