using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLerp : MonoBehaviour
{
    float zeit = 0;
    public GameObject posStart;
    public GameObject posEnd;

    // Update is called once per frame
    void Update()
    {
        zeit += Time.deltaTime;
        
        this.transform.position = Vector3.Lerp(posStart.transform.position,posEnd.transform.position, zeit * 0.04f);
    }
}
