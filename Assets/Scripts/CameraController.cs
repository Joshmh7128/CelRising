using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// script handles camera controlling 
    [SerializeField] Transform targetRot; // our target rotation
    [SerializeField] float slerpSpeed;

    private void Start()
    {
        
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

        // lerp to our target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot.rotation, slerpSpeed * Time.deltaTime);
    }
}
