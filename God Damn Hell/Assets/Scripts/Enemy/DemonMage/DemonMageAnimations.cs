using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonMageAnimations : MonoBehaviour
{
    // General fields
    public Animator animator;
    private Stats demonMageStats;
    private DemonMageMovement demonMageMovement;

    // GetHit
    private int tempHealthPoints;
    public int knockbackStrength = 4;

    // Attack
    public bool inAttackRange = false;
    private float random;
    public GameObject pyroball;
    public GameObject fireball;
    private GameObject castedObject;
    public Transform pyroballEmpty;
    public Transform fireballEmpty;
    private Transform player;

    // Die
    private Collider demonMageCollider;

    // IdleWalk
    private float randIdleWaitTime;
    private float timeCount;
    private NavMeshAgent agent;

    private void Start()
    {
        demonMageStats = GetComponent<Stats>();
        demonMageMovement = GetComponent<DemonMageMovement>();
        tempHealthPoints = GetComponent<Stats>().healthPoints;
        demonMageCollider = GetComponent<BoxCollider>();
        randIdleWaitTime = Random.Range(2f, 8f);
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        timeCount += Time.deltaTime;

        Run();
        Attack();
        Die();
        GetHit();
        IdleWalk();
    }

    private void Run()
    {
        if (demonMageMovement.activated == true)
        {
            if (agent.remainingDistance <= 8)
            {
                agent.ResetPath();
                animator.SetFloat("WalkRunFloat", 0f);
            }
            else
            {
                animator.SetFloat("WalkRunFloat", 1f);
                AkSoundEngine.PostEvent("DemonMageRun", gameObject);
            }
        }
    }

    private void Attack()
    {
        // If the player is close enough start attacking
        if (inAttackRange == true && (!animator.GetCurrentAnimatorStateInfo(1).IsName("Attack1") || !animator.GetCurrentAnimatorStateInfo(1).IsName("Attack2")))
        {
            animator.SetFloat("AttackFloat", random = Random.Range(0f, 1f));
            AkSoundEngine.PostEvent("DemonMageCast", gameObject);
            StartCoroutine(DemonMageCast());
        }
        else
        {
            random = 0f;
        }
    }

    private IEnumerator DemonMageCast()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(1).length);

        if (random < 0.2f && random > 0f)
        {
            Instantiate(pyroball, transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
        }
        else if (random > 0.2f)
        {
            Instantiate(fireball, transform.position + new Vector3(0f, 3f, 0f), Quaternion.identity);
        }

        Debug.Log(castedObject);
    }

    private void Die()
    {
        // Play animation when dead
        if (demonMageStats.healthPoints <= 0)
        {
            if (!animator.GetCurrentAnimatorStateInfo(3).IsName("Death"))
            {
                animator.SetBool("IsDeadBool", true);
                AkSoundEngine.PostEvent("DemonMageDeath", gameObject);
                GetComponentInParent<AlarmOtherEnemies>().activityHasChanged = true;
                demonMageMovement.enabled = false;
                demonMageCollider.enabled = false;
                StartCoroutine(DieCoroutine());
            }
        }
    }

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        animator.enabled = false;
        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
    }

    private void GetHit()
    {
        if (tempHealthPoints != demonMageStats.healthPoints)
        {
            animator.SetTrigger("GetHitTrigger");
            tempHealthPoints = demonMageStats.healthPoints;
            AkSoundEngine.PostEvent("DemonMageGotHit", gameObject);
        }
    }

    private void IdleWalk()
    {
        if (demonMageMovement.activated == false)
        {
            if (timeCount >= randIdleWaitTime)
            {
                agent.speed = demonMageStats.movementspeed;
                Vector3 newDestination = new Vector3(Random.Range(-8, 8), 0f, Random.Range(-8, 8)) + transform.position;
                agent.destination = newDestination;
                randIdleWaitTime = Random.Range(2f, 8f);
                timeCount = 0f;
            }

            if (agent.remainingDistance >= 0.1)
            {
                animator.SetFloat("WalkRunFloat", 0.5f);
                AkSoundEngine.PostEvent("DemonMageWalk", gameObject);
            }
            else
            {
                animator.SetFloat("WalkRunFloat", 0f);
            }
        }
    }
}