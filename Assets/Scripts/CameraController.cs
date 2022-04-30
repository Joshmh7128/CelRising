using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// script handles camera controlling 
    [SerializeField] Transform targetRot; // our target rotation
    [SerializeField] float slerpSpeed, lerpSpeed;
    [SerializeField] Vector3 centerPos, builderPos, targetPos; 

    private void Start()
    {
        targetPos = centerPos;
    }

    // Update is called once per frame
    void Update()
    {
        // rotate it left or right if we press buttons
        if (Input.GetKeyDown(KeyCode.E))
        {   // rotate right
            targetRot.Rotate(0, 90, 0, Space.Self);
        }        
        
        if (Input.GetKeyDown(KeyCode.Q))
        {   // rotate right
            targetRot.Rotate(0, -90, 0, Space.Self);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            // toggle our targetpos
            if (targetPos == centerPos)
            { targetPos = builderPos; } 
            else 
            if (targetPos == builderPos)
            { targetPos = centerPos; }
        }

        // lerp to our target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot.rotation, slerpSpeed * Time.deltaTime);

        // lerp to our target destination
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
    }
}
