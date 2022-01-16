using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRot : MonoBehaviour
{
    public float angle = 5f;
    public GameObject point;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle = Time.deltaTime * 100;
        this.transform.RotateAround(point.transform.position,Vector3.up,angle);
    }
}
