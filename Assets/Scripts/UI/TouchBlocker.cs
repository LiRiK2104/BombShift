using System;
using UnityEngine;

namespace UI
{
    public class TouchBlocker : MonoBehaviour
    {
        private void Awake()
        {
            Disable();
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
