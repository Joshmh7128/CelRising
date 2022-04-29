using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClass : MonoBehaviour
{
    // our gameobject
    [SerializeField] GameObject tile;

    private void Start()
    {
        Instantiate(tile);
    }
}
