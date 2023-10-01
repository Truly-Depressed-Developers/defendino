using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities.Player {
    public class PlayersController : MonoBehaviour {
        private List<Player> players;
        [SerializeField] public Player playerA;
        [SerializeField] public Player playerB;
        [SerializeField] private float attackInterval = 1.5f;
        private float lastAttackTime;
        public static PlayersController instance;

        private void Awake() {
            if(instance == null) {
                instance = this;
            }
        }

        private void Start() {
            players = GetComponentsInChildren<Player>().ToList();
            // AddPlayer(secondPlayer);
            InputManager.actions.Player.SpawnA.started += context => AddPlayer(playerA);
            InputManager.actions.Player.SpawnB.started += context => AddPlayer(playerB);
        }

        public void AddPlayer(Player player) {
            players.Add(player);
            player = Instantiate(player, transform);
            float angleSteps = 360f / players.Count;
            for (int i = 0; i < players.Count; i++) {
                player.transform.eulerAngles = Vector3.forward * angleSteps * i;
                player.transform.localScale = Vector3.one * 0.7f;
            }
        }

        // private void Update() {
        //     if (lastAttackTime + attackInterval < Time.time) {
        //         lastAttackTime = Time.time;
        //         Attack();
        //     }
        // }

        private void Attack() {
            foreach (var player in players) {
                player.Attack();
            }
        }
    }
}
