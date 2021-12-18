
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private EnemyStats enemystats;

    private GameObject player;

    private float aggroRange;

    private NavMeshAgent agent;



    public bool activated = false;


    private void Start()
    {
        player = GameObject.Find("Player Object");
        agent = GetComponent<NavMeshAgent>();
        enemystats = gameObject.GetComponent<EnemyStats>() ;

        agent.speed = enemystats.movementspeed;
        aggroRange = enemystats.aggroRange;
    }

    private void FixedUpdate()
    {
        //movement();
        navmovement();
        death();
    }


    private void navmovement()
    {
        if (activated)
        {
            agent.destination = player.transform.position;
        }
        else
        {
            if ((Vector3.Distance(transform.position, player.transform.position) <= aggroRange))    //if the Player is in a certain distance
            {
                if (Vector3.Angle(transform.forward, player.transform.position - transform.position) <= 40)//if the player is in the view field of the enemy
                {
                    activated = true;
                    GetComponentInParent<AlarmOtherEnemies>().activityHasChanged = true;
                }
            }
        }
    }

    private void death()
    {
        if (enemystats.healthPoints == 0)
        {
            //deathanimation

            GetComponentInParent<AlarmOtherEnemies>().activityHasChanged = true;
            Destroy(gameObject);
        }
    }






    //old movement Script, without NavMesh, only for the Presentation of changed code
    /*
    private void movement()
     {
         if (activated)
         {
             //if (facesPlayer)
             {
                 transform.LookAt(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z));
                 transform.position += transform.forward * Time.deltaTime * enemystats.movementspeed;
             }
         }
         else
         {
             if ((Vector3.Distance(transform.position, player.transform.position) <= aggroRange))    //if the Player is in a certain distance
             {
                 activated = true;
                 GetComponentInParent<AlarmOtherEnemies>().activityHasChanged = true;

             }
         }
     }
    */
    
}
