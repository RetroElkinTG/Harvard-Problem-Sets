using System.Collections;
using UnityEngine;

// Camera behaviour
public class CameraManager : MonoBehaviour
{
    [Header("Camera Variables")]
    public Transform target;
    public float smoothing;
    public Vector2 minPosition;
    public Vector2 maxPosition;
    public Animator myAnimator;

    [Header("Audio Variables")]
    public AudioSource hitAudio;

    [Header("Scene Transition Variables")]
    public TransitionValues transitionValues;

    // Get camera components
    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        transform.position = new Vector3(transitionValues.runtimePlayerPosition.x,
            transitionValues.runtimePlayerPosition.y, transform.position.z);
        minPosition = transitionValues.runtimeCameraMinPosition;
        maxPosition = transitionValues.runtimeCameraMaxPosition;
    }

    // Bound camera and smoothly follow player movement
    void LateUpdate()
    {
        if (transform.position != target.position) 
        { 
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }

    // Kick the screen
    public void ScreenKick()
    {
        hitAudio.Play();
        myAnimator.SetBool("kickActive", true);
        StartCoroutine(ScreenKickCo());
    }

    // Screen kick coroutine
    public IEnumerator ScreenKickCo()
    {
        yield return null;
        myAnimator.SetBool("kickActive", false);
    }
}