using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        int total = SceneManager.sceneCountInBuildSettings;
        Debug.Log(total);
        if (current + 1 < total)
        {
            SceneManager.LoadScene(current + 1);
        }
    }
}