using UnityEngine;

// Heart pickup behaviour
public class HeartPickup : PickupManager
{
    [Header("Heart Pickup Variables")]
    public HealthValues playerHealth;
    public HealthValues heartContainers;
    public ObjectValues objectValues;
    public int healthIncrease;
    public int healthPerHeartContainer;
    public bool isPickedUp;

    // Disable heart if already picked up
    private void Start()
    {
        isPickedUp = objectValues.runtimeHeartIsPickedUp;
        if (isPickedUp)
        {
            gameObject.SetActive(false);
        }
    }

    // Increase Player health on collision
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            playerHealth.runtimeValue += healthIncrease;
            if (playerHealth.runtimeValue > heartContainers.runtimeValue * healthPerHeartContainer)
            {
                playerHealth.runtimeValue = heartContainers.runtimeValue * healthPerHeartContainer;
            }
            isPickedUp = true;
            objectValues.runtimeHeartIsPickedUp = isPickedUp;
            pickupSignal.Raise();
            gameObject.SetActive(false);
        }
    }
}