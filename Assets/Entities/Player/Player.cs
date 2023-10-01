using System;
using System.ComponentModel;
using DamageSystem;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Entities.Player {
    public class Player : MonoBehaviour {
        [SerializeField] private float acceleration = .1f;
        [SerializeField] private float deceleration = 1f;
        [SerializeField] private float maxRotationSpeed = 1f;
        [SerializeField] private Transform spriteToCounterrotate;
        private float spriteToCounterrotateInitialScaleX;
        private float currentSpeed = 0f;
        private WeaponDamageDealer weaponDamageDealer;
        private Rigidbody2D rb;
        private float lastPressedTurn;
        private float turn;
        [Tooltip("Check if there is animation controller above weapon triggering attack")]
        [SerializeField] private bool disableInputAttackTrigger = false;

        private void Start() {
            weaponDamageDealer = GetComponent<WeaponDamageDealer>();
            rb = GetComponent<Rigidbody2D>();
            if (!disableInputAttackTrigger) {
                InputManager.actions.Player.Attack.started += context => weaponDamageDealer.OnAttack();
            }
            InputManager.actions.Player.Left.started += context => turn = -1;
            InputManager.actions.Player.Right.started += context => turn = 1;
            if (spriteToCounterrotate) {
                spriteToCounterrotateInitialScaleX = spriteToCounterrotate.localScale.x;
            }
        }

        public void OnAttackEnd() {
            if (InputManager.actions.Player.Attack.IsPressed()) {
                weaponDamageDealer.OnAttack();
            }
        }

        void FixedUpdate() {
            if (!InputManager.actions.Player.Left.IsPressed() &&
                !InputManager.actions.Player.Right.IsPressed()) turn = 0;
            if (turn != 0 && turn == -lastPressedTurn) {
                currentSpeed *= -1;
            } else if (turn == 0) {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, deceleration);
            } else {
                if (Math.Abs(lastPressedTurn - (-turn)) < .1f) {
                    currentSpeed *= -1;
                } else {
                    currentSpeed += turn * acceleration;
                }
            }

            currentSpeed = Math.Clamp(currentSpeed, -maxRotationSpeed, maxRotationSpeed);
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                transform.eulerAngles.z - currentSpeed
            );
            if (spriteToCounterrotate) {
                spriteToCounterrotate.eulerAngles = new Vector3(
                    spriteToCounterrotate.eulerAngles.x,
                    spriteToCounterrotate.eulerAngles.y,
                    spriteToCounterrotate.eulerAngles.z + currentSpeed
                );
                spriteToCounterrotate.localScale = new Vector3(
                    transform.eulerAngles.z % 360 <= 180 ? spriteToCounterrotateInitialScaleX : -spriteToCounterrotateInitialScaleX,
                    spriteToCounterrotate.localScale.y,
                    spriteToCounterrotate.localScale.z
                );
            }

            lastPressedTurn = turn;
        }
    }
}
