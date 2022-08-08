using UnityEngine;
using Mirror;
using System.Threading.Tasks;

namespace Gameplay
{
    public class ClashController : NetworkBehaviour
    {
        private const int ScorePointsForClash = 1;
        [SerializeField] private int _ignoreTime = 3;
        [SyncVar(hook = nameof(OnIgnoreClashingSync))]
        public bool IgnoresClashing;
        private MaterialChanger _materialChanger;
        private Dash _dash;
        private PlayerScore _playerScore;
        public bool IsDashing => _dash.IsDashing;

        public void Initialize(MaterialChanger materialChanger, Dash dash, PlayerScore playerScore)
        {
            _materialChanger = materialChanger;
            _dash = dash;
            _playerScore = playerScore;
        }

        [ServerCallback]
        private void OnTriggerEnter(Collider other)
        {
            if (IsDashing)
            {
                var opponent = other.GetComponent<ClashController>();
                if (opponent.IgnoresClashing)
                    return;

                _playerScore.AddScore(ScorePointsForClash);
                opponent.IgnoresClashing = true;
                SetIgnoreClashingFalseAfterDelay(_ignoreTime, opponent);
            }
        }

        [Client]
        private void OnIgnoreClashingSync(bool oldValue, bool newValue)
        {
            if (IgnoresClashing)
                SetCurrentMaterial(PlayerMaterials.Unactive);
            else
                SetCurrentMaterial(PlayerMaterials.Default);
        }

        [ServerCallback]
        private void SetCurrentMaterial(PlayerMaterials material) =>
            _materialChanger.Current = material;

        [ServerCallback]
        private async void SetIgnoreClashingFalseAfterDelay(int seconds, ClashController clashController)
        {
            await Task.Delay(seconds * 1000);
            clashController.IgnoresClashing = false;
        }

    }
}