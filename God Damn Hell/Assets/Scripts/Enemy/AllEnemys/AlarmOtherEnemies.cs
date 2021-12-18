using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
this Script is for the purpose of alarming other enemies,
if an enemy in Range has already seen a player object
*/


public class AlarmOtherEnemies : MonoBehaviour
{
    private int enemyNumber;
    private int alarmEnemyRange = 15;
    private List<int> listOfActivatedEnemys= new List<int>();    //list of all activated enemys
    private List<int> notActivatedEnemyList = new List<int>();   //list of all unactive enemys


    public bool activityHasChanged = true;      //if a new Enemy has been set active ->  updates the lists of activated/deactivated enemys


    private void FixedUpdate()       
    {

        if (activityHasChanged)
        {
            updateActivity();
        }
        if(notActivatedEnemyList.Count != 0 && listOfActivatedEnemys.Count != 0)
        {  
            alarmOtherEnemys();
        }
       
    }

    private void alarmOtherEnemys() // Looks if any deactivated enemys are in range of an activated enemy, if so it activates that enemy.
    {     
        for(int activEnemy = 0; activEnemy< listOfActivatedEnemys.Count; activEnemy++)
        {
            for (int unactiveEnemy = 0; unactiveEnemy < notActivatedEnemyList.Count; unactiveEnemy++)
            {
                if (Vector3.Distance(transform.GetChild(listOfActivatedEnemys[activEnemy]).position, transform.GetChild(notActivatedEnemyList[unactiveEnemy]).position) <= alarmEnemyRange)
                {
                    activityHasChanged = true;
                    transform.GetChild(notActivatedEnemyList[unactiveEnemy]).GetComponentInChildren<EnemyMovement>().activated = true;
                }
            }
        }          
       
    }

    private void updateActivity()   // updates the lists of activated/deactivated enemys
    {
        enemyNumber = transform.childCount;
        activityHasChanged = false;
        
        listOfActivatedEnemys.Clear();
        notActivatedEnemyList.Clear();

        for (int i = 0; i < enemyNumber; i++)
        {
           
            if (transform.GetChild(i).GetComponentInChildren<EnemyMovement>().activated)
            {
                listOfActivatedEnemys.Add(i);
            }
            else
            {
                notActivatedEnemyList.Add(i);
            }
        }
    }
}
