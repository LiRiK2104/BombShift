using PlayerLogic;
using ShopSystem.Items;
using UnityEngine;

namespace UI.BannerSystem
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
        
        protected override Skin CreateSkin(Skin skinPrefab)
        {
            var skin = base.CreateSkin(skinPrefab);
            MeshUILayerSetter.SetUILayer(skin.gameObject);

            return skin;
        }
    }
}
