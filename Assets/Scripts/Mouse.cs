using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] TileClass highlightedTile;

    private void Update()
    {
        // check if we can pick it up
        if (highlightedTile && !player.heldTile)
        {
            // if we press the mouse button
            if (Input.GetMouseButtonDown(0))
            {
                // pick it up
                highlightedTile.OnPickup();
                player.heldTile = highlightedTile;
                highlightedTile = null;
            }
        }

        if (highlightedTile && player.heldTile)
        {
            // if we press the mouse button
            if (Input.GetMouseButtonDown(0))
            {
                // replace the tile
                player.heldTile.OnPlace(highlightedTile.transform.position);
                highlightedTile.OnReplace();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Tile")
        {
            Debug.Log("tile highlighted");
            highlightedTile = other.transform.gameObject.GetComponent<TileClass>();
        }
    }
}
