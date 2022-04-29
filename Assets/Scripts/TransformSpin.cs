using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSpin : MonoBehaviour
{
    [SerializeField] float spinSpeed; // how fast we spin

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y + spinSpeed, 0));
    }
}
