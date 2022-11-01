using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public static class Scene
    {
        public static void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
