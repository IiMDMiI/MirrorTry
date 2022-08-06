using UnityEngine;
using Mirror;
namespace Gameplay
{
    public class Player : NetworkBehaviour
    {
        public const string NameKey = "Name";
        [SyncVar(hook = nameof(UpdateScore))]
        public string SyncName;
        public string GetNameFromPlayerPrefs() =>
            PlayerPrefs.GetString(NameKey, "Unknown" + Random.Range(0, 1000));

        private void UpdateScore(string oldValue, string newValue) =>
            ScoreView.Instance.UpdateScoreText();

    }
}