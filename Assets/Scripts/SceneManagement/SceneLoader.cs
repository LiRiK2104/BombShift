using UnityEditor;
using UnityEngine;

namespace SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        public void CloseGame()
        {
            EditorApplication.isPlaying = false;
        }
    }
}
