using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsScripts : MonoBehaviour
{

    private Rigidbody rb;
    bool isHit;
    float waitTodestory;
    private void Start()
    {
        waitTodestory = 2;
        rb = GetComponent<Rigidbody>();
        
    }

    private void LateUpdate()
    {
        waitTodestory -= Time.deltaTime;
        if (!isHit && waitTodestory<=0)
        {
            Controller.instance.currentObjects--;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag==Tags.Ground)
        {
            Destroy(rb);
            isHit = true;
        }
    }

} // class




























































