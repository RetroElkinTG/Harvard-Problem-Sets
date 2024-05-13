using UnityEngine;

// Projectile behaviour
public class ProjectileManager : MonoBehaviour
{
    [Header("Projectile Variables")]
    public float speed;
    public Vector2 targetDirection;
    private float runtimeShotDuration;
    public float defaultShotDuration;
    public Rigidbody2D myRigidbody;

    // Get projectile components
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        runtimeShotDuration = defaultShotDuration;
    }

    // Destroy projectile after specific duration
    void Update()
    {
        runtimeShotDuration -= Time.deltaTime;
        if (runtimeShotDuration <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    // Shoot projectile
    public void Shoot(Vector2 targetDirection)
    {
        myRigidbody.velocity = targetDirection * speed;
    }

    // Destroy projectile on hit
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
