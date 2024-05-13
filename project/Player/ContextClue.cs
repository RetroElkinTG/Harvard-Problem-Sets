using UnityEngine;

// Context clue behaviour
public class ContextClue : MonoBehaviour
{
    public GameObject contextClue;
    public bool contextActive = false;

    // Change context clue
    public void ChangeContext()
    {
        contextActive = !contextActive;
        if (contextActive) 
        { 
            contextClue.SetActive(true); 
        }
        else 
        {
            contextClue.SetActive(false); 
        }
    }
}