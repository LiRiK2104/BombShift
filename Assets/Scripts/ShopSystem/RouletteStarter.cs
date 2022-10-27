using ShopSystem.Pages;
using UnityEngine;

namespace ShopSystem
{
    public class RouletteStarter : MonoBehaviour
    {
        public RoulettePageView PageView { get; set; }
        
        public void Roll()
        {
            if (PageView != null)
                PageView.StartRoll();
        }
    }
}
