using UnityEngine;
using UnityEngine.Events;

namespace DamageSystem.Weapons.ConeWeapon {
    public class ConeWeaopn : Weapon {
        public float damage = 20f;
        [SerializeField] private UnityEvent OnAttackEnd;
        private new Collider2D collider;
        private Animator animator;
        private ParticleSystem particleSystem;
        private Transform inner;

        public override float GetDamage() {
            return damage;
        }

        private void Awake() {
            collider = GetComponentInChildren<Collider2D>();
            animator = GetComponent<Animator>();
            particleSystem = GetComponentInChildren<ParticleSystem>();
            inner = transform.GetChild(0);
        }

        public override void Attack() {
            collider.enabled = true;
            animator.Play("Attack");
        }

        public void PlayParticleSystem() {
            particleSystem.Play();
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
