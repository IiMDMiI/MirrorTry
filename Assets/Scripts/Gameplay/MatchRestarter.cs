using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Mirror;

namespace Gameplay
{
    public class MatchRestarter : NetworkBehaviour
    {
        private const int MatchRespawnTime = 5000;
        public static MatchRestarter Instance;

        [ServerCallback]
        private void Awake() =>
            Instance = this;

        [ServerCallback]
        public async void Restart()
        {
            List<Transform> spawnPositions = new List<Transform>(NetworkManager.startPositions);
            await Task.Delay(MatchRespawnTime);
            foreach (var player in FindObjectsOfType<Player>())
            {
                ResetPosition(spawnPositions[0].position, player);
                ResetRotation(player);
                spawnPositions.RemoveAt(0);
                player.GetComponent<PlayerScore>().ResetScore();
                player.GetComponent<Dash>().ResetDash();
                player.GetComponent<ThirdPersonCamera>().ResetCamera();
                ScoreView.Instance.ChangeWinnerText("");
            }
        }

        [ClientRpc]
        private void ResetPosition(Vector3 position, Player player) =>
            player.transform.position = position;

        [ClientRpc]
        private void ResetRotation(Player player) =>
            player.transform.rotation = Quaternion.LookRotation(-player.transform.position);

    }
}