using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPower : MonoBehaviour
{
    [SerializeField] private float Angle;
    [SerializeField] private float Power;

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Angle,90,0)*Power,ForceMode.Force);
    }
}
