using SceneManagement;
using UnityEngine;
using Zenject;

namespace Progress
{
    public class ProgressController : MonoBehaviour
    {
        [Inject] private SceneLoader _sceneLoader;
        
        public void StartOver()
        {
            PlayerPrefs.DeleteAll();
            _sceneLoader.CloseGame();
        }
    }
}
