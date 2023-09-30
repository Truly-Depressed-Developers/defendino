using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace DamageSystem {
    public class WeaponDamageDealer : MonoBehaviour, IActiveDamageDealer {
        [SerializeField] private Weapon weapon;
        [SerializeField] private ParticleSystem particleSystem;

        private float lastTimeDamageGot = 0;

        public float GetDamage() {
            particleSystem.gameObject.SetActive(true);
            lastTimeDamageGot = Time.time;

            return weapon.GetDamage();
        }

        public void OnAttack() {
            if (!weapon) return;

            weapon.Attack();
        }

        IEnumerator CheckParticleSystem() {
            while (true) {
                if (Time.time - lastTimeDamageGot > 1000) {
                    particleSystem.gameObject.SetActive(false);
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
