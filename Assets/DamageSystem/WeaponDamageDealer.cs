using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace DamageSystem {
    public class WeaponDamageDealer : MonoBehaviour, IActiveDamageDealer {
        [SerializeField] private Weapon weapon;
        [SerializeField] private ParticleSystem particleSystem;


        public float GetDamage() {
            return weapon.GetDamage();
        }

        public void OnAttack() {
            if (!weapon) return;

            weapon.Attack();

            if (particleSystem == null) return;
            
            if (!particleSystem.isPlaying) 
                particleSystem.Play();

            
           
        }
    }
}
