using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ballon")
        {
            collision.collider.GetComponent<Rigidbody>().useGravity = true;
            
        }
    }
}
