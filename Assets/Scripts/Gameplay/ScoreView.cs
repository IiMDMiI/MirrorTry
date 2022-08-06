using System.Linq;
using UnityEngine;
using TMPro;
using Mirror;

namespace Gameplay
{
    public class ScoreView : NetworkBehaviour
    {
        public static ScoreView Instance;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _winner;



        private void Awake() =>
            Instance = this;

        [Client]
        public void UpdateScoreText()
        {
            PlayerScore[] playersScores = FindObjectsOfType<PlayerScore>().OrderByDescending(ps => ps.Score).ToArray();
            string text = "";
            foreach (var score in playersScores)
                text += $"{score.GetComponent<Player>().SyncName}: {score.Score}\n";

            _score.text = text;
        }

        [ClientRpc]
        public void ChangeWinnerText(string winner) =>
            _winner.text = winner;
    }
}