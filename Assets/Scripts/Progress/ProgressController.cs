using UnityEngine;
using Scene = SceneManagement.Scene;

namespace Progress
{
    public class ProgressController : MonoBehaviour
    {
        public void StartOver()
        {
            PlayerPrefs.DeleteAll();
            Scene.Reload();
        }
    }
}
