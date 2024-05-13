using UnityEngine;

// Death sign behaviour
public class DeathSign : Sign
{
    [Header("Death Sign Variables")]
    public PlayerManager playerManager;
    private float knockbackTime = 0;
    public int damage;
    public float damageDelay;
    private float damageDelaySeconds;

    // Check if Player reads sign
    void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            ActivateText();
        }
        if (isRead)
        {
            KillPlayer();
        }
    }

    // Kill the Player
    void KillPlayer()
    {
        damageDelaySeconds -= Time.deltaTime;
        if (damageDelaySeconds <= 0)
        {
            playerManager.DamagePlayer(knockbackTime, damage);
            damageDelaySeconds = damageDelay;
        }
    }
}