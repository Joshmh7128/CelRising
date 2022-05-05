using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillHandler : MonoBehaviour
{
    [SerializeField] Transform bladeRot;
    [SerializeField] float spinRate;

    // update our direction every fixed update
    private void FixedUpdate()
    {
        bladeRot.Rotate(0, 0, spinRate);
    }
}
