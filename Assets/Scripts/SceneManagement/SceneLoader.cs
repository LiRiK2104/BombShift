using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        public void CloseGame()
        {
            EditorApplication.isPlaying = false;
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
