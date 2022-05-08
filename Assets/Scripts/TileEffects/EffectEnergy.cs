using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectEnergy : TileEffect
{
    // this is a tile effect which adds or removes energy
    [Header("Our Energy Effect on the system")]
    [SerializeField] float energy;

    public override void OnPlace()
    {
        FindObjectOfType<GameManager>().energyAmount += energy;
    }
}
