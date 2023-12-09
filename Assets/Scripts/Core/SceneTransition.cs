using UnityEngine.SceneManagement;

namespace Assets.Scripts.Core
{
    public static class SceneTransition
    {
        public static void Initiate(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

}