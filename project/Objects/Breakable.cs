using System.Collections;
using UnityEngine;

// Breakable object behaviour
public class Breakable : MonoBehaviour
{
    [Header("Breakable Object Variables")]
    private Animator objectAnimator;

    // Get breakable object components
    void Start()
    {
        objectAnimator = GetComponent<Animator>();
    }

    // Break object
    public void Break() 
    {
        objectAnimator.SetBool("broken", true);
        StartCoroutine(BreakTimeCo());
    }

    // Break time coroutine
    IEnumerator BreakTimeCo() 
    {
        yield return new WaitForSeconds(.3f);
        gameObject.SetActive(false);
    }
}