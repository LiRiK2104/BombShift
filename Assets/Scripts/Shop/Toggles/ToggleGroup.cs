using System.Collections.Generic;
using UnityEngine;

namespace Shop.Toggles
{
    public class ToggleGroup : MonoBehaviour
    {
        protected List<Toggle> Toggles = new List<Toggle>();

        public void AddToggle(Toggle toggle)
        {
            if (Toggles.Contains(toggle) == false)
                Toggles.Add(toggle);
        }

        public void SelectToggle(Toggle targetToggle)
        {
            if (Toggles.Contains(targetToggle))
            {
                Toggles.ForEach(toggle => toggle.Deselect());
                targetToggle.Select();
            }
        }
    }
}
