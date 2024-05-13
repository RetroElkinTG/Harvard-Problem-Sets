using UnityEngine;

// Object behaviour
public class ObjectManager : MonoBehaviour
{
    [Header("Object Variables")]
    public bool playerInRange;
    public SignalSender context;

    // Check if Player enters range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    // Check if Player leaves range
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}