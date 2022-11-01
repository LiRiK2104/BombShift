using Helpers;
using ShopSystem.Items;
using UI;
using UnityEngine;

namespace ShopSystem.Cells.States
{
    [RequireComponent(typeof(MeshUILayerSetter))]
    public class OpenState : State
    {
        [SerializeField] private Transform _previewParent;

        public void SetPreview(Skin skin)
        {
            if (_previewParent == null)
                return;
            
            var preview = DiContainerRef.Container.InstantiatePrefab(skin, _previewParent);
            MeshUILayerSetter.SetUILayer(preview.gameObject);
        }
    }
}
