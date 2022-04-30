using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] LayerMask ignore;
    [SerializeField] GameObject highlightedObject; // the object we are highlighting
    public Transform mouseTransform, mouseHoldPoint; // our mouse transform
    [SerializeField] float mouseLerpSpeed; // our mouse lerp speed
    RaycastHit hit;

    // stuff for picking up and placing tiles
    public TileClass heldTile; // the tile we are holding

    private void Start()
    {
        // lock the cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // move our mouse in world space
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 20;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        // then get the direction
        Vector3 mouseCastDir = (mouseWorldPos - Camera.main.transform.position);
        // then cast a ray
        Physics.Raycast(Camera.main.transform.position, mouseCastDir, out hit, 50f, ~ignore, QueryTriggerInteraction.Collide);
        // move the worldspace mouse
        if (hit.transform != null)
        { 
            Vector3 mouseLerpTarget = hit.point;
            mouseTransform.position = Vector3.Lerp(mouseTransform.position, mouseLerpTarget, mouseLerpSpeed * Time.deltaTime);
        }
        
    }
}
