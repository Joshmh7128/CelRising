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

    public void OnPickup()
    {

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
