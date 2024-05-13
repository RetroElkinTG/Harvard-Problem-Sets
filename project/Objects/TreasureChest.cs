using UnityEngine;
using TMPro;

// Treasure chest behaviour
public class TreasureChest : ObjectManager
{
    [Header("Chest Contents Variables")]
    public ItemValues contents;
    public InventoryValues inventory;
    public ObjectValues objectValues;
    public bool isOpen;
    private Animator myAnimator;

    [Header("Dialog Variables")]
    public SignalSender raiseItem;
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    // Get treaure chest components
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        isOpen = objectValues.runtimeChestIsOpen;
        if (isOpen)
        {
            myAnimator.SetBool("opened", true);
        }
    }

    // Raise context clue if Player enters range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    // Remove context clue if Player leaves range
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = false;
        }
    }

    // Check for Player interaction
    void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (!isOpen) 
            {
                OpenChest();
            }
            else
            {
                ChestOpened();
            }
        }
    }

    // Open chest
    public void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        inventory.AddItem(contents);
        inventory.currentItem = contents;
        raiseItem.Raise();
        context.Raise();
        isOpen = true;
        objectValues.runtimeChestIsOpen = isOpen;
        myAnimator.SetBool("opened", true);
    }

    // Chest is already opened
    public void ChestOpened()
    {
        dialogBox.SetActive(false);
        raiseItem.Raise();
    }
}