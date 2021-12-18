using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public Animator animator;
    public BoxCollider leftHandCollider;
    public BoxCollider rightHandCollider;
    private Animator playerAnimator;
    float lastHit = 0;

    private void Start()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        lastHit += Time.deltaTime;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        // Colliders are on the enemies hands, code below only works if the target hit is the player
        // lastHit acts as a cooldown so the player cant get hit multiple times within one animation
        if (lastHit >= 1f && other.gameObject.transform.tag == "Player" && (animator.GetCurrentAnimatorStateInfo(2).IsName("AttackLeft") || animator.GetCurrentAnimatorStateInfo(2).IsName("AttackRight")))
        {
            other.gameObject.GetComponent<PlayerStats>().healthPoints--;
            lastHit = 0;
        }
        else if (other.gameObject.transform.tag == "Shield")
        {
            playerAnimator.SetTrigger("AttackBlockTrigger");
            leftHandCollider.enabled = false;
            rightHandCollider.enabled = false;
        }
    }
}
