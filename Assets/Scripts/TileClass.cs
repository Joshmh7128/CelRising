using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClass : MonoBehaviour
{
    // are we being hovered over?
    public bool isHover = false;
    float highlightAlphaLerp;
    [SerializeField] float highlightAlphaLerpSpeed; // how fast do we lerp too and from our current alpha state

    // our gameobject
    [SerializeField] Renderer highlightRenderer;

    private void Start()
    {

    }


    private void FixedUpdate()
    {
        // check and update our hover lerp
        if (isHover)
        { highlightAlphaLerp = 1; } else
        { highlightAlphaLerp = 0; }

        // run our hover lerp
        HoverLerp();
    }

    void HoverLerp()
    {
        Mathf.Lerp(highlightRenderer.material.color.a, highlightAlphaLerp, highlightAlphaLerpSpeed*Time.deltaTime);
    }

}
