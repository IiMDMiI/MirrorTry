using UnityEngine;
using Mirror;

namespace Gameplay
{
    public class PlayerSetuper : NetworkBehaviour
    {
        private void Start()
        {   
            IInputService input = new MouseKeybordInput();
            PlayerAnimator animator = new PlayerAnimator(GetComponentInChildren<Animator>());

            Dash dash = GetComponent<Dash>();
            dash.Initialize(animator);

            Camera camera = GetComponentInChildren<Camera>();
            ThirdPersonCamera thirdPersonCamera = GetComponent<ThirdPersonCamera>();
            thirdPersonCamera.Initialize(input, transform, camera.transform);

            GetComponent<ClashController>().Initialize(GetComponent<MaterialChanger>(), dash, GetComponent<PlayerScore>());
            
            if (isLocalPlayer)
            {   
                Cursor.lockState = CursorLockMode.Locked;
                Player player = GetComponent<Player>();
                SetPlayerName(player.GetNameFromPlayerPrefs(), player);

                camera.enabled = true;

                GetComponent<Movement>().Initialize(input, animator, dash, camera);
                RotateToSceneCenter();
                thirdPersonCamera.SetCameraLookBehindCharacter();
            }
        }

        [Command]
        private void SetPlayerName(string name, Player player) =>
            player.SyncName = name;

        [Command]
        private void RotateToSceneCenter() =>
            transform.rotation = Quaternion.LookRotation(-transform.position);
    }
}

