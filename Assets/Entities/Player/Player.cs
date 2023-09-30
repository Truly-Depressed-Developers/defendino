using System;
using DamageSystem;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Entities.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float acceleration = .1f;
        [SerializeField] private float deceleration = 1f;
        [SerializeField] private float maxRotationSpeed = 1f;
        private float currentSpeed = 0f;
        private WeaponDamageDealer weaponDamageDealer;
        private Rigidbody2D rb;
        private float lastPressedTurn;
        private float turn;

        private void Start()
        {
            weaponDamageDealer = GetComponent<WeaponDamageDealer>();
            rb = GetComponent<Rigidbody2D>();
            InputManager.actions.Player.Attack.started += context => weaponDamageDealer.OnAttack();
            InputManager.actions.Player.Left.started += context => turn = -1;
            InputManager.actions.Player.Right.started += context => turn = 1;
        }

        public void OnAttackEnd()
        {
            if (InputManager.actions.Player.Attack.IsPressed())
            {
                weaponDamageDealer.OnAttack();
            }
        }

        void FixedUpdate()
        {
            if (!InputManager.actions.Player.Left.IsPressed() && !InputManager.actions.Player.Right.IsPressed()) turn = 0;
            if (turn != 0 && turn == -lastPressedTurn)
            {
                currentSpeed *= -1;
            } else if (turn == 0)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, deceleration);
            }
            else
            {
                if (Math.Abs(lastPressedTurn - (-turn)) < .1f)
                {
                    currentSpeed *= -1;
                }
                else
                {
                    currentSpeed += turn * acceleration;
                }
            }
            currentSpeed = Math.Clamp(currentSpeed, -maxRotationSpeed, maxRotationSpeed);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - currentSpeed);
            lastPressedTurn = turn;
        }
    }
}