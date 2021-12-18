using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEnemies : MonoBehaviour
{
    public GameObject demonFighterPreFab;
    public GameObject[] floors;
    public GameObject enemies;

    // Spawns an enemy on the selected ground tiles and puts them as childs in the Enemies Empty
    private void Start()
    {
            for (int i = 0; i < floors.Length; i++)
            {
                Instantiate(demonFighterPreFab, new Vector3(floors[i].transform.position.x, 0.3f, floors[i].transform.position.z), Quaternion.identity, enemies.transform);
            }
    }
}
