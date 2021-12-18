using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    // General variables
    public Animator animator;
    private GameObject player;
    private EnemyStats enemyStats;

    // HP
    private int tempHealthPoints;

    // Attack
    private float timeSinceLastAttack;
    private float randFloat;
    public GameObject leftHand;
    public GameObject rightHand;
    private Collider leftHandCollider;
    private Collider rightHandCollider;

    // Die
    private float timeDead;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        enemyStats = GetComponent<EnemyStats>();
        tempHealthPoints = enemyStats.healthPoints;

        leftHandCollider = leftHand.GetComponent<BoxCollider>();
        rightHandCollider = rightHand.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        Run();
        Attack();
        Die();
        GetHit();
    }

    public virtual void Run()
    {

    }

    public virtual void Attack()
    {
        // If the player is close enough start attacking
        if ((Vector3.Distance(transform.position, player.transform.position) < 5f))
        {
            randFloat = Random.Range(0f, 1f);
            animator.SetFloat("AttackFloat", randFloat);
        }
        else
        {
            animator.SetFloat("AttackFloat", 0f);
        }

        // Activates / deactivates the collider
        if (animator.GetCurrentAnimatorStateInfo(1).IsName("AttackLeft"))
        {
            leftHandCollider.enabled = true;
        }
        else
        {
            leftHandCollider.enabled = false;
        }

        if (animator.GetCurrentAnimatorStateInfo(1).IsName("AttackRight"))
        {
            rightHandCollider.enabled = true;
        }
        else
        {
            rightHandCollider.enabled = false;
        }
    }


    public virtual void Die()
    {
        // Play animation when dead
        if (enemyStats.healthPoints <= 0)
        {
            if (!animator.GetCurrentAnimatorStateInfo(3).IsName("Death"))
            {
                animator.SetBool("IsDeadBool", true);
            }
        }

        if (animator.GetBool("IsDeadBool") == true)
        {
            timeDead += Time.deltaTime;

            // Disable animator after 1.5s so it cant attack anymore
            if (timeDead > 1.5f)
            {
                animator.enabled = false;
            }

            // Despawn after 5s
            if (timeDead >= 5)
            {
                Destroy(gameObject);
            }
        }
    }

    public virtual void GetHit()
    {
        // Checks if the hp values changed, if so then play the hit animation
        if (tempHealthPoints != enemyStats.healthPoints)
        {
            animator.SetTrigger("GetHitTrigger");
            tempHealthPoints = enemyStats.healthPoints;
        }
    }
}
