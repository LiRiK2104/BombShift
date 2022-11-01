using System;
using System.Collections.Generic;
using ShopSystem;
using ShopSystem.Items;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    public class SkinSetter : MonoBehaviour
    {
        [SerializeField] private Transform _skinSpawnPoint;
        
        [Inject] protected Inventory Inventory;
        
        private List<Skin> _createdSkins = new List<Skin>();
        
        public event Action<Skin> SkinSetted;
        

        public virtual void SetSkin(Skin skin)
        {
            if (SkinIsAvailable(skin))
            {
                DisableAllCreatedSkins();

                if (TryGetCreatedSkin(skin, out Skin foundSkin))
                    foundSkin.gameObject.SetActive(true);
                else
                    _createdSkins.Add(CreateSkin(skin));
                
                SkinSetted?.Invoke(skin);
            }
        }

        protected bool SkinIsAvailable(Skin skin)
        {
            return Inventory.HasSkin(skin);
        }

        private bool TryGetCreatedSkin(Skin needSkin, out Skin foundSkin)
        {
            foundSkin = null;

            foreach (var createdSkin in _createdSkins)
            {
                if (createdSkin.Id == needSkin.Id)
                {
                    foundSkin = createdSkin;
                    return true;
                }
            }

            return false;
        }

        protected virtual Skin CreateSkin(Skin skinPrefab)
        {
            return Instantiate(skinPrefab, _skinSpawnPoint);
        }
        
        private void DisableAllCreatedSkins()
        {
            foreach (var createdSkin in _createdSkins)
                createdSkin.gameObject.SetActive(false);
        }
    }
}
