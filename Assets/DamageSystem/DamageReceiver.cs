﻿using System.Collections;
using System.Collections.Generic;
using DamageSystem.Health;
using UnityEngine;
using UnityEngine.Events;

namespace DamageSystem {
    public class DamageReceiver : MonoBehaviour {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private DeathAction deathAction = DeathAction.RespawnAtInitialPosition;
        [SerializeField] private LayerMask damageSources;
        [SerializeField] private UnityEvent OnDeath;
        [SerializeField] private UnityEvent<float> OnDamageReceived;

        private float health;
        private Vector3 initialPosition;

        private enum DeathAction {
            RespawnAtInitialPosition,
            Destroy,
            None,
        }

        private void Awake() {
            health = maxHealth;
            if (healthBar) healthBar.SetMaxHealth(maxHealth);
            initialPosition = transform.position;
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (damageSources != (damageSources | 1 << other.gameObject.layer)) return;
            other.gameObject.TryGetComponent(out CollisionDamageDealer damageDealer);
            if (!damageDealer) return;
            TakeDamage(damageDealer.GetDamage());
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (GetComponent<Weapon>()) return;
            if (damageSources != (damageSources | 1 << other.gameObject.layer)) return;
            WeaponDamageDealer damageDealer = other.gameObject.GetComponentInParent<WeaponDamageDealer>();
            if (!damageDealer) return;
            TakeDamage(damageDealer.GetDamage());
        }

        private void TakeDamage(float amount) {
            OnDamageReceived.Invoke(amount);

            health -= amount;
            health = Mathf.Clamp(health, 0, maxHealth);
            if (health == 0) {
                Die();
            }

            if (healthBar) healthBar.SetHealth(health);

            void Die() {               
                OnDeath.Invoke();

                if (deathAction == DeathAction.Destroy) {
                    Destroy(gameObject);
                } else if (deathAction == DeathAction.RespawnAtInitialPosition) {
                    transform.position = initialPosition;
                    health = maxHealth;
                } else if (deathAction == DeathAction.None) {
                    return;
                }
            }
        }
    }
}
