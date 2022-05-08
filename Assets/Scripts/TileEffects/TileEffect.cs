using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileEffect : MonoBehaviour
{
    // add this on to anything that is an effect on a tile
    public abstract void OnPlace();

}
