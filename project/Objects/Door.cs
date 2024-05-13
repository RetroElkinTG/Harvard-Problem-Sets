using UnityEngine;

// Door types
public enum DoorType
{
    key,
    enemy,
    button
}

// Door behaviour
public class Door : ObjectManager
{
    [Header("Door Variables")]
    public DoorType myDoorType;
    public bool isOpen;
    public InventoryValues inventory;
    public ObjectValues objectValues;
    public SpriteRenderer doorSprite;
    public BoxCollider2D boxCollider;

    // Disable door if already opened
    void Start()
    {
        isOpen = objectValues.runtimeDoorIsOpen;
        if (isOpen)
        {
            doorSprite.enabled = false;
            boxCollider.enabled = false;
            gameObject.SetActive(false);
        }
    }

    // CheckForKey each frame update
    void Update()
    {
        CheckForKey();
    }

    // Check if Player has key
    public void CheckForKey()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (playerInRange && myDoorType == DoorType.key)
            {
                if (inventory.runtimeKeyCount > 0)
                {
                    inventory.runtimeKeyCount--;
                    OpenDoor();
                }
            }
        }
    }

    // Open door
    public void OpenDoor() 
    {
        isOpen = true;
        objectValues.runtimeDoorIsOpen = isOpen;
        doorSprite.enabled = false;
        boxCollider.enabled = false;
        gameObject.SetActive(false);
    }
}