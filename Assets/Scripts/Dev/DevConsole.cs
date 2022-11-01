using System.Diagnostics;
using Progress;
using UnityEngine;
using Scene = SceneManagement.Scene;
using UnityEngine.UI;
using Zenject;

namespace Dev
{
    public class DevConsole : MonoBehaviour, IInitializable
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _startOverButton;

        [Inject] private ProgressController _progressController;
        
        public void Initialize()
        {
#if UNITY_EDITOR
            _closeButton.onClick.AddListener(Scene.Reload);
            _startOverButton.onClick.AddListener(StartOver);
#else
            Close();
#endif
        }
        
        public void Open()
        {
#if UNITY_EDITOR
            gameObject.SetActive(true);
#endif
        }
        
        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void StartOver()
        {
            _progressController.StartOver();
        }
    }
}
