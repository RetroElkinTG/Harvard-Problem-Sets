using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Main menu behaviour
public class MainMenu : MonoBehaviour
{
    [Header("Main Menu Variables")]
    public string sceneToLoad;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    // Start game
    public void StartGame()
    {
        StartCoroutine(SceneTransitionCo());
    }

    // Scene transition coroutine from main menu
    private IEnumerator SceneTransitionCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    // Quit application
    public void Quit()
    {
        Application.Quit();
    }
}
