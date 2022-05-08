using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTerraform : TileEffect
{
    [Header("Relative to this tile, which tile are we terraforming?")]
    [SerializeField] int xAdd;
    [SerializeField] int yAdd;
    [SerializeField] float energyCost; // how much energy does it cost?
    [Header("What tile should we terraform tiles into?")]
    [SerializeField] Generator.tileTypes desiredTile;
 
    // on placement
    public override void OnPlace()
    {
        Debug.Log("Terraform OnPlace() running...");
        if (FindObjectOfType<GameManager>().energyAmount >= energyCost)
        {
            Debug.Log("Energy cost paid");
            // pay the cost
            FindObjectOfType<GameManager>().energyAmount -= energyCost;
            // find the tile next to us on the xAdd and yAdd axes
            TileClass tileClass = gameObject.GetComponent<TileClass>();
            TileClass targetTileClass = FindObjectOfType<Generator>().tiles[tileClass.arrayPosX + xAdd, tileClass.arrayPosY + yAdd].GetComponent<TileClass>();
            // terraform that target tile
            targetTileClass.OnReplace(desiredTile);
        }
    }
}
