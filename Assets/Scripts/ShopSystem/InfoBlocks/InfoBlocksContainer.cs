using System.Collections.Generic;
using Helpers;
using ShopSystem.Pages;
using UnityEngine;

namespace ShopSystem.InfoBlocks
{
    public class InfoBlocksContainer : MonoBehaviour
    {
        private List<InfoBlock> _spawnedBlocks = new List<InfoBlock>();

        public void SetInfoBlock(IInfoBlockOwner infoBlockOwner)
        {
            var infoBlock = GetCreatedInfoBlock(infoBlockOwner.InfoBlockPrefab);

            HideAllBlocks();
            infoBlock.gameObject.SetActive(true);
            infoBlock.Initialize(infoBlockOwner.InfoBlockData);
        }

        public void SetInfoBlock(Page page)
        {
            if (page is IInfoBlockOwner infoBlockOwner)
                SetInfoBlock(infoBlockOwner);
            else
                HideAllBlocks();
        }

        private InfoBlock GetCreatedInfoBlock(InfoBlock reference)
        {
            foreach (var spawnedBlock in _spawnedBlocks)
            {
                if (reference == spawnedBlock.Prefab)
                    return spawnedBlock;
            }

            var newBlock = DiContainerRef.Container.InstantiatePrefabForComponent<InfoBlock>(reference, transform);
            newBlock.OnCreate(reference);
            _spawnedBlocks.Add(newBlock);

            return newBlock;
        }

        private void HideAllBlocks()
        {
            _spawnedBlocks.ForEach(infoBlock => infoBlock.gameObject.SetActive(false));
        }
    }
}