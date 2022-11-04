using ShopSystem.Items;
using UI;
using UnityEngine;

namespace ItemSetters
{
    [RequireComponent(typeof(MeshUILayerSetter))]
    public class FragmentSetter : ItemSetter<Fragment>
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
        
        public virtual void SetFragment(Fragment fragment)
        {
            SetItem(fragment);
        }
        
        protected override Fragment CreateItem(Fragment fragmentPrefab)
        {
            var fragment = base.CreateItem(fragmentPrefab);
            MeshUILayerSetter.SetUILayer(fragment.gameObject);

            return fragment;
        }
    }
}
