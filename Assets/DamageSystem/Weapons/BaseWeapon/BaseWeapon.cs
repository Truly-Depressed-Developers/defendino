using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace DamageSystem.Weapons.MeleeWeapon {
    public class MeleeWeapon : Weapon {
        public float damage = 20f;
        [SerializeField] private UnityEvent OnAttackEnd;
        private new Collider2D collider;
        private Animator animator;
        private Transform inner;

        public override float GetDamage() {
            return damage;
        }

        private void Awake() {
            collider = GetComponentInChildren<Collider2D>();
            animator = GetComponent<Animator>();
            inner = transform.GetChild(0);
        }

        public override void Attack() {
            collider.enabled = true;
            if (animator) {
                animator.Play("Attack");
            }
        }

        private void _OnAttackEnd() {
            collider.enabled = false;
            OnAttackEnd.Invoke();
        }

        private void OnDrawGizmos() {
            if (!collider) return;
            Gizmos.color = collider.enabled ? Color.yellow : Color.gray;
            DrawHitbox();
        }

        private void DrawHitbox() {
            Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
            // Vector3[] points = collider.points.Select(point => inner.TransformPoint(point)).ToArray();
            // for (int i = 0; i < points.Length; i++) {
            //     Vector3 a = points[i];
            //     Vector3 b = points[i + 1 < points.Length ? i + 1 : 0];
            //     Gizmos.DrawLine(a, b);
            // }
        }
    }
}
