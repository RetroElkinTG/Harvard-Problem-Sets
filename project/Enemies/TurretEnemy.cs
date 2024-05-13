using UnityEngine;

// Turret Enemy behaviour
public class TurretEnemy : BasicEnemy
{
    [Header("Turret Enemy Variables")]
    public GameObject projectile;
    public float shotDelay;
    private float shotDelaySeconds;
    private bool canShoot = true;

    // Delay shots
    void Update()
    {
        shotDelaySeconds -= Time.deltaTime;
        if (shotDelaySeconds <= 0)
        {
            canShoot = true;
            shotDelaySeconds = shotDelay;
        }
    }

    // Check if enemy can shoot Player
    public override void CheckDistance()
    {
        if (Vector2.Distance(targetPosition.position, transform.position) <= chaseRadius &&
            Vector2.Distance(targetPosition.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                if (canShoot)
                {
                    ShootProjectile();
                    UpdateState(EnemyState.walk);
                    myAnimator.SetBool("wakeUp", true);
                }
            }
        }
        else if (Vector2.Distance(targetPosition.position, transform.position) > chaseRadius)
        {
            myAnimator.SetBool("wakeUp", false);
        }
    }

    // Shoot projectile
    public void ShootProjectile()
    {
        Vector3 targetDirection = targetPosition.transform.position - transform.position;
        GameObject currentProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        currentProjectile.GetComponent<ProjectileManager>().Shoot(targetDirection);
        canShoot = false;
    }
}