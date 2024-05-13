using UnityEngine;

// Basic Enemy behaviour
public class BasicEnemy : EnemyManager
{
    [Header("Basic Enemy Movement Variables")]
    public Rigidbody2D myRigidbody;
    public Transform homePosition;
    public Transform targetPosition;
    public float chaseRadius;
    public float attackRadius;

    [Header("Basic Enemy Animation Variables")]
    public Animator myAnimator;

    // Get basic enemy components
    void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        targetPosition = GameObject.FindWithTag("Player").transform;
    }

    // CheckDistance per physics frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    // Check distance to Player
    public virtual void CheckDistance()
    {
        if (Vector2.Distance(targetPosition.position, transform.position) <= chaseRadius &&
            Vector2.Distance(targetPosition.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                UpdateState(EnemyState.walk);
                myAnimator.SetBool("wakeUp", true);
                MoveTowardsPlayer();
            }
        }
        else if (Vector2.Distance(targetPosition.position, transform.position) > chaseRadius)
        {
            myAnimator.SetBool("wakeUp", false);
        }
    }

    // Move towards Player
    public virtual void MoveTowardsPlayer()
    {
        Vector3 targetDirection = Vector2.MoveTowards(transform.position, targetPosition.position,
                    enemyMovementSpeed * Time.deltaTime);
        UpdateAnimation(targetDirection - transform.position);
        myRigidbody.MovePosition(targetDirection);
    }

    // Update state
    public void UpdateState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    // Update animation
    public void UpdateAnimation(Vector2 direction)
    { 
        direction = direction.normalized;
        myAnimator.SetFloat("moveX", direction.x);
        myAnimator.SetFloat("moveY", direction.y);
    }
}