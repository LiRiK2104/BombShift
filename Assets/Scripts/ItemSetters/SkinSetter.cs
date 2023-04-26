using System;
using ShopSystem.Items;

namespace ItemSetters
{
    public class SkinSetter : ItemSetter<Skin>
    {
        public event Action<Skin> SkinSetted;
        

        public virtual void SetSkin(Skin skin)
        {
            if (SkinIsAvailable(skin))
            {
                SetItem(skin);
                SkinSetted?.Invoke(skin);
            }
        }

        protected bool SkinIsAvailable(Skin skin)
        {
            return Inventory.HasSkin(skin);
        }
    }
}
