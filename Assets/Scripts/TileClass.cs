using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClass : MonoBehaviour
{
    // are we being hovered over?
    bool isHover = false;
    public bool isPermanent;
    public bool becomesPermanent;
    public bool inSlot; // are we in a slot?
    public bool isBedrock; // is this bedrock?
    SlotClass slot;
    float highlightAlphaLerp;
    [SerializeField] float highlightAlphaLerpSpeed; // how fast do we lerp too and from our current alpha state

    // game management
    GameManager gameManager;
    Player player; 
    // our gameobject
    [SerializeField] Renderer highlightRenderer;
    [SerializeField] GameObject belowTile; // the tile that is revealed underneath ours
    public Generator.tileTypes tileType;
    public int arrayPosX, arrayPosY; // our x and y position in the tile array

    // game mechanic variables
    [SerializeField] float energyContribution; // how much energy do we contribute or take from the system?

    // all of our tile effects
    [SerializeField] List<TileEffect> tileEffects;

    private void Start()
    {
        // find our player controller
        player = FindObjectOfType<Player>();
        // 
        gameManager = FindObjectOfType<GameManager>();
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
        if (!inSlot && !isPermanent)
        { TileClass tempTileClass = Instantiate(belowTile, transform.position, Quaternion.identity, null).GetComponent<TileClass>();
            tempTileClass.arrayPosX = arrayPosX; tempTileClass.arrayPosY = arrayPosY;
        } 
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
        if (!targetTile.isPermanent)
        {
            player.heldTile = null;
            transform.position = position;
            gameObject.GetComponent<Collider>().enabled = true;
            if (targetTile.inSlot) { inSlot = true; } else { inSlot = false; }

            // set our x and y pos of the tile to be that of the tile we are replacing
            Debug.Log(targetTile.arrayPosX);
            Debug.Log(targetTile.arrayPosY);
            arrayPosX = targetTile.arrayPosX;
            arrayPosY = targetTile.arrayPosY;

            if (becomesPermanent)
            {
                isPermanent = true;
            }

            // check if we contribute energy
            if (energyContribution != 0)
            { ContributeEnergy(); }

        }
   
        // make sure to activate all the effects of our tile
        if (tileEffects != null)
        {
            foreach (TileEffect effect in tileEffects)
            {
                effect.OnPlace();
            }
        }
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
        if (!isPermanent)
        Destroy(gameObject);
    }

    // overload for when we want to replace an object with a specific type
    public void OnReplace(Generator.tileTypes tileType)
    {
        // instantiate the new tile here, and give it our coordinates
        TileClass tempTileClass = Instantiate(FindObjectOfType<Generator>().tileTypeList[(int)tileType], transform.position, Quaternion.identity).GetComponent<TileClass>();
        tempTileClass.arrayPosX = this.arrayPosX;
        tempTileClass.arrayPosY = this.arrayPosY;
        // set ourselve in the generator
        FindObjectOfType<Generator>().tiles[arrayPosX, arrayPosY] = tempTileClass.gameObject;

        // destroy this
        Destroy(gameObject);
    }

    public void OnCycle()
    {
        /*
        if (!inSlot)
        { Instantiate(belowTile, transform.position, Quaternion.identity, null); }
        Destroy(gameObject);*/
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

    void ContributeEnergy()
    {
        gameManager.energyAmount += energyContribution;
    }
}
