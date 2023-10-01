using DamageSystem;
using UnityEngine;

namespace Entities.Enemy {
    public class Enemy : MonoBehaviour {
        [SerializeField] float _speed = 1.0f;
        [SerializeField] float attackInterval = 2.0f;
        private WeaponDamageDealer weaponDamageDealer;

        Vector3 _target = new Vector3(0, 0, 0);
        Rigidbody2D rb;
        private bool stopped = false;
        private float lastAttackTime = 0f;
        [SerializeField] public float expOnKill { get; private set; } = 5f;

        // Start is called before the first frame update
        void Start() {
            rb = GetComponent<Rigidbody2D>();
            weaponDamageDealer = GetComponent<WeaponDamageDealer>();
        }

        // Update is called once per frame
        void Update() {
            if (stopped) {
                if (lastAttackTime < Time.time + attackInterval) {
                    weaponDamageDealer.OnAttack();
                    lastAttackTime = Time.time;
                }
            } else {
                rb.MovePosition(Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime));
            }
        }

        private void OnCollisionEnter2D(Collision2D other) {
            stopped = true;
            rb.velocity = Vector3.zero;
        }
    }
}
