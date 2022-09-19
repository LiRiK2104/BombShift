using System.Collections.Generic;
using UnityEngine;

namespace Shop.Toggles
{
    public class ToggleGroup : MonoBehaviour
    {
        private List<Toggle> _toggles = new List<Toggle>();

        public void AddToggle(Toggle toggle)
        {
            if (_toggles.Contains(toggle) == false)
                _toggles.Add(toggle);
        }

        public void SelectToggle(Toggle targetToggle)
        {
            if (_toggles.Contains(targetToggle))
            {
                _toggles.ForEach(toggle => toggle.Deselect());
                targetToggle.Select();
            }
        }
    }
}
