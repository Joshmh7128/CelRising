using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSpin : MonoBehaviour
{
    [SerializeField] float spinSpeedY; // how fast we spin

    // Update is called once per frame
    void FixedUpdate()
    {
        // Rotate the object around its local y axis at 1 degree per second
        transform.Rotate(transform.up * spinSpeedY * Time.deltaTime);
    }
}
