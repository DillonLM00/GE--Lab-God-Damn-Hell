using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    private float lastHit;

    public int tempKnockbackStrength = 5;

    private void Update()
    {
        lastHit += Time.deltaTime;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (lastHit >= 1f && other.gameObject.transform.tag == "Enemy" && animator.GetCurrentAnimatorStateInfo(2).IsName("Attack"))
        {
            other.gameObject.transform.GetComponent<EnemyStats>().healthPoints--;
            Vector3 direction = (other.transform.position - player.transform.position);
            other.gameObject.GetComponent<Rigidbody>().AddForce(direction * tempKnockbackStrength, ForceMode.Impulse);
            lastHit = 0;
        }
    }


    //public virtual void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.transform.parent.tag == "Enemy" && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack"))
    //    {
    //        // <EnemyStats> should just be stats once actual enemies exist
    //        other.gameObject.transform.parent.GetComponent<EnemyStats>().healthPoints--;
    //        Debug.Log("Hit the enemy!");
    //        //other.gameObject.GetComponent<Rigidbody>().AddForce(-other.transform.forward * 100, ForceMode.Impulse);
    //    }
    //}
}
