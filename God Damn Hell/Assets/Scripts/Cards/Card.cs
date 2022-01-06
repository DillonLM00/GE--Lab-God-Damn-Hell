using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName = "Card") ]
public class Card : ScriptableObject
{
    public string itemname;

    public int defense;     //optional
    public int cost;

}
    
