using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities.Player {
    public class PlayersController : MonoBehaviour {
        private List<Player> players;
        [SerializeField] private Player secondPlayer;

        private void Start() {
            players = GetComponentsInChildren<Player>().ToList();
            AddPlayer(secondPlayer);
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
    }
}
