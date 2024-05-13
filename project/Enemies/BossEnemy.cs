using System.Collections;
using UnityEngine;

// Boss Enemy behaviour
public class BossEnemy : BasicEnemy
{
    [Header("Boss Enemy Variables")]
    private float attackTime = 0.5f;

    // Check distance to Player
    public override void CheckDistance()
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
        else if (Vector2.Distance(targetPosition.position, transform.position) <= chaseRadius &&
                 Vector2.Distance(targetPosition.position, transform.position) <= attackRadius)
        {
            if (currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                StartCoroutine(AttackCo());
            }
        }
    }

    // Attack coroutine
    public IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        myAnimator.SetBool("attacking", true);
        yield return new WaitForSeconds(attackTime);
        currentState = EnemyState.walk;
        myAnimator.SetBool("attacking", false);
    }
}