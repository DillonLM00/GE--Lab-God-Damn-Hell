using UnityEngine;
using UnityEngine.AI;

public class DemonSpearMovement : MonoBehaviour
{
    private Stats demonSpearStats;
    private DemonSpearAnimations demonSpearAnimations;
    private GameObject player;
    private float aggroRange;
    private NavMeshAgent agent;
    public bool activated;


    private void Start()
    {
        player = GameObject.Find("Player Object");
        agent = GetComponent<NavMeshAgent>();
        demonSpearStats = GetComponent<Stats>();
        demonSpearAnimations = GetComponent<DemonSpearAnimations>();
        activated = demonSpearStats.activated;

        agent.speed = demonSpearStats.movementspeed;
        aggroRange = demonSpearStats.aggroRange;
    }

    void FixedUpdate()
    {
        navmovement();
    }

    private void navmovement()
    {
        if (activated)
        {
            agent.destination = player.transform.position;

            if (Vector3.Distance(transform.position, player.transform.position) <= 6)
            {
                demonSpearAnimations.inAttackRange = true;
            }
            else
            {
                demonSpearAnimations.inAttackRange = false;
            }
        }
        else
        {
            if ((Vector3.Distance(transform.position, player.transform.position) <= aggroRange))    //if the Player is in a certain distance
            {
                if (Vector3.Angle(transform.forward, player.transform.position - transform.position) <= 40) //if the player is in the view field of the enemy
                {
                    activated = true;
                    GetComponentInParent<AlarmOtherEnemies>().activityHasChanged = true;
                }
            }
        }
    }
}
