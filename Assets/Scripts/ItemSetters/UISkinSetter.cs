using ShopSystem.Items;
using UI;
using UnityEngine;

namespace ItemSetters
{
    [RequireComponent(typeof(MeshUILayerSetter))]
    public class UISkinSetter : SkinSetter
    {
        private MeshUILayerSetter _meshUILayerSetter;
        
        private MeshUILayerSetter MeshUILayerSetter
        {
            get
            {
                if (_meshUILayerSetter == null)
                    _meshUILayerSetter = GetComponent<MeshUILayerSetter>();

                return _meshUILayerSetter;
            }
        }

        protected override Skin CreateItem(Skin fragmentPrefab)
        {
            var skin = base.CreateItem(fragmentPrefab);
            MeshUILayerSetter.SetUILayer(skin.gameObject);

            return skin;
        }
    }
}
