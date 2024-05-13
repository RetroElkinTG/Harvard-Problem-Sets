using System.Collections;
using UnityEngine;
using TMPro;

// Room transition behaviour
public class RoomTransition : MonoBehaviour
{
    [Header("Room Transition Variables")]
    private CameraManager cameraManager;
    public TransitionValues transitionValues;
    public Vector3 playerChange;
    public Vector2 cameraMinChange;
    public Vector2 cameraMaxChange;

    [Header("Location Variables")]
    public GameObject locationTextObject;
    public TextMeshProUGUI locationText;
    public string locationName;
    public bool needText;
    private float waitForSeconds = 4f;

    // Get room transition components
    void Start()
    {
        cameraManager = Camera.main.GetComponent<CameraManager>();
    }

    // Transition rooms on collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            cameraManager.minPosition += cameraMinChange;
            cameraManager.maxPosition += cameraMaxChange;
            transitionValues.runtimeCameraMinPosition = cameraManager.minPosition;
            transitionValues.runtimeCameraMaxPosition = cameraManager.maxPosition;
            collision.transform.position += playerChange;
            if (needText)
            {
                StartCoroutine(LocationNameCo());
            }
        }
    }

    // Display location name coroutine
    private IEnumerator LocationNameCo()
    {
        locationTextObject.SetActive(true);
        locationText.text = locationName;
        yield return new WaitForSeconds(waitForSeconds);
        locationTextObject.SetActive(false);
    }
}