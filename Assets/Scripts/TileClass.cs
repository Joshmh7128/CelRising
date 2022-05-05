using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClass : MonoBehaviour
{
    // are we being hovered over?
    public bool isHover = false;
    public bool inSlot; // are we in a slot?
    SlotClass slot;
    float highlightAlphaLerp;
    [SerializeField] float highlightAlphaLerpSpeed; // how fast do we lerp too and from our current alpha state
    Player player; 
    // our gameobject
    [SerializeField] Renderer highlightRenderer;
    [SerializeField] GameObject belowTile; // the tile that is revealed underneath ours
    public Generator.tileTypes tileType;
    public float arrayPosX, arrayPosY; // our x and y position in the tile array

    private void Start()
    {
        // find our player controller
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CullTile();
        }
    }

    private void FixedUpdate()
    {
        if (player.heldTile == this)
        {
            transform.position = player.mouseHoldPoint.position;
        }

        // run our hover lerp
        HoverShow();
    }

    void CullTile()
    { Destroy(gameObject); }

    // what to do when we are picked up
    public void OnPickup()
    {
        if (!inSlot)
        { Instantiate(belowTile, transform.position, Quaternion.identity, null); } 
        else if (inSlot)
        {  
            inSlot = false;
            if (slot != null)
            {
                slot.CheckSlot();
            }
            slot = null;
        }
        gameObject.GetComponent<Collider>().enabled = false;
    }    

    // what to do when we are placed
    public void OnPlace(Vector3 position, TileClass targetTile)
    {
        player.heldTile = null;
        transform.position = position;
        gameObject.GetComponent<Collider>().enabled = true;
        if (targetTile.inSlot) { inSlot = true; } else { inSlot = false; }

        // set our x and y pos of the tile to be that of the tile we are replacing
        arrayPosX = targetTile.arrayPosX;
        arrayPosY = targetTile.arrayPosY;
    }

    public void OnPlaceInSlot(Vector3 position, SlotClass localSlot)
    {
        inSlot = true;
        player.heldTile = null;
        transform.position = position;
        gameObject.GetComponent<Collider>().enabled = true;
        slot = localSlot;
        slot.heldTile = this;
        slot.CheckSlot();
    }

    // what to do when we are replaced
    public void OnReplace()
    {
        Destroy(gameObject);
    }

    public void OnCycle()
    {
        if (!inSlot)
        { Instantiate(belowTile, transform.position, Quaternion.identity, null); }
        Destroy(gameObject);
    }

    void HoverShow()
    {        
        // check and update our hover lerp
        if (isHover)
        {
            highlightRenderer.enabled = true;
        }
        else
        {
            highlightRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Mouse")
        {
            isHover = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Mouse")
        {
            isHover = false;
        }
    }
}
