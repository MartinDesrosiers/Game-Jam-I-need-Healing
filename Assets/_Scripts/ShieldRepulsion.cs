using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRepulsion : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemies")
        {
            Vector3 norm = Vector3.Normalize(transform.position - other.transform.position);
            norm.x = -norm.x;
            norm.y = 0;
            norm.z = -norm.z;
            other.transform.GetComponent<Enemy>().canMove = false;
            other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.GetComponent<Enemy>().oldPos = transform.position;
            other.transform.GetComponent<Enemy>().norm = norm;
        }
    }
}
