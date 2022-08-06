using Mirror;
namespace Gameplay
{
    public class PlayerScore : NetworkBehaviour
    {
        private const int PointsForWin = 3;

        [SyncVar(hook = nameof(UpdateScore))]
        private int _score;
        public int Score => _score;

        [ServerCallback]
        public void AddScore(int points)
        {
            _score += points;
            if (_score == PointsForWin)
            {
                ScoreView.Instance.ChangeWinnerText($"{GetComponent<Player>().SyncName} wins");
                MatchRestarter.Instance.Restart();
            }
        }

        [Client]
        private void UpdateScore(int oldValue, int newValue) =>
             ScoreView.Instance.UpdateScoreText();

        [ServerCallback]
        public void ResetScore() =>
            _score = 0;

    }
}