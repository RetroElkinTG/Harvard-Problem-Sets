using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// World transition behaviour
public class WorldTransition : MonoBehaviour
{
    [Header("Scene Transition Variables")]
    public string sceneToLoad;
    public Vector2 cameraMinPosition;
    public Vector2 cameraMaxPosition;
    public Vector2 playerPosition;
    public TransitionValues transitionValues;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    // Create fade panel on scene transition
    private void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity);
            Destroy(panel, 1);
        }
    }

    // Store transition values on collision
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            transitionValues.runtimePlayerPosition = playerPosition;
            transitionValues.runtimeCameraMinPosition = cameraMinPosition;
            transitionValues.runtimeCameraMaxPosition = cameraMaxPosition;
            StartCoroutine(SceneTransitionCo(sceneToLoad));
        }
    }

    // Scene transition coroutine from world
    public IEnumerator SceneTransitionCo(string sceneToLoad) 
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
}