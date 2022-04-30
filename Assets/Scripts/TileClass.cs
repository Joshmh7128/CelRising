using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClass : MonoBehaviour
{
    // are we being hovered over?
    public bool isHover = false;
    float highlightAlphaLerp;
    [SerializeField] float highlightAlphaLerpSpeed; // how fast do we lerp too and from our current alpha state
    Player player; 
    // our gameobject
    [SerializeField] Renderer highlightRenderer;
    [SerializeField] GameObject belowTile; // the tile that is revealed underneath ours

    private void Start()
    {
        // find our player controller
        player = FindObjectOfType<Player>();
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

    // what to do when we are picked up
    public void OnPickup()
    {
        Instantiate(belowTile, transform.position, Quaternion.identity, null);
        gameObject.GetComponent<Collider>().enabled = false;
    }    

    // what to do when we are placed
    public void OnPlace(Vector3 position)
    {
        player.heldTile = null;
        transform.position = position;
        gameObject.GetComponent<Collider>().enabled = true;
    }

    // what to do when we are replaced
    public void OnReplace()
    {
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
