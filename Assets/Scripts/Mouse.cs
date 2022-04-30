using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] TileClass highlightedTile;
    [SerializeField] SlotClass highlightedSlot;

    private void Update()
    {
        // check if we can pick it up
        if (highlightedTile && !player.heldTile)
        {
            // if we press the mouse button
            if (Input.GetMouseButtonDown(0))
            {
                highlightedSlot = null;
                // pick it up
                highlightedTile.OnPickup();
                player.heldTile = highlightedTile;
                highlightedTile = null;
            }

            // if we right click
            if (Input.GetMouseButtonDown(1))
            {
                // relace it it
                highlightedTile.OnCycle();
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

            // if we generate a new level
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // delete the tile
                Destroy(player.heldTile.gameObject);
            }
        }

        if (highlightedSlot && player.heldTile)
        {
            // if we press the mouse button
            if (Input.GetMouseButtonDown(0))
            {
                // make sure we do not have a highlighted tile
                highlightedTile = null;
                // place our tile in the slot
                highlightedSlot.heldTile = player.heldTile;
                player.heldTile.OnPlaceInSlot(highlightedSlot.transform.position);
                highlightedSlot = null;
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Tile")
        {
            Debug.Log("tile highlighted");
            highlightedTile = other.transform.gameObject.GetComponent<TileClass>();
        } else if (other.transform.tag == "Slot")
        {
            Debug.Log("slot highlighted");
            highlightedSlot = other.transform.gameObject.GetComponent<SlotClass>();
            highlightedTile = null;
        } else
        {
            Debug.Log("nothing highlighted");
            highlightedTile = null;
            highlightedSlot = null;
        }
    }

}
