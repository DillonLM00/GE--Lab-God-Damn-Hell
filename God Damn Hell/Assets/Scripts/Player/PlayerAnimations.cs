using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    // General variables
    private Animator animator;
    private PlayerMovement playermovement;
    private PlayerStats playerstats;

    // Run
    private float angle;
    private Vector2 movementAxis;

    // GetHit
    private int tempHealth;

    // Die
    public bool stopRotation = false;
    public bool stopMovement = false;

    // Attack
    public BoxCollider swordCollider;
    public BoxCollider shieldCollider;

    //AttackBlock is in EnemyHit!

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerstats = GetComponent<PlayerStats>();
        playermovement = GetComponent<PlayerMovement>();
        tempHealth = playerstats.healthPoints;
    }

    private void Update()
    {
        Run();
        Attack();
        Die();
        Block();
        GetHit();
    }

    // Angle calculations for rotations are being used to determine which animations play depending on the cursors position
    private void Run()
    {

        angle = playermovement.angle;

        movementAxis.x = Input.GetAxisRaw("Horizontal");
        movementAxis.y = Input.GetAxisRaw("Vertical");

        if (angle <= 180 && angle > 90)
        {
            if (angle <= 180 && angle > 150)
            {
                animator.SetFloat("MovementForward", -movementAxis.x);
                animator.SetFloat("MovementSides", movementAxis.y);
            }
            else if (angle < 150 && angle > 120)
            {
                animator.SetFloat("MovementForward", movementAxis.y);
                animator.SetFloat("MovementSides", movementAxis.x);
            }
            else if (angle < 120 && angle >= 90)
            {
                animator.SetFloat("MovementForward", movementAxis.y);
                animator.SetFloat("MovementSides", movementAxis.x);
            }
        }

        if (angle <= 90 && angle > 0)
        {
            if (angle <= 90 && angle > 60)
            {
                animator.SetFloat("MovementForward", movementAxis.y);
                animator.SetFloat("MovementSides", movementAxis.x);
            }
            else if (angle < 60 && angle > 30)
            {
                animator.SetFloat("MovementForward", movementAxis.y);
                animator.SetFloat("MovementSides", movementAxis.x);
            }
            else if (angle < 30 && angle >= 0)
            {
                animator.SetFloat("MovementForward", movementAxis.x);
                animator.SetFloat("MovementSides", movementAxis.y);
            }
        }

        if (angle >= -180 && angle < -90)
        {
            if (angle >= -180 && angle < -150)
            {
                animator.SetFloat("MovementForward", -movementAxis.x);
                animator.SetFloat("MovementSides", -movementAxis.y);
            }
            else if (angle > -150 && angle < -120)
            {
                animator.SetFloat("MovementForward", -movementAxis.y);
                animator.SetFloat("MovementSides", -movementAxis.x);
            }
            else if (angle > -120 && angle <= -90)
            {
                animator.SetFloat("MovementForward", -movementAxis.y);
                animator.SetFloat("MovementSides", -movementAxis.x);
            }
        }

        if (angle >= -90 && angle < 0)
        {
            if (angle >= -90 && angle < -60)
            {
                animator.SetFloat("MovementForward", -movementAxis.y);
                animator.SetFloat("MovementSides", -movementAxis.x);
            }
            else if (angle > -60 && angle < -30)
            {
                animator.SetFloat("MovementForward", -movementAxis.y);
                animator.SetFloat("MovementSides", -movementAxis.x);
            }
            else if (angle > -30 && angle <= 0)
            {
                animator.SetFloat("MovementForward", movementAxis.x);
                animator.SetFloat("MovementSides", -movementAxis.y);
            }
        }
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("AttackTrigger");
        }

        if (animator.GetCurrentAnimatorStateInfo(2).IsName("Attack"))
        {
            swordCollider.enabled = true;
        }
        else
        {
            swordCollider.enabled = false;
        }
    }

    private void Die()
    {
        // Plays the death animation and sets the weight of all other layers to 0 so no animations can be played anymore
        if (playerstats.healthPoints <= 0)
        {
            animator.SetBool("isDeadBool", true);
            animator.SetLayerWeight(1, 0f);
            animator.SetLayerWeight(2, 0f);
            animator.SetLayerWeight(3, 0f);

            playermovement.enabled = false;
        }
    }

    private void Block()
    {
        if (Input.GetAxis("Fire2") != 0)
        {
            animator.SetBool("isBlocking", true);
        }
        else
        {
            animator.SetBool("isBlocking", false);
        }

        // Activates / deactivate colliders to avoid unintended blocking and hitbox shenanigans
        if (animator.GetCurrentAnimatorStateInfo(2).IsName("Block"))
        {
            shieldCollider.enabled = true;
        }
        else
        {
            shieldCollider.enabled = false;
        }
    }

    private void GetHit()
    {
        // Start() creates a copy of healthPoints and if the hp change the animation plays and the copy gets updated
        if (tempHealth != playerstats.healthPoints)
        {
            float randFloat = Random.Range(0f, 1f);
            animator.SetFloat("GetHitFloat", randFloat);
            animator.SetTrigger("GetHitTrigger");
            tempHealth = playerstats.healthPoints;
        }
    }
}




