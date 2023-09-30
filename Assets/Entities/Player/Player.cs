using System;
using DamageSystem;
using UnityEngine;

namespace Entities.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 1f;
        private WeaponDamageDealer weaponDamageDealer;

        private void Start()
        {
            weaponDamageDealer = GetComponent<WeaponDamageDealer>();
            InputManager.actions.Player.Attack.started += context => weaponDamageDealer.OnAttack();
        }

        public void OnAttackEnd()
        {
            if (InputManager.actions.Player.Attack.IsPressed())
            {
                weaponDamageDealer.OnAttack();
            }
        }

        void Update()     {
            if (InputManager.actions.Player.Left.IsPressed())
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + rotationSpeed);
            } else if (InputManager.actions.Player.Right.IsPressed()) {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - rotationSpeed);
            }

        }
    }
}