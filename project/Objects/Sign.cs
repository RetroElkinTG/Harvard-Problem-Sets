using UnityEngine;
using TMPro;

// Sign behaviour
public class Sign : ObjectManager
{
    [Header("Sign Variables")]
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public string dialog;
    public bool isRead;

    // Check if Player reads sign
    void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        { 
            ActivateText();
        }
    }

    // Activate text
    public void ActivateText()
    {
        if (dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(false);
            isRead = true;
        }
        else
        {
            dialogBox.SetActive(true);
            dialogText.text = dialog;
        }
    }

    // Remove text if Player leaves range
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            context.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}