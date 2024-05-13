using System.Collections;
using UnityEngine;

// Enemy states
public enum EnemyState
{ 
    idle,
    walk,
    attack,
    stagger
}

// Enemy behaviour
public class EnemyManager : MonoBehaviour
{
    [Header("State Variables")]
    public EnemyState currentState;

    [Header("Enemy Variables")]
    public HealthValues enemyMaxHealth;
    public string enemyName;
    public float health;
    public int enemyBaseDamage;
    public float enemyMovementSpeed;

    [Header("Death Variables")]
    public GameObject deathAnimation;
    private float deathDuration = 1f;

    // Set enemy health
    private void Awake()
    {
        health = enemyMaxHealth.runtimeValue;
    }

    // Enemy gets hit
    public void GetHit(Rigidbody2D myRigidbody, float knockbackTime, float damage)
    {
        StartCoroutine(KnockbackTimeCo(myRigidbody, knockbackTime));
        TakeDamage(damage);
    }

    // Knockback time coroutine
    private IEnumerator KnockbackTimeCo(Rigidbody2D myRigidbody, float knockbackTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
        }
    }

    // Enemy takes damage
    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathAnimation();
            gameObject.SetActive(false);
        }
    }

    // Death animation
    private void DeathAnimation() 
    {
        if (deathAnimation != null)
        {
            GameObject animation = Instantiate(deathAnimation, transform.position, Quaternion.identity);
            Destroy(animation, deathDuration);
        }
    }
}