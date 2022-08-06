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
            List<Transform> spawnPositions = NetworkManager.startPositions;
            await Task.Delay(MatchRespawnTime);
            foreach (var player in FindObjectsOfType<Player>())
            {
                player.transform.position = spawnPositions[0].position;
                player.transform.rotation = Quaternion.LookRotation(-player.transform.position);
                spawnPositions.RemoveAt(0);
                player.GetComponent<PlayerScore>().ResetScore();
                player.GetComponent<Dash>().ResetDash();              
                ScoreView.Instance.ChangeWinnerText("");
            }
        }


    }
}