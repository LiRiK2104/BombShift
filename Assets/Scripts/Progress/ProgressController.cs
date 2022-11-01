using UnityEngine;

namespace Progress
{
    public class ProgressController : MonoBehaviour
    {
        public void StartOver()
        {
            PlayerPrefs.DeleteAll();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
