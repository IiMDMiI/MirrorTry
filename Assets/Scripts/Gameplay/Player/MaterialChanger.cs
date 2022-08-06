using UnityEngine;
using Mirror;

namespace Gameplay
{
    public partial class MaterialChanger : NetworkBehaviour
    {
        [SyncVar(hook = nameof(ChangeMaterial))]
        public PlayerMaterials Current;
        [SerializeField] private Material _default;
        [SerializeField] private Material _ignore;
        [SerializeField] private SkinnedMeshRenderer _meshRenderer;


        private void ChangeMaterial(PlayerMaterials oldMaterial, PlayerMaterials newMaterialIndex)
        {
            switch (Current)
            {
                case PlayerMaterials.Default:
                    _meshRenderer.materials = new Material[] { _default };
                    break;

                case PlayerMaterials.Unactive:
                    _meshRenderer.materials = new Material[] { _ignore };
                    break;
            }
        }
    }
}